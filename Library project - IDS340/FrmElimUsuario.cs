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
    public partial class FrmElimUsuario : Form
    {
        SqlCommand cmd;
        Connection connection;
        public FrmElimUsuario()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtEliminar_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                connection.OpenConnection();
                cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @id", connection.connection);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar)).Value = txtEliminar.Text;
                cmd.ExecuteNonQuery();
                connection.CloseConnection();
                MessageBox.Show("Usuario eliminado correctamente", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
            catch (Exception)
            {
                MessageBox.Show("El usuario no se ha podido eliminar", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
    }
}
