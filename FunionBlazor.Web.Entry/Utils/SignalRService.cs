using FunionBlazor.Core;

namespace FunionBlazor.Web.Entry.Utils
{
    public  class SignalRService
    {

        public static HubConnection hubConnection;
        
        public  SignalRService(string connectionUrl)
        {
            if (hubConnection == null) {
                // 根据应用程序需求设置 connectionUri 和 hubName
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(connectionUrl)
                    .Build();
                // 启动连接
                hubConnection.StartAsync();
            }
            
        }
        
    }
}
