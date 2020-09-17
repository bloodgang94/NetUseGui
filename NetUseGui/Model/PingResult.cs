using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetUseGui.Model
{
    class PingResult
    {
        public long RoundtripTime { get; private set; }
        public IPStatus Status { get; private set; }
        public IPAddress IpAddress { get; private set; }

        public PingResult(long roundtripTime, IPStatus status, IPAddress ipAddress)
        {
            RoundtripTime = roundtripTime;
            Status = status;
            IpAddress = ipAddress;
        }
    }
}
