using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalRampa
    {
        double Amplitud { get; set; }
        double Fase { get; set; }
        double Frecuencia { get; set; }

        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }

        public SeñalRampa()
        {
            Amplitud = 1;
            Fase = 0;
            Frecuencia = 1;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        public double evaluar(double tiempo)
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
