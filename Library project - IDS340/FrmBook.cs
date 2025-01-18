using Library.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_project___IDS340
{
    public partial class FrmBook : Form
    {   
        SqlCommand cmd;
        Connection connection;
        public FrmBook()
        {
            InitializeComponent();
            connection = new Connection();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(txtBuscar.Text == "")
            {
                showBooks();
            }
            else
            {
                try
                {
                    connection.OpenConnection();
                    cmd = new SqlCommand("SELECT * FROM Books WHERE ISBN = @isbn", connection.connection);
                    cmd.Parameters.Add(new SqlParameter("@isbn", SqlDbType.VarChar)).Value = txtBuscar.Text;

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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            FrmEliminar frm1 = new FrmEliminar();
            frm1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {          
            try
            {
                
                cmd = new SqlCommand("Insert_Book", connection.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.OpenConnection();

                cmd.Parameters.AddWithValue("@isbn", txtISBN.Text);
                cmd.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@autor", txtAutor.Text);
                cmd.Parameters.AddWithValue("@editorial", txtEditorial.Text);
                cmd.Parameters.AddWithValue("@fechapublicacion", txtPublicacion.Text);
                cmd.Parameters.AddWithValue("@genero", txtGenero.Text);
                cmd.Parameters.AddWithValue("@copias", txtCopias.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Libro insertado correctamente.", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.CloseConnection();
                Limpiar();
            }
            catch (Exception)
            {
                MessageBox.Show("Libro no insertado", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        public void showBooks()
        {
            cmd = new SqlCommand("SELECT * FROM Books", connection.connection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Id"].Visible = false;

            }
        }

        public void FrmBook_Load(object sender, EventArgs e)
        {
            showBooks();
            btnEditar.Visible = false;
            btnEditar.Enabled = false;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnInsertar.Visible = false;
            btnInsertar.Enabled = false;
            txtISBN.Enabled = false;

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
              
            txtISBN.Text = selectedRow.Cells["ISBN"].Value?.ToString() ?? string.Empty;
            txtTitulo.Text = selectedRow.Cells["Titulo"].Value?.ToString() ?? string.Empty;
            txtAutor.Text = selectedRow.Cells["Autor"].Value?.ToString() ?? string.Empty;
            txtEditorial.Text = selectedRow.Cells["Editorial"].Value?.ToString() ?? string.Empty;
            txtPublicacion.Text = selectedRow.Cells["Fecha_publicacion"].Value?.ToString() ?? string.Empty;
            txtGenero.Text = selectedRow.Cells["Genero"].Value?.ToString() ?? string.Empty;
            txtCopias.Text = selectedRow.Cells["Copias"].Value?.ToString() ?? string.Empty;

            tabControl1.SelectedTab = tabPage2;

            btnEditar.Visible = true;
            btnEditar.Enabled = true;
           
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            connection.OpenConnection();

            string query = @"UPDATE Books SET 
            Titulo = @Titulo,
            Autor = @Autor,
            Editorial = @Editorial,
            Fecha_publicacion = @Fecha_publicacion,
            Genero = @Genero,
            Copias = @Copias
            WHERE 
            ISBN = @ISBN";

            cmd = new SqlCommand(query, connection.connection);

            // Añadir los parámetros
            cmd.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar)).Value = txtISBN.Text;
            cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar)).Value = txtTitulo.Text;
            cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.VarChar)).Value = txtAutor.Text;
            cmd.Parameters.Add(new SqlParameter("@Editorial", SqlDbType.VarChar)).Value = txtEditorial.Text;
            cmd.Parameters.Add(new SqlParameter("@Fecha_publicacion", SqlDbType.VarChar)).Value = txtPublicacion.Text;
            cmd.Parameters.Add(new SqlParameter("@Genero", SqlDbType.VarChar)).Value = txtGenero.Text;
            cmd.Parameters.Add(new SqlParameter("@Copias", SqlDbType.VarChar)).Value = txtCopias.Text;
            cmd.ExecuteNonQuery();

            connection.CloseConnection();

            btnEditar.Enabled = false;
            btnEditar.Visible = false;
            btnInsertar.Visible = true;
            btnInsertar.Enabled = true;
            tabControl1.SelectedTab = tabPage1;
            FrmBook_Load(sender, e);
            Limpiar();
        }

        private void Limpiar()
        {
            txtISBN.Text = string.Empty;
            txtPublicacion.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtAutor.Text = string.Empty;
            txtEditorial.Text = string.Empty;
            txtCopias.Text = string.Empty;
            txtGenero.Text = string.Empty;
        }
    }
}
