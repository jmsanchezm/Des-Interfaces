using Juego.Models;

using Microsoft.AspNetCore.SignalR.Client;
namespace Juego.ViewModels
{
    public class PartidaVM : clsVMBase
    {

        #region atributos
        private HubConnection conexion;

        private DelegateCommand sumaCommand;
        private string emojiSeleccionado;
        private string cadenaEmoji="";
        private int segundos;
        private int recuento;
        private int recuentoPlayer2;
        #endregion

        public PartidaVM()
        {
            segundos = 15;
            recuento = 0;
            recuentoPlayer2 = 0;
            sumaCommand = new DelegateCommand(sumaCommand_Execute);

            conexion = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7229/juegoHub")
                    .Build();


            //Recibe la cadena de emojis y el emoji seleccionado
            conexion.On<string, string>("ReceiveCadenaEmoji", (cadena, emoji)=>
            {
                cadenaEmoji = cadena;
                emojiSeleccionado = emoji;
                NotifyPropertyChanged("CadenaEmoji");
                NotifyPropertyChanged("EmojiSeleccionado");
            });

            //Recibe la orden de empezar a contar los segundos
            conexion.On("SendSeconds", () =>
            {
                restarSeg();
            });

            Iniciar();
            conexion.InvokeAsync("SendCadenaEmoji");

        }

        public DelegateCommand SumaCommand
        {
            get { return sumaCommand; }
        }

        public string CadenaEmoji
        {
            get { return cadenaEmoji; }
        }
        
        public int Recuento
        {
            get { return recuento; }
            set { recuento = value; }
        }   

        public string EmojiSeleccionado
        {
            get { return emojiSeleccionado; }
        }
        public int Segundos
        {
            get { return segundos; }
            set { segundos = value; }
        }


        private void sumaCommand_Execute()
        {
            recuento++;
            NotifyPropertyChanged("Recuento");

        }

        private async void restarSeg()
        {
            while (segundos >0)
            {
                await Task.Delay(1000);
                segundos--;
                NotifyPropertyChanged("Segundos");

            }
            //Si se acaba el tiempo, envia el recuento de emojis del jugador 1 al jugador 2
            if (segundos == 0)
            {
                //Compruebo si la conexion esta activa
                if (conexion.State == HubConnectionState.Disconnected || conexion.State == HubConnectionState.Connected)
                {
                    //Si no esta activa, la inicia
                    if (conexion.State == HubConnectionState.Disconnected)
                    {
                        Iniciar();
                    }

                    //Recibe el recuento de emojis del jugador 2
                    conexion.On<int>("ReceivePlayer", (recuentoP2) =>
                    {
                        recuentoPlayer2 = recuentoP2;
                        //Comprueba los resultados de la partida
                        comprobacionesResultados(recuentoP2);
                    });

                    conexion.InvokeAsync("SendPlayer", recuento);

                }
            }
        }

        /// <summary>
        /// Inicia la conexion con el servidor
        /// Llama al metodo SendCadenaEmoji del servidor
        /// </summary>
        private async void Iniciar()
        {
            await conexion.StartAsync();

          
        }

        /// <summary>
        /// Cuenta cuantas veces aparece el emoji seleccionado en la cadena de emojis
        /// </summary>
        /// <returns></returns>
        private int cuentaEmoji()
        {
            int cuenta = 0;

            //Recorre la cadena de emojis y cuenta cuantas veces aparece el emoji seleccionado
            for (int i = 0; i < cadenaEmoji.Length; i++)
            {
                if (cadenaEmoji[i].ToString() == emojiSeleccionado)
                {
                    cuenta++;
                }
            }
            return cuenta;
        }
       

        /// <summary>
        /// Comprueba los resultados de la partida
        /// </summary>
        /// <param name="puntuacionp2"></param>
        private async void comprobacionesResultados(int puntuacionp2)
        {
            //Si los dos jugadores han contado el mismo numero de emojis
            if (recuento == cuentaEmoji() && puntuacionp2 == cuentaEmoji())
            {
                await App.Current.MainPage.DisplayAlert("Empate", "Habeis empatado", "Aceptar");
            }
            //Si el jugador 1 ha acertado el numero de emojis y el jugador 2 no
            else if (recuento == cuentaEmoji() && puntuacionp2!=cuentaEmoji())
            {
                await App.Current.MainPage.DisplayAlert("Ganaste", "Has ganado", "Aceptar");
            }
            //Si el jugador 2 ha acertado el numero de emojis y el jugador 1 no
            else if (recuento != cuentaEmoji() && puntuacionp2 == cuentaEmoji())
            {
                await App.Current.MainPage.DisplayAlert("Perdiste", "Has perdido", "Aceptar");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Perdisteis", "Habeis perdido", "Aceptar");
            }
        }
    }
}
