using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetUseGui.Model
{
    interface IConnection
    {
        Task Plug();
    }
}
