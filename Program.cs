using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ProtoBuf;

namespace SteamAutoStarter
{
    static class Program
    {
        private const int STEAM_IHS_PORT = 27036;
        private const ulong STEAM_IHS_SIGNATURE = 0xA05F4C21FFFFFFFF;

        [STAThread]
        static void Main()
        {
            var listener = new UdpClient(STEAM_IHS_PORT, AddressFamily.InterNetwork);
            Console.WriteLine("SteamAutoStarter running, waiting for Steam IHS discovery messages");

            while(true)
            {
                var endpoint = new IPEndPoint(IPAddress.Any, STEAM_IHS_PORT);
                byte[] data = listener.Receive(ref endpoint);
                int position = 0;

                Console.WriteLine($"Got data from {endpoint.Address}!");

                //check signature
                ulong signature = BitConverter.ToUInt64(data, position);
                position += 8;
                if (signature != STEAM_IHS_SIGNATURE) {
                    Console.WriteLine("Packet has no Steam IHS signature, ignoring");
                    continue;
                }

                //check header
                int headerLength = (int)BitConverter.ToUInt32(data, position);
                position += 4;

                var headerMsg = Serializer.Deserialize<CMsgRemoteClientBroadcastHeader>(data.AsSpan(position, headerLength));
                if (headerMsg.MsgType != ERemoteClientBroadcastMsg.kERemoteClientBroadcastMsgDiscovery) {
                    Console.WriteLine("This is not a discovery message, ignoring");
                    continue;
                }

                Console.WriteLine($"Got a discovery message from {endpoint.Address} (clientId {headerMsg.ClientId})!");
                //use the Steam browser protocol to launch steam regardless of installation location
                Console.WriteLine("Starting steam...");
                System.Diagnostics.Process.Start("steam:");
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            }
        }
    }
}
