using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalRampa : Señal
    {
        double Amplitud { get; set; }
        double Fase { get; set; }
        double Frecuencia { get; set; }

      

        public SeñalRampa()
        {
           
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = tiempo;
            if (resultado < 0)
            {
                resultado = 0;
            }
            return resultado;
        }
    }
}
