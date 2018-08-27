using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Muestra
    {
        //EL INSTANTE DEL TIEMPO EN QUE FUE TOMADA LA MUESTRA
        public double X { get; set; }
        //EL VALOR DE ESA MUESTRA EN ESE INSTANTE
        public double Y { get; set; }

        //CONSTRUCTOR QUE INICIALIZA VALORES
        public Muestra(double x, double y)
        {
            X = x;
            Y = y;
        }

        //CONSTRUCTOR SIN PARAMETROS 
        
        public Muestra()
        {
            X = 0.0;
            Y = 0.0;
        }

        

    }
}
