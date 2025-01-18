using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_project___IDS340
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void librosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBook frm1 = new FrmBook();
            frm1.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios frm2 = new FrmUsuarios();
            frm2.Show();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReservas frm3= new FrmReservas();
            frm3.Show();
        }
    }
}
