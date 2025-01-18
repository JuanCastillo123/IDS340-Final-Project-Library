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
    public partial class FrmElimReservas : Form
    {
        SqlCommand cmd;
        Connection connection;
        public FrmElimReservas()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                connection.OpenConnection();
                cmd = new SqlCommand("DELETE FROM Reservations WHERE Id = @id", connection.connection);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = txtEliminar.Text;
                cmd.ExecuteNonQuery();
                connection.CloseConnection();
                MessageBox.Show("Reservacion eliminada correctamente", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
            catch (Exception)
            {
                MessageBox.Show("La reservacion no se ha podido eliminar", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
    }
}
