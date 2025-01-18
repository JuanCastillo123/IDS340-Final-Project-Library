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
    public partial class FrmEliminar : Form
    {
        SqlCommand cmd;
        Connection connection;
        public FrmEliminar()
        {
            InitializeComponent();
            connection = new Connection();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                connection.OpenConnection();
                cmd = new SqlCommand("DELETE FROM Books WHERE ISBN = @isbn", connection.connection);
                cmd.Parameters.Add(new SqlParameter("@isbn", SqlDbType.VarChar)).Value = txtEliminar.Text;
                cmd.ExecuteNonQuery();
                connection.CloseConnection();
                MessageBox.Show("Libro eliminado correctamente", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
            catch (Exception)
            {
                MessageBox.Show("El libro no se ha podido eliminar", "Libreria App", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
    }
}
