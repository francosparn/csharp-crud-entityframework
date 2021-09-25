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

namespace CrudWF.Presentation
{
    public partial class RegisterForm : Form
    {
        public int? id;
        clients c = null;
        public RegisterForm(int? id=null)
        {
            InitializeComponent();
            this.id = id;

            if(id != null)
            {
                LoadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Save clients in the database
        private void btnSave_Click(object sender, EventArgs e)
        {
            ValidateForm();
            this.Close();
        }

        // Upload user data to registration form
        private void LoadData()
        {
            using (crudEntities db = new crudEntities())
            {
                c = db.clients.Find(id);
                txtName.Text = c.name;
                txtSurname.Text = c.surname;
                txtDni.Text = c.dni;
                txtPhone.Text = c.phone;
                txtEmail.Text = c.email;
            }
        }
        
        // Add clients to database
        private void AddClient()
        {
            using (crudEntities db = new crudEntities())
            {
                if(id == null)
                    c = new clients();

                c.name = txtName.Text;
                c.surname = txtSurname.Text;
                c.dni = txtDni.Text;
                c.phone = txtPhone.Text;
                c.email = txtEmail.Text;


                if (id == null)
                    db.clients.Add(c);
                else
                {
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        // Validate customer registration form fields
        private void ValidateForm()
        {
            if (txtName.Text.Trim() == "" || txtName.Text == "Ingrese el nombre" || 
                txtSurname.Text.Trim() == "" || txtSurname.Text == "Ingrese el apellido" ||
                txtDni.Text.Trim() == "" || txtDni.Text == "Ingrese el número de documento" ||
                txtPhone.Text.Trim() == "" || txtPhone.Text == "Ingrese el número de teléfono" ||
                txtEmail.Text.Trim() == "" || txtEmail.Text == "Ingrese el correo electrónico")
            {
                MessageBox.Show("Todos los campos del formulario son obligatorios");
            }
            else
            {
                MessageBox.Show("¡El cliente ha sido registrado exitosamente!");
                AddClient();
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if(txtName.Text == "Ingrese el nombre")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Gold;
                txtName.Font = new Font(txtName.Font, FontStyle.Bold);
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                txtName.Text = "Ingrese el nombre";
                txtName.ForeColor = Color.DimGray;
                txtName.Font = new Font(txtName.Font, FontStyle.Regular);
            }
        }

        private void txtSurname_Enter(object sender, EventArgs e)
        {
            if (txtSurname.Text == "Ingrese el apellido")
            {
                txtSurname.Text = "";
                txtSurname.ForeColor = Color.Gold;
                txtSurname.Font = new Font(txtSurname.Font, FontStyle.Bold);
            }
        }

        private void txtSurname_Leave(object sender, EventArgs e)
        {
            if (txtSurname.Text == "")
            {
                txtSurname.Text = "Ingrese el apellido";
                txtSurname.ForeColor = Color.DimGray;
                txtSurname.Font = new Font(txtSurname.Font, FontStyle.Regular);
            }
        }

        private void txtDni_Enter(object sender, EventArgs e)
        {
            if (txtDni.Text == "Ingrese el número de documento")
            {
                txtDni.Text = "";
                txtDni.ForeColor = Color.Gold;
                txtDni.Font = new Font(txtDni.Font, FontStyle.Bold);
            }
        }

        private void txtDni_Leave(object sender, EventArgs e)
        {
            if (txtDni.Text == "")
            {
                txtDni.Text = "Ingrese el número de documento";
                txtDni.ForeColor = Color.DimGray;
                txtDni.Font = new Font(txtDni.Font, FontStyle.Regular);
            }
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            if (txtPhone.Text == "Ingrese el número de teléfono")
            {
                txtPhone.Text = "";
                txtPhone.ForeColor = Color.Gold;
                txtPhone.Font = new Font(txtPhone.Font, FontStyle.Bold);
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text == "")
            {
                txtPhone.Text = "Ingrese el número de teléfono";
                txtPhone.ForeColor = Color.DimGray;
                txtPhone.Font = new Font(txtPhone.Font, FontStyle.Regular);
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Ingrese el correo electrónico")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Gold;
                txtEmail.Font = new Font(txtPhone.Font, FontStyle.Bold);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Ingrese el correo electrónico";
                txtEmail.ForeColor = Color.DimGray;
                txtEmail.Font = new Font(txtEmail.Font, FontStyle.Regular);
            }
        }

    }
}
