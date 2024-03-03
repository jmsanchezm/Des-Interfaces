using System.ComponentModel;
using System.Text;

namespace Server
{
    public class GameInfo : INotifyPropertyChanged
    {

        #region atributos
        private string cadenaEmoji;
        private string emojiSeleccionado;
        private int segundos;
        private List<string> emojis = new List<string>
        {
            "😀", "😃", "😄", "😁", "😆", "😅", "😂", "🤣", "😊", "😇", "🙂", "🙃",
            "😉", "😌", "😍", "🥰", "😘", "😗", "😙", "😚", "😋", "😛", "😝", "😜"
        };
        #endregion


        #region Constructor
        public GameInfo()
        {
            generarCadenaEmojis();
            segundos = 15;
            Task.Run(() => restarSeg());
        }

        #endregion

        #region Propiedades
        public int Segundos
        {
            get { return segundos; }
        }

        public string CadenaEmoji
        {
            get { return cadenaEmoji; }
            set { cadenaEmoji = value; }
        }


        public string EmojiSeleccionado
        {
            get { return emojiSeleccionado; }
            set { emojiSeleccionado = value; }
        }

        #endregion


        #region Metodos
        private string SeleccionarEmoji()
        {
            string emoji = "";
            Random random = new Random();
            int index = random.Next(emojis.Count);
            emoji = emojis[index];

            return emoji;
        }

        public void generarCadenaEmojis()
        {
            // Generar cadena aleatoria de emojis
            for (int i = 0; i < 110; i++)
            {
                cadenaEmoji += SeleccionarEmoji();
            }

            // Obtener el emoji específico
            emojiSeleccionado = SeleccionarEmoji();

            // Obtener el número aleatorio de veces para agregar el emoji específico
            Random random = new Random();
            int numeroVeces = random.Next(1, 30);

            // Obtener posiciones aleatorias para agregar el emoji específico
            List<int> randomPositions = Enumerable.Range(0, cadenaEmoji.Length).OrderBy(x => random.Next()).Take(numeroVeces).ToList();

            // Agregar el emoji específico en las posiciones aleatorias
            foreach (int position in randomPositions)
            {
                cadenaEmoji.Insert(position, emojiSeleccionado);
            }

        }

        private void restarSeg()
        {
            while (segundos > 0)
            {
                Thread.Sleep(1000);
                segundos--;

                NotifyPropertyChanged("Segundos");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
