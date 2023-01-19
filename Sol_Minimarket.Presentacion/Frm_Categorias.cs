using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sol_Minimarket.Entidades;
using Sol_Minimarket.Negocio;

namespace Sol_Minimarket.Presentacion
{
    public partial class Frm_Categorias : Form
    {
        public Frm_Categorias()
        {
            InitializeComponent();
        }

        #region MisMetodos
        private void Formato_ca()
        {
            dgv_Principal.Columns[0].Width= 200;
            dgv_Principal.Columns[0].HeaderText = "CÓDIGO CATEGORÍA";
            dgv_Principal.Columns[1].Width= 300;
            dgv_Principal.Columns[1].HeaderText = "DESCRIPCIÓN CATEGORIA";
        }

        private void Listado_ca(string cTexto)
        {
            try
            {
                dgv_Principal.DataSource = N_Categorias.Listado_ca(cTexto);
                this.Formato_ca();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hay un error consultando: " + e.Message + e.StackTrace);
                throw e;
            }
        }
        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.Listado_ca("%");
        }
    }
}
