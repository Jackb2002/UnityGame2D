using System.Net.Sockets;
using System.Text;

namespace Assets.Scripts.Multiplayer
{
    internal class NetworkStateObject
    {
        public TcpClient client;
        public const int BufferSize = 16384;
        public byte[] Buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
