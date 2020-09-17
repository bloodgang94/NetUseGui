using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetUseGui.Model
{
    public class NetUseModel
    {
        private ConcurrentBag<NetworkInterfaces> _networkCollection;

        public ConcurrentBag<NetworkInterfaces> NetworkCollection
        {
            get => _networkCollection ?? (_networkCollection = new ConcurrentBag<NetworkInterfaces>());
            private set=> _networkCollection = value; 
        }
        public async Task SearchNetworkAsync()
        {
            var adapter = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.OperationalStatus == OperationalStatus.Up
                                                                                && i.NetworkInterfaceType !=
                                                                                NetworkInterfaceType.Loopback);
            await Task.Run((() =>
            {
                foreach (var value in adapter)
                {
                    var ipV4 = value.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(g => g.Address.AddressFamily == AddressFamily.InterNetwork)?.Address;
                    var gateway = value.GetIPProperties().GatewayAddresses
                        .Where(i => i.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Select(i=>i.Address).FirstOrDefault();
                    var ipv4Mask = value.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(g => g.Address.AddressFamily == AddressFamily.InterNetwork)?.IPv4Mask;
                    NetworkCollection.Add(new NetworkInterfaces(ipV4,gateway, ipv4Mask));
                }
            }));
        }
    }
}
