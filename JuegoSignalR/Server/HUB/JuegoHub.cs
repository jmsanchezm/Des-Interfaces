using Microsoft.AspNetCore.SignalR;
using Server.Models;

namespace Server.HUB
{
    public class JuegoHub: Hub
    {
        //Para controlar el acceso a la generación de la cadena de emojis
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Método que envía la cadena de emojis a los clientes
        /// Envia la orden de empezar a contar los segundos
        /// </summary>
        /// <returns></returns>
        public async Task SendCadenaEmoji()
        {
            
            await semaphore.WaitAsync();

            try
            {
                //Genera la cadena de emojis y selecciona un emoji aleatorio
                GameInfo.generarCadenaEmojis();

                if (GameInfo.Player == 0)
                {
                    
                    GameInfo.Player = 1;

                }
              
                else if (GameInfo.Player == 1)
                {
                    await Clients.All.SendAsync("ReceiveCadenaEmoji", GameInfo.CadenaEmoji, GameInfo.EmojiSeleccionado);
                    await Clients.All.SendAsync("SendSeconds");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //Libera el semáforo
                semaphore.Release();
            }

        } 

        /// <summary>
        /// Método que envía el recuento de los jugadores a los clientes
        /// </summary>
        /// <param name="recuentoPlayer"></param>
        /// <returns></returns>
        public async Task SendPlayer(int recuentoPlayer)
        {
            await Clients.Others.SendAsync("ReceivePlayer", recuentoPlayer);
            
        }
    }
}
