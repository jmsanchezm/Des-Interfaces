using System.ComponentModel;

namespace Server.Models
{
    public static class GameInfo
    {

        #region atributos
        private static string cadenaEmoji;
        private static string emojiSeleccionado;
        private static int player;    
        private static List<string> emojis = new List<string>
        {
            "😀", "😃", "😄", "😁", "😆", "😅", "😂", "🤣", "😊", "😇", "🙂", "🙃",
            "😉", "😌", "😍", "🥰", "😘", "😗", "😙", "😚", "😋", "😛", "😝", "😜"
        };

        #endregion

        #region Propiedades

        public static string CadenaEmoji
        {
            get { return cadenaEmoji; }
            set { cadenaEmoji = value; }
        }


        public static string EmojiSeleccionado
        {
            get { return emojiSeleccionado; }
            set { emojiSeleccionado = value; }
        }
        public static int Player
        {
            get { return player; }
            set { player = value; }
        }

        #endregion

        
        #region Metodos
        /// <summary>
        /// Método que selecciona un emoji aleatorio de la lista de emojis
        /// </summary>
        /// <returns></returns>
        private static string SeleccionarEmoji()
        {
            string emoji = "";
            Random random = new Random();
            int index = random.Next(emojis.Count);
            emoji = emojis[index];

            return emoji;
        }

        /// <summary>
        /// Función que genera una cadena de emojis aleatorios y agrega un emoji específico en posiciones aleatorias
        /// </summary>
        public static void generarCadenaEmojis()
        {
            cadenaEmoji = "";
            // Genera cadena aleatoria de emojis
            for (int i = 0; i < 110; i++)
            {
                cadenaEmoji += SeleccionarEmoji();
            }

            // Obtiene el emoji específico
            emojiSeleccionado = SeleccionarEmoji();

            // Obtiene el número aleatorio de veces para agregar el emoji específico
            Random random = new Random();
            int numeroVeces = random.Next(1, 30);

            // Obtiene posiciones aleatorias para agregar el emoji específico
            List<int> randomPositions = Enumerable.Range(0, cadenaEmoji.Length).OrderBy(x => random.Next()).Take(numeroVeces).ToList();

            // Agrega el emoji específico en las posiciones aleatorias
            foreach (int position in randomPositions)
            {
                cadenaEmoji.Insert(position, emojiSeleccionado);
            }

        }

        #endregion

    }
}
