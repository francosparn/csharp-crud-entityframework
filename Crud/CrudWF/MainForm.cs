using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudWF.Models;
using CrudWF.Presentation;

namespace CrudWF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Refresh();
            hideAlert();
        }

        #region HELPER
        private void Refresh(string search = null)
        {
            // Make connection to the DB
            using (crudEntities db = new crudEntities())
            {
                var list = from d in db.clients
                           select d;

                if (search != null && search != "")
                {
                    list = list.Where(d => d.name == search);
                    hideAlert();
                }

                if (search == "" || db.clients.Any(d => d.name != search))
                {
                    lblMessage.Visible = true;
                }


                if(search == "" || db.clients.Any(d=> d.name == search)){
                    hideAlert();
                }

                // Send data from DB to dataGridView element
                gridClients.DataSource = list.ToList();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(gridClients.Rows[gridClients.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Presentation.RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
            Refresh();
            hideAlert();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int? id = GetId();

            if (id != null)
            {
                Presentation.RegisterForm registerForm = new Presentation.RegisterForm(id);
                registerForm.ShowDialog();
                Refresh();
                lblMessage.Visible = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar al cliente?", "Eliminar Cliente", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int? id = GetId();

                if (id != null)
                {
                    using (crudEntities db = new crudEntities())
                    {
                        clients c = db.clients.Find(id);
                        db.clients.Remove(c);
                        db.SaveChanges();
                    }
                    MessageBox.Show("El cliente ha sido eliminado correctamente");
                    Refresh();
                    hideAlert();
                }
            }
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Refresh(txtSearch.Text.Trim());
        }

        private void hideAlert()
        {
            lblMessage.Visible = false;
        }
    }
}
