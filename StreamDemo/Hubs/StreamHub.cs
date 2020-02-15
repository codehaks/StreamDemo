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

    public class StreamHub : Hub
    {
        public async Task SendTempData(IAsyncEnumerable<string> lines)
        {
            await foreach (var line in lines)
            {
                await Clients.Others.SendAsync("GetData", line);
            }

        }

        public async  IAsyncEnumerable<string> GetTempData()
        {
            
            while (true)
            {
                await Task.Delay(1000);
                yield return DateTime.Now.Second.ToString();
            }
            
        }

       
    }
}
