using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetUseGui.Model
{
    public class NetworkInterfaces: IConnection
    {
        public IPAddress IpAddressV4 { get; private set; }
        public IPAddress IpAddressGateway { get; private set; }
        public IPAddress IpAddressMaskV4 { get; private set; }

        public NetworkInterfaces(IPAddress ipAddressV4,IPAddress ipAddressGateway, IPAddress ipAddressMaskV4)
        {
            IpAddressGateway = ipAddressGateway;
            IpAddressMaskV4 = ipAddressMaskV4;
            IpAddressV4 = ipAddressV4;
        }

        public Task Plug()
        {
            var pinger = new Pinger(this);
            return Task.Run(async () => { await pinger.StartShareAsync(); });
        }
    }
}
