using System;
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
            // Este es el evento de bucle del juego, todas las instrucciones dentro de este evento se ejecutarán cada vez que el temporizador avance.

            // Primero se actualiza la puntuación y luego se muestra la puntuación final en uno de estos campos de texto.

            txtPuntuacion.Content = "Puntuación: " + puntuacion; // Puntuación actual del juego.
            txtPuntuacionFinal.Content = "Puntuación final: " + puntuacionFinal; // Puntuación final del juego.

            // Reduce a 2 el índice actual de cada círculo o elipse mientras el tiempo se acelera.

            indiceActual -= 2;

            // Si su índice actual es menor que 1.

            if (indiceActual < 1)
            {
                // Reinicia el valor actual de su índice para regresar a la de su aparición.

                indiceActual = indiceAparicion;

                // Genera un número aleatorio para los valores de los círculos mediante su posición en X e Y.

                posicionX = numeroAleatorio.Next(15, 700);
                posicionY = numeroAleatorio.Next(50, 350);

                // Genera colores aleatorios para cada círculo y lo guardan dentro del pincel.

                nuevoPincel = new SolidColorBrush(Color.FromRgb((byte)numeroAleatorio.Next(1, 255), (byte)numeroAleatorio.Next(1, 255), (byte)numeroAleatorio.Next(1, 255)));

                // Se crea una elipse llamado círculo.
                // Con este círculo tendrá una etiqueta, altura y ancho por defecto, color de borde y su relleno.

                Ellipse circulo = new Ellipse
                {
                    Tag = "circulo",
                    Height = 10,
                    Width = 10,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Fill = nuevoPincel
                };

                // Se coloca el círculo recién creado en el Canvas con la posición X e Y generada anteriormente.

                Canvas.SetLeft(circulo, posicionX);
                Canvas.SetTop(circulo, posicionY);

                // Finalmente, se agrega un nuevo círculo dentro del Canvas.

                MyCanvas.Children.Add(circulo);
            }
            
            // El bucle "foreach" por debajo encontrará para cada elipse dentro del Canvas y la hará incrementar su tamaño.

            foreach (var x in MyCanvas.Children.OfType<Ellipse>())
            {
                // Se busca en el Canvas y encuentra la elipse cuando existe dentro de ella.

                x.Height += indiceCrecimiento; // Incrementa el valor de su altura del círculo.
                x.Width += indiceCrecimiento; // Incrementa el valor de su ancho del círculo.
                x.RenderTransformOrigin = new Point(0.5, 0.5); // Incrementa para el centro del círculo su valor cuando reintenta su transformación original.

                // Si el valor de su ancho del círculo es mayor que 70 entonces se revienta por sí sola.

                    if (Width > 70)
                    {
                        // Si su ancho es mayor que 70 entonces se genera este mismo círculo y lo elimina dentro de esta lista.

                        eliminarElipses.Add(x);
                        saludActual -= 15; // Reduce su salud actual en 15.
                        popEfecto.Open(PopSound); // Carga esta Uri dentro del efecto de estallido.
                        popEfecto.Play(); // Reproduce este efecto.
                    }

            } // Fin de cada bucle.

            // Si su salud actual es mayor que 1.

            if (saludActual > 1)
            {
               // Se enlaza a la barra del rectángulo de la salud con su valor en enteros.

               salud.Width = saludActual;
            }
            else
            {
               // Si su salud es menor que 1 o igual a 0 entonces llamaremos al método de finalizar el juego.

               gameOverFunction();
            }

            // Para eliminar una elipse dentro del juego creamos otro bucle "foreach".

            foreach (Ellipse i in eliminarElipses)
            {
                // Con este bucle buscaremos para cada elipse cuando existe dentro del Canvas para luego eliminarlo dentro de esta lista.

                MyCanvas.Children.Remove(i); // Si encuentra una elipse y lo elimina dentro de ella.
            }

            // Si su puntuación es mayor que 5.

            if (puntuacion > 5)
            {
                // Incrementa la velocidad de su índice al aparecer cada círculo.

                indiceAparicion = 25;
            }

            // Si su puntuación es mayor que 20.

            if (puntuacion > 20)
            {
                // Se aplicará la misma situación anteriormente pero crecerá más rápido.

                indiceAparicion = 15;
                indiceCrecimiento = 1.5;
            }
        }
    
        // Método que permite clickear un elemento dentro del Canvas.
        private void ClickOnCanvas(object sender, MouseButtonEventArgs e)
        {
            // Con este evento está vinculado dentro del Canvas, pues debemos verificar si hemos hecho clic en la elipse o no.

            // Si su recurso original cliqueado es una elipse.

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
            // Esta es la función de finalizar el juego.

            temporizadorJuego.Stop(); // Paraliza el temporizador por primera vez.

            // Se muestra un mensaje de ventana al final de la pantalla y espera al jugador para darle un consejo sorpresa XD.

            MessageBox.Show("Fin del juego" + Environment.NewLine + "Usted obtuvo: " + puntuacion + Environment.NewLine + "Ya Po, Compadre Poh!");

            // Después de que el jugador haya visto esta penitencia tenemos que hacer algo respecto a este bucle ("foreach").

            foreach (var y in MyCanvas.Children.OfType<Ellipse>())
            {
                // Se busca todas las elipses existentes que están en la pantalla y luego las agrega a esta lista.

                eliminarElipses.Add(y);
            }

            // Aquí necesitaremos otro bucle "foreach" para eliminar todo lo que hay dentro de eliminar en esta lista.

            foreach (Ellipse i in eliminarElipses)
            {
                MyCanvas.Children.Remove(i);
            }

            // Se reestablecen todos los valores del juego a la de los predeterminados, incluida la eliminación de todas las elipses de eliminar esta lista.

            indiceCrecimiento = .6;
            indiceAparicion = 60;
            puntuacionFinal = puntuacion;
            puntuacion = 0;
            indiceActual = 5;
            saludActual = 300;
            eliminarElipses.Clear();
            temporizadorJuego.Start();

        }
    }
}
