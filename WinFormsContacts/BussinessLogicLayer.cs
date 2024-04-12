using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    internal class BussinessLogicLayer
    {
        // Para acceder a la capa de datos
        private DataAccessLayer _dataAccessLayer;

        public BussinessLogicLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        public Contact SaveContact(Contact contact)
        {
            // Verificar si no existe el contacto
            if (contact.Id == 0)
            {
                _dataAccessLayer.InsertContact(contact);
            }
            else
            {
                _dataAccessLayer.UpdateContact(contact);
            }
            return contact;
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccessLayer.GetContacts(searchText);
        }

        public void DeleteContact(int id)
        {
            _dataAccessLayer.DeleteContact(id);
        }
    }
}
