using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apotek
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string localIp = GetLocalIPAddress();

            Console.WriteLine("ip: " + localIp);

            DatabaseConfig.Instance.Server = "192.168.43.226";
            DatabaseConfig.Instance.DatabaseName = "apotek";
            DatabaseConfig.Instance.UserId = "apotek";
            DatabaseConfig.Instance.Password = "a321";

            if (DatabaseConfig.Instance.Server != "192.168.43.226")
            {
                //MessageBox.Show("Hubungkan komputer ke Wifi.");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDashboard());
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
    }
}
