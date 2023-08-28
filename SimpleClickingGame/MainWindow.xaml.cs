﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace SimpleClickingGame
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Variables o componentes a declarar:

        DispatcherTimer temporizadorJuego = new DispatcherTimer(); // Se crea una nueva instancia del tiempo del despachador llamada temporizadorJuego.
        List<Ellipse> eliminarElipses = new List<Ellipse>(); // Se crea una lista de elipses llamada eliminarElipses, ya que con esto se usará para eliminar del juego los objetos circulares en los que hagamos clic en ellos.

        // A continuación van a estar presentes todos los números enteros necesarios declarados para este juego.

        int indiceAparicion = 60; // Este es el índice de aparición predeterminado para cada círculo o elipse.
        int indiceActual; // El índice actual ayudará a agregar un intervalo entre el desove de los círculos.
        int puntuacionFinal = 0; // Esto contendrá el puntaje final logrado para este juego.
        int saludActual = 300; // Salud total del jugador al momento de inicializar el juego.
        int posicionX; // Posición en x para cada objeto circular.
        int posicionY; // Posición en y para cada objeto circular.
        int puntuacion = 0; // Puntuación actual del juego.

        double indiceCrecimiento = 0.6; // El índice de crecimiento predeterminado para este juego.

        Random numeroAleatorio = new Random(); // Variable numérica aleatoria.

        // A continuación se muestran las dos clases de reproductores multimedia, una para un efecto de sonido al hacer clic y otro efecto para el sonido pop.

        MediaPlayer hacerClick = new MediaPlayer();
        MediaPlayer popEfecto = new MediaPlayer();

        // A continuación se encuentran los dos buscadores de ubicación URI para los dos archivos mp3 que importamos para este juego.

        Uri ClickedSound;
        Uri PopSound;

        // Se van agregando colores para cada círculo con un nuevo pincel.

        Brush nuevoPincel;

        public MainWindow()
        { 
            InitializeComponent();

            // Dentro del constructor principal de esta clase se establecerá un algoritmo para el comienzo del juego.

            temporizadorJuego.Tick += GameLoop; // Se configura el evento del temporizador del juego llamado "GameLoop".
            temporizadorJuego.Interval = TimeSpan.FromMilliseconds(20); // Con este tiempo marcará cada 20 milisegundos.
            temporizadorJuego.Start(); // Inicia el temporizador.

            indiceActual = indiceAparicion; // Ajusta al índice actual con el valor del índice de aparición.

            // Se ubican ambos archivos mp3 dentro de la carpeta "sonidos" y agregarlos al URI correcto a continuación...

            ClickedSound = new Uri("pack://siteoforigin:,,,/sonidos/clickedpop.mp3");
            PopSound = new Uri("pack://siteoforigin:,,,/sonidos/pop.mp3");
        }

        // Dentro del método anterior, le agregaremos otro método para probar la ejecución del juego.

        private void GameLoop(object sender, EventArgs e)
        {
            // EN INSTANTES...
        }

        // Método que permite clickear un elemento dentro del Canvas.
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            // Con este evento está vinculado dentro del Canvas, pues debemos verificar si hemos hecho clic en la elipse o no.

            // Si su recurso original clickeado es una elipse.

            if (e.OriginalSource is Ellipse)
            {
                // Se crea un elipse local y se enlaza hacia su recurso original.

                Ellipse circulo = (Ellipse)e.OriginalSource;

                // Ahora desaparece esta elipse cuando está clickeado dentro del Canvas.

                MyCanvas.Children.Remove(circulo);

                // Incrementa en 1 a la puntuación.

                puntuacion++;

                // Carga cada uri del sonido en el que se hizo clic al reproducir y se reproduce el archivo de sonido.

                hacerClick.Open(ClickedSound);
                hacerClick.Play();
            }
        }

        // Método que llama al usuario a finalizar la partida cuando gana o pierde.

        private void gameOverFunction()
        {
            // EN INSTANTES...
        }
    }
}
