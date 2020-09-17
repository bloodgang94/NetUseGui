using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetUseGui.Model
{
    class Pinger
    {
        private NetworkInterfaces networkInterfaces;
        public Pinger(NetworkInterfaces networkInterfaces)
        {
            this.networkInterfaces = networkInterfaces;
        }
        public async Task StartShareAsync()
        {
            var ip = IPNetwork.Parse(networkInterfaces.IpAddressGateway.ToString(), netmask: networkInterfaces.IpAddressMaskV4.ToString());
            await Task.Run(() =>
            {
                ip.ListIPAddress().ToList()
                    .AsParallel()
                    .ForAll(value => SendPing(value)
                        .ContinueWith(t => MountingAsync(t.Result)).Wait());
            });
        }
        private async Task<PingResult> SendPing(IPAddress host, int timeout = 2000)
        {
            var ping = new Ping();
            Console.WriteLine($"{host}->Start Ping... ");
            var result = await ping.SendPingAsync(host, timeout);
            return new PingResult(result.RoundtripTime, result.Status, host);
        }
        private async Task MountingAsync(PingResult result)
        {
            if (result.Status == IPStatus.Success)
            {
                SharingLogic.ShareCollection shi = SharingLogic.ShareCollection.GetShares($"{result.IpAddress}");
                foreach (var value in shi)
                {
                    if (!IsLogic(((SharingLogic.Share)value).Path))
                    {
                        Console.WriteLine($"Good! Found share path:{value} . Mounting...");
                        await Task.Run(() => NetworkDrive.MapNetworkDrive(value.ToString()));
                    }
                }
            }
            else Console.WriteLine($"{result.IpAddress}->Bad");
        }
        private bool IsLogic(string drive)
        {
            string[] drives;
            try
            {
                drives = Environment.GetLogicalDrives();
            }
            catch (Exception e)
            {
                return false;
            }
            if (drive == "" || drives.Length == 0)
                return false;
            if (drive.Length > 3)
                drive = drive.Remove(3);
            return
                drives
                    .Any(x => x.Equals(drive)) ? true : false;
        }
    }
}
