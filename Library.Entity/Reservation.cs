using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity
{
   public class Reservation

    {
        public int Id { get; set; }
        public string Usuario {  get; set; }
        public string Libro { get; set; }
        public string Fecha_reserva { get; set; }

       public string Fecha_retorno { get; set; }
        

    }
}
