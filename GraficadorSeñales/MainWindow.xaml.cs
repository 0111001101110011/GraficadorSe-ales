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

            /*plnGrafica.Points.Add(new Point(0, 10));
            plnGrafica.Points.Add(new Point(50, 20));
            plnGrafica.Points.Add(new Point(150, 10));
            plnGrafica.Points.Add(new Point(200, 50));
            plnGrafica.Points.Add(new Point(250, 0));
            plnGrafica.Points.Add(new Point(300, 100));
            plnGrafica.Points.Add(new Point(350, 30));
            plnGrafica.Points.Add(new Point(450, 50));
            plnGrafica.Points.Add(new Point(550, 100));
            plnGrafica.Points.Add(new Point(650, 10));
            plnGrafica.Points.Add(new Point(750, 25));
            plnGrafica.Points.Add(new Point(850, 120));
            plnGrafica.Points.Add(new Point(950, 30));
            plnGrafica.Points.Add(new Point(1050, 54));*/


            /*
            SeñalSenoidal señal = new SeñalSenoidal();

            for (double i = 0; i <= 1; i += 0.0001)
            {
                Console.WriteLine(señal.evaluar(i));
            }
            Console.ReadLine();
            */
        }

        private void BotonGraficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txt_Amplitud.Text);
            double fase = double.Parse(txt_Fase.Text);
            double frecuencia = double.Parse(txt_Frecuencia.Text);
            double tiempoInicial = double.Parse(txt_TiempoInicial.Text);
            double tiempoFinal = double.Parse(txt_TiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txt_FrecuenciaDeMuestreo.Text);

            SeñalSenoidal señal = new SeñalSenoidal(amplitud, fase, frecuencia);

            plnGrafica.Points.Clear();

            double periodoMuestreo = 1 / frecuenciaMuestreo;

            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = señal.evaluar(i);

                if (Math.Abs(valorMuestra) > señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima = Math.Abs(valorMuestra);
                }

                señal.Muestras.Add(new Muestra(i, valorMuestra));
            }

            // Sirve para recorrer una coleccion o arreglo
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(new Point((muestra.X - tiempoInicial) * scrContenedor.Width, (muestra.Y / señal.AmplitudMaxima * ((scrContenedor.Height / 2) - 30) * -1 + (scrContenedor.Height / 2))));
            }

            plnEjeX.Points.Clear();
              //Punto del principio

            //Eje X
            plnEjeX.Points.Add(new Point(0, //Coordenada X Punto Inicial 
                (scrContenedor.Height / 2)));  //Coordenada Y Punto Inicial
            plnEjeX.Points.Add(new Point((tiempoFinal - tiempoInicial) * scrContenedor.Width // X Final
                ,(scrContenedor.Height / 2)));                                               // Y Final

            //Eje Y
            plnEjeY.Points.Clear();
            plnEjeY.Points.Add(new Point((0 - tiempoInicial) * scrContenedor.Width,  
               (-señal.AmplitudMaxima* ((scrContenedor.Height / 2) - 30) * -1 + (scrContenedor.Height / 2)));

            plnEjeY.Points.Add(new Point(0-tiempoInicial) * scrContenedor.Width, 
                (-1 *((scrContenedor.Height / 2.0)-30) *-1 ) + (scrContenedor.Height)/2)));


            lbl_AmplitudMaxima.Text = señal.AmplitudMaxima.ToString();
            lbl_AmplitudMinima.Text = "-" + señal.AmplitudMaxima.ToString();
        }

        private void BotonGraficar_Rampa_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txt_TiempoInicial.Text);
            double tiempoFinal = double.Parse(txt_TiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txt_FrecuenciaDeMuestreo.Text);

            SeñalRampa señal = new SeñalRampa();

            plnGrafica.Points.Clear();

            double periodoMuestreo = 1 / frecuenciaMuestreo;
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = señal.evaluar(i);

                if (Math.Abs(valorMuestra) > señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima = Math.Abs(valorMuestra);
                }

                señal.Muestras.Add(new Muestra(i, valorMuestra));
            }

            // Sirve para recorrer una coleccion o arreglo
            foreach (Muestra muestra in señal.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * scrContenedor.Width, (muestra.Y * ((scrContenedor.Height / 2) - 30) * -1 + (scrContenedor.Height / 2))));
            }

            lbl_AmplitudMaxima.Text = señal.AmplitudMaxima.ToString();
            lbl_AmplitudMinima.Text = "-" + señal.AmplitudMaxima.ToString();

        }
    }
}