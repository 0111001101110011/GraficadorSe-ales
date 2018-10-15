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

        private void BotonGraficar_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txt_TiempoInicial.Text);
            double tiempoFinal = double.Parse(txt_TiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txt_FrecuenciaDeMuestreo.Text);

            Señal señal;
            Señal segundaSeñal;

            switch (cb_TipoSeñal.SelectedIndex)
            {
                // Señal Senoidal
                case 0:
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txt_Amplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txt_Fase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txt_Frecuencia.Text);

                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;

                // Señal Rampa
                case 1:
                    señal = new SeñalRampa();
                    break;

                // Señal Exponencial
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)(panelConfiguracion.Children[0])).txt_Alpha.Text);
                    señal = new SenalExponencial(alpha);
                    break;

                default:
                    señal = null;
                    break;
            }

            //Segunda Señal

            switch (cb_TipoSeñal_2.SelectedIndex)
            {
                // Señal Senoidal
                case 0:
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_2.Children[0])).txt_Amplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_2.Children[0])).txt_Fase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion_2.Children[0])).txt_Frecuencia.Text);

                    segundaSeñal = new SeñalSenoidal(amplitud, fase, frecuencia);
                    break;

                // Señal Rampa
                case 1:
                    segundaSeñal = new SeñalRampa();
                    break;

                // Señal Exponencial
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)(panelConfiguracion_2.Children[0])).txt_Alpha.Text);
                    segundaSeñal = new SenalExponencial(alpha);
                    break;

                default:
                    segundaSeñal = null;
                    break;
            }

            señal.TiempoInicial = tiempoInicial;
            señal.TiempoFinal = tiempoFinal;
            señal.FrecuenciaMuestreo = frecuenciaMuestreo;

            //Segunda señal
            segundaSeñal.TiempoInicial = tiempoInicial;
            segundaSeñal.TiempoFinal = tiempoFinal;
            segundaSeñal.FrecuenciaMuestreo = frecuenciaMuestreo;

            señal.construirSeñalDigital();
            segundaSeñal.construirSeñalDigital();

            // Escalar
            if ((bool)ckb_Escala.IsChecked)
            {
                double factorEscala = double.Parse(txt_EscalaAmplitud.Text);
                señal.escalar(factorEscala);
            }

            // Desplazar
            if ((bool)ckb_Desplazamiento.IsChecked)
            {
                double factorDesplazamiento = double.Parse(txt_Desplazamiento.Text);
                señal.desplazar(factorDesplazamiento);
            }

            //Truncar 
            if ((bool) ckb_Truncar.IsChecked)
            {
                double n = double.Parse(txt_Truncar.Text);
                señal.Truncar(n);

            }


             // Escalar 2
            if ((bool)ckb_Escala_2.IsChecked)
            {
                double factorEscala_2 = double.Parse(txt_EscalaAmplitud_2.Text);
                señal.escalar(factorEscala_2);
            }

            // Desplazar 2
            if ((bool)ckb_Desplazamiento_2.IsChecked)
            {
                double factorDesplazamiento_2 = double.Parse(txt_Desplazamiento_2.Text);
                señal.desplazar(factorDesplazamiento_2);
            }

            //Truncar 2
            if ((bool) ckb_Truncar_2.IsChecked)
            {
                double n = double.Parse(txt_Truncar_2.Text);
                señal.Truncar(n);

            }

            // Actualizar 
            señal.actualizarAmplitudMaxima();

            plnGrafica.Points.Clear();

            if (señal != null)
            {
                // Sirve para recorrer una coleccion o arreglo
                foreach (Muestra muestra in señal.Muestras)
                {
                    plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, (muestra.Y / señal.AmplitudMaxima * ((scrContenedor.Height / 2) - 30) * -1 + (scrContenedor.Height / 2))));
                }

                lbl_AmplitudMaxima.Text = señal.AmplitudMaxima.ToString("F");
                lbl_AmplitudMinima.Text = "-" + señal.AmplitudMaxima.ToString("F");
            }

            // Línea del Eje X
            plnEjeX.Points.Clear();
            plnEjeX.Points.Add(new Point(0, scrContenedor.Height / 2));
            plnEjeX.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width, scrContenedor.Height / 2));

            // Línea del Eje Y
            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(new Point((-tiempoInicial) * scrContenedor.Width, 0));
            plnEjeY.Points.Add(new Point((-tiempoInicial) * scrContenedor.Width, scrContenedor.Height));
        }

        private void cb_TipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            switch (cb_TipoSeñal.SelectedIndex)
            {
                // Señal Senoidal
                case 0:
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;

                // Señal Rampa
                case 1:
                    break;
                default:
                    break;

                // Señal Senoidal
                case 2:
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                    break;
            }
        }

        private void cb_TipoSeñal_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            panelConfiguracion_2.Children.Clear();
            switch (cb_TipoSeñal.SelectedIndex)
            {
                // Señal Senoidal
                case 0:
                    panelConfiguracion_2.Children.Add(new ConfiguracionSeñalExponencial());
                    break;

                // Señal Rampa
                case 1:
                    break;
             
              
                // Señal Senoidal
                case 2:
                    panelConfiguracion_2.Children.Add(new ConfiguracionSeñalExponencial());
                    break;

                default:
                    break;
            }

        }
    }
}

