﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamDemo.Hubs
{
    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class StreamHub : Hub
    {
        public async Task SendCoord(Coord coord)
        {
            await Clients.Others.SendAsync("GetCoord", coord);
        }
    }
}
