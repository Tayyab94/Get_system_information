using System;
using System.Linq;
using System.Net;
using System.Management;
using System.Net.NetworkInformation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            string IPAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address =>
                                   address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));

            string MachineName = ipHostInfo.HostName;


            string mac = getMachAccdress();
            
            Console.WriteLine($"IP => {IPAddress} _ Host Name => {MachineName} _ mac_address => {mac}");
            Console.ReadKey();
        }


        public static string getMachAccdress()
        {
            string mac_src = "";
            string mac_add = "";

            foreach (System.Net.NetworkInformation.NetworkInterface net_interface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(net_interface.OperationalStatus==OperationalStatus.Up)
                {
                    mac_src += net_interface.GetPhysicalAddress().ToString();
                    break;
                }
            }
            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");

            }
            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_add = mac_add.Insert(mac_add.Length, mac_add.Substring(i, 2));
                    }
                    else
                    {
                        mac_add = mac_add.Insert(mac_add.Length, mac_src.Substring(i, 2)) + "-";
                    }
                }
            }
            return mac_add;
        }
    }
}
