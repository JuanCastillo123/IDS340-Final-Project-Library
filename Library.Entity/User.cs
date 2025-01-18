using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity
{
     public class User
     {
        public int Id { get; set; }
       
        public string Nombre { get; set; }
        public int Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

     }
}
