using System;
using System.Net.Sockets;

public class RecievedEventArgs : EventArgs
{
    public readonly byte[] Data;
    public readonly string StringData;
    public readonly System.Net.Sockets.TcpClient Client;

    public RecievedEventArgs(byte[] data, string stringData, TcpClient client)
    {
        Data = data;
        StringData = stringData;
        Client = client;
    }
}