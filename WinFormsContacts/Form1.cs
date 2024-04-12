using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class Form1 : Form
    {
        private BussinessLogicLayer _bussinessLogicLayer;
        public Form1()
        {
            InitializeComponent();
            _bussinessLogicLayer = new BussinessLogicLayer();
        }

        #region Events
        // Manejador de eventos
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetail contactDetail = new ContactDetail();
                contactDetail.LoadContact(new Contact
                {
                    Id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString()
                });
                contactDetail.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
        }
        #endregion

        #region Private Methods
        private void OpenContactDetailDialog()
        {
            ContactDetail contactDetail = new ContactDetail();
            contactDetail.ShowDialog(this);
        }

        private void DeleteContact(int id)
        {
            _bussinessLogicLayer.DeleteContact(id);
        }
        #endregion

        #region Public Methos
        
        // Si el valor es nulo significa que el parametro es opcional
        public void PopulateContacts(string searchText = null)
        {
            List<Contact> contacts = _bussinessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }


        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
        }
    }
}
