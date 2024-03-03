using Microsoft.AspNetCore.SignalR;

namespace JuegoServer.HUB
{
    public class JuegoHub: Hub
    {

        GameInfo gameInfo = new GameInfo();

        public async Task SendCadenaEmoji()
        {
            await Clients.All.SendAsync("ReceiveCadenaEmoji", gameInfo.CadenaEmoji);
        }

        public async Task SendEmojiSeleccionado()
        {
            await Clients.All.SendAsync("ReceiveEmojiSeleccionado", gameInfo.EmojiSeleccionado);
        }

        public async Task SendSegundos()
        {
            await Clients.All.SendAsync("ReceiveSegundos", gameInfo.Segundos);
        }

        public async Task SendFinJuego()
        {
            await Clients.All.SendAsync("ReceiveFinJuego");
        }

        public async Task SendPlayer (clsPlayer player)
        {
            await Clients.Others.SendAsync("ReceivePlayer", player);
        }
    }
}
