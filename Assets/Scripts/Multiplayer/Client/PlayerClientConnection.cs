using Assets.Scripts.Multiplayer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class PlayerClientConnection
{
    public event EventHandler<RecievedEventArgs> DataRecieved;

    private TcpClient client;
    private const int BUFFER_SIZE = 16384;
    private ManualResetEvent connectDone = new ManualResetEvent(false);
    private ManualResetEvent sendDone = new ManualResetEvent(false);
    private ManualResetEvent recieveDone = new ManualResetEvent(false);

    public string Name { get; private set; }
    public GUID ID { get; private set; }
    public IPAddress IP { get; private set; }
    public Port Port { get; private set; }
    public bool Connected { get; private set; }
    public PlayerClientConnection(IPAddress ip, Port port)
    {
        client = new TcpClient();
        IP = ip;
        Port = port;
        client.BeginConnect(ip, port, new AsyncCallback(ConnectionCallback), client);
        connectDone.WaitOne();
    }

    private void ConnectionCallback(IAsyncResult ar)
    {
        try
        {
            client = (TcpClient)ar.AsyncState;
            client.EndConnect(ar);
            Debug.Log($"Connected to {IP}");
            SendClientData("CONNECTED");
            connectDone.Set();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void SendClientData(string text)
    {
        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(text);
        client.Client.BeginSend(byteData, 0, byteData.Length,SocketFlags.None,new AsyncCallback(SendCallback),client);
        sendDone.WaitOne();
    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            client.Client.EndSend(ar);
            sendDone.Set();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void Recieve()
    {
        NetworkStateObject state = new NetworkStateObject();
        state.client = client;
        client.Client.BeginReceive(state.Buffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(RecieveCallback), state);
        recieveDone.WaitOne();
    }

    private void RecieveCallback(IAsyncResult ar)
    {
        NetworkStateObject state = (NetworkStateObject)ar.AsyncState;
        TcpClient client = state.client;

        int bytesRead = client.Client.EndReceive(ar);
        if(bytesRead > 0)
        {
            state.sb.Append(Encoding.UTF8.GetString(state.Buffer));
            client.Client.BeginReceive(state.Buffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(RecieveCallback), state);
        }
        else
        {
            string response = string.Empty;
            byte[] byteData;
            if(state.sb.Length > 0)
            {
                response = state.sb.ToString();
                byteData = System.Text.Encoding.UTF8.GetBytes(response);
                DataRecieved?.Invoke(this, new RecievedEventArgs(byteData, response, state.client));
            }
            recieveDone.Set(); 
        }
    }
}
