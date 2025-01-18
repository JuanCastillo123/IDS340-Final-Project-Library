using Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_project___IDS340
{
    public partial class FrmUsuarios : Form
    {
        SqlCommand cmd;
        Connection connection;
        public FrmUsuarios()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            FrmElimUsuario frm1 = new FrmElimUsuario();
            frm1.Show();


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                showUsuario();
            }
            else
            {
                try
                {
                    connection.OpenConnection();
                    cmd = new SqlCommand("SELECT * FROM Usuario WHERE Id = @id", connection.connection);
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = txtBuscar.Text;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Vincular el DataTable al DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void showUsuario()
        {
            cmd = new SqlCommand("SELECT * FROM Usuario", connection.connection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

            }
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            showUsuario();
            btnEditar.Visible = false;
            btnEditar.Enabled = false;
        }
        private void Limpiar()
        {
            txtID.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {

            try
            {

                cmd = new SqlCommand("Insert_Usuario", connection.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.OpenConnection();

                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuario agregado correctamente.", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.CloseConnection();
                Limpiar();
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario no agregado", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnInsertar.Visible = false;
            btnInsertar.Enabled = false;
            txtID.Enabled = false;

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            txtID.Text = selectedRow.Cells["ID"].Value?.ToString() ?? string.Empty;
            txtNombre.Text = selectedRow.Cells["Nombre"].Value?.ToString() ?? string.Empty;
            txtApellido.Text = selectedRow.Cells["Apellido"].Value?.ToString() ?? string.Empty;
            txtEmail.Text = selectedRow.Cells["Email"].Value?.ToString() ?? string.Empty;
            txtTelefono.Text = selectedRow.Cells["Telefono"].Value?.ToString() ?? string.Empty;
          
            tabControl1.SelectedTab = tabPage2;

            btnEditar.Visible = true;
            btnEditar.Enabled = true;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            connection.OpenConnection();

            string query = @"UPDATE Usuario SET 
            Nombre = @Nombre,
            Apellido = @Apellido,
            Email = @Email,
            Telefono = @Telefono
            
            WHERE 
            Id = @Id";

            cmd = new SqlCommand(query, connection.connection);

            // Añadir los parámetros
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.VarChar)).Value = txtID.Text;
            cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = txtNombre.Text;
            cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar)).Value = txtApellido.Text;
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = txtEmail.Text;
            cmd.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = txtTelefono.Text;
            cmd.ExecuteNonQuery();

            connection.CloseConnection();

            btnEditar.Enabled = false;
            btnEditar.Visible = false;
            btnInsertar.Visible = true;
            btnInsertar.Enabled = true;
            tabControl1.SelectedTab = tabPage1;
            FrmUsuarios_Load(sender, e);
            Limpiar();
        }
    }
}


          