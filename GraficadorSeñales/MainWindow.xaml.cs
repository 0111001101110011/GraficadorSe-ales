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

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btngraficar_click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txt_amplitud.Text);
            double fase = double.Parse(txt_fase.Text);
            double frecuencia = double.Parse(txt_frecuencia.Text);
            double tiempoinicial = double.Parse(txt_tiempoinicial.Text);
            double tiempofinal = double.Parse(txt_tiempofinal.Text);
            double frecuenciademuestreo = double.Parse(txt_frecuenciademuestreo.Text);

            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

            plnGrafica.Points.Clear();
            double periodomuestreo = 1 / frecuenciademuestreo;
            for (double i = tiempoinicial; i <= tiempofinal; i += periodomuestreo)
            {

                plnGrafica.Points.Add(new Point(i*contenedor.Width,señal.Evaluar(i)*((contenedor.Height/2)-30)*-1+(contenedor.Height/2))) ;


            }

        }
    }
}
