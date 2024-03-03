using Microsoft.AspNetCore.SignalR;

namespace Servidor.Hub
{
    public class JuegoHub 
    {
        public async Task SendCadenaEmojis(string cadenaEmoji)
        {
           // await Clients.All.SendAsync("RecibirCadenaEmojis", cadenaEmoji);
        }


    }
}
