using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamDemo.Hubs
{
    public class StreamHub : Hub
    {
        public async Task SendLine(IAsyncEnumerable<string> lines)
        {
            await foreach (var line in lines)
            {
                await Clients.Others.SendAsync("GetLine", line);
            }

        }
        
    }
}
