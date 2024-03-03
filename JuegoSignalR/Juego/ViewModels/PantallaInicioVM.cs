using Juego.Models;
using Juego.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.ViewModels
{
    public class PantallaInicioVM
    {

        private DelegateCommand jugarCommand;

        public PantallaInicioVM()
        {
            jugarCommand = new DelegateCommand(jugarCommand_Execute);
        }

        public DelegateCommand JugarCommand
        {
            get { return jugarCommand; }
        }   

        /// <summary>
        /// Funcion que te lleva a la pantalla de partida
        /// </summary>
        private async void jugarCommand_Execute()
        {
            await Shell.Current.Navigation.PushAsync(new Partida());
        }
    }
}
