using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamDemo.Hubs
{
    public class StreamHub:Hub
    {
        public async Task SendCoord(int x, int y)
        {
            await Clients.All.SendAsync("ReceiveCoord", x, y);
        }
    }
}
