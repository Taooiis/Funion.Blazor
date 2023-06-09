using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Furion.InstantMessaging;
using Microsoft.AspNetCore.SignalR;

namespace FunionBlazor.Core
{
    [MapHub("/hubs/chathub")]
    public class ChatHub : Hub
    {
    }
}
