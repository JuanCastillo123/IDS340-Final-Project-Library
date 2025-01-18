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
using Library.Data;

namespace Library_project___IDS340
{
    public partial class FrmReservas : Form
    {
        SqlCommand cmd;
        Connection connection;
        public FrmReservas()
        {
            InitializeComponent();
            connection = new Connection();
        }


        public void FrmReservas_Load(object sender, EventArgs e)
        {
            showReservations();
           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public void showReservations()
        {
            cmd = new SqlCommand("SELECT * FROM Reservations", connection.connection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
               

            }


        }

       
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                showReservations();
            }
            else
            {
                try
                {
                    connection.OpenConnection();
                    cmd = new SqlCommand("SELECT * FROM Reservations WHERE Id = @id", connection.connection);
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
        private void Limpiar()
        {
            txtID.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtLibroreservado.Text = string.Empty;
            txtFecharetorno.Text = string.Empty;

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {

                cmd = new SqlCommand("Insert_Reservation", connection.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.OpenConnection();

                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@libro", txtLibroreservado.Text);
                cmd.Parameters.AddWithValue("@fecharetorno", txtFecharetorno.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Libro reservado correctamente.", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.CloseConnection();
                Limpiar();
            }
            catch (Exception)
            {
                MessageBox.Show("Libro no reservado", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            FrmElimReservas frm1 = new FrmElimReservas();
            frm1.Show();
        }
    }
}
