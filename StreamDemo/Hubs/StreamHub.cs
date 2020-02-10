using Microsoft.AspNetCore.SignalR;
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
    public class StreamHub:Hub
    {
        public async  IAsyncEnumerable<Coord> SendCoord(IAsyncEnumerable<Coord> coords)
        {
            await foreach (var coord in coords)
            {
                await Clients.All.SendAsync("ReceiveCoord", coord.X, coord.Y);
                yield return coord;
            }
            
        }
    }
}
