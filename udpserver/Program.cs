using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udpserver
{
    class Program
    {
        public static int listenPort = 1111;

        static void Main(string[] args)
        {
            Console.WriteLine("Started, listening on port " + listenPort);
            const bool done = false;
            var listener = new UdpClient(listenPort);
            var groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    // this is the line of code that receives the broadcast message.
                    // It calls the receive function from the object listener (class UdpClient)
                    // It passes to listener the end point groupEP.
                    // It puts the data from the broadcast message into the byte array
                    // named received_byte_array.
                    // I don't know why this uses the class UdpClient and IPEndPoint like this.
                    // Contrast this with the talker code. It does not pass by reference.
                    // Note that this is a synchronous or blocking call.
                    var receiveByteArray = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP);
                    var receivedData = Encoding.ASCII.GetString(receiveByteArray, 0, receiveByteArray.Length);
                    Console.WriteLine("data follows \n{0}\n\n", receivedData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            listener.Close();
            Console.ReadLine();
        }
    }
}
