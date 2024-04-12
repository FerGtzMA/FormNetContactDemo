using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    public class Contact
    {
        // Si escribimos 'prop' y despues tabulador, nos crea una propiedad
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
    }
}
