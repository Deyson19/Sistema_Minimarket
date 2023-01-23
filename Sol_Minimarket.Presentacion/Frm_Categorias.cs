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
        #region Variables 
        int estadoGuarda = 0;//no hace nada el sistema
        int _codigo_ca = 0;
        string _descripcion_ca;

        #endregion
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

        private void Estado_botones_principales(bool lEstado)
        {
            this.btnNuevo.Enabled = lEstado;
            this.btnNuevo.ForeColor = Color.Gray;
            this.btnNuevo.Font = new Font(btnNuevo.Font,FontStyle.Regular);
            this.btnNuevo.Font = new Font(btnNuevo.Font,FontStyle.Italic);
            
            
            this.btnActualizar.Enabled = lEstado;
            this.btnActualizar.ForeColor = Color.Gray;
            this.btnActualizar.Font = new Font(btnActualizar.Font, FontStyle.Regular);
            this.btnActualizar.Font = new Font(btnActualizar.Font, FontStyle.Italic);

            this.btnEliminar.Enabled = lEstado;
            this.btnEliminar.ForeColor = Color.Gray;
            this.btnEliminar.Font = new Font(btnEliminar.Font, FontStyle.Regular);
            this.btnEliminar.Font = new Font(btnEliminar.Font, FontStyle.Italic);

            this.btnReporte.Enabled = lEstado;
            this.btnReporte.ForeColor = Color.Gray;
            this.btnReporte.Font = new Font(btnReporte.Font, FontStyle.Regular);
            this.btnReporte.Font = new Font(btnReporte.Font, FontStyle.Italic);

            this.btnSalir.Enabled = lEstado;
            this.btnSalir.ForeColor = Color.Gray;
            this.btnSalir.Font = new Font(btnSalir.Font, FontStyle.Regular);
            this.btnSalir.Font = new Font(btnSalir.Font, FontStyle.Italic);
        }

        private void Estado_botones_procesos(bool lEstado)
        {
            this.btnCancelar.Visible = lEstado;
            this.btnGuardar.Visible = lEstado;
            this.btnRetornar.Visible = !lEstado;
        }
        
        private void Selecciona_item()
        {
            string datoGrilla = Convert.ToString(dgv_Principal.CurrentRow.Cells["codigo_ca"].Value);

            if (string.IsNullOrEmpty(datoGrilla))
            {
                MessageBox.Show("No hay información para mostrar","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                this._descripcion_ca = (dgv_Principal.CurrentRow.Cells["descripcion_ca"].Value).ToString();
                this._codigo_ca = (int) dgv_Principal.CurrentRow.Cells["codigo_ca"].Value;
                txtDescripcion_ca.Text = Convert.ToString(dgv_Principal.CurrentRow.Cells["descripcion_ca"].Value);
            }
        }
        
        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.Listado_ca("%");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion_ca.Text == string.Empty)
            {
                ErrorProvider error_guardado = new ErrorProvider();
                error_guardado.SetError(txtDescripcion_ca, "Debes ingresar los datos requeridos.");
                txtDescripcion_ca.Focus();
                lblCategoria.ForeColor= Color.Red;
            }
            else
            {
                E_Categorias oCa = new E_Categorias();
                string respuesta =string.Empty;
                oCa.Codigo_ca = this._codigo_ca;
                oCa.Descripcion_ca = txtDescripcion_ca.Text.Trim();
                respuesta = N_Categorias.Guardar_ca(estadoGuarda,oCa);


                if(respuesta == "Ok")
                {
                    this.Listado_ca("%");
                    MessageBox.Show("Los datos se guardaron satisfactoriamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    estadoGuarda = 0;
                    this.Estado_botones_principales(true);
                    this.Estado_botones_procesos(true);
                    txtDescripcion_ca.Text = string.Empty;
                    txtDescripcion_ca.ReadOnly= true; 
                    Tbp_Principal.SelectedIndex= 0;

                    this._codigo_ca = 0;
                }
                else
                {
                    MessageBox.Show(respuesta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            estadoGuarda = 1; //Nuevo registro
            this.Estado_botones_principales(false);
            this.Estado_botones_procesos(true);
            txtDescripcion_ca.Text = string.Empty;
            txtDescripcion_ca.ReadOnly = false;
            Tbp_Principal.SelectedIndex = 1;
            txtDescripcion_ca.Focus() ;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            estadoGuarda = 2; //Actualiza registro
            this.Estado_botones_principales(false);
            this.Estado_botones_procesos(true);
            this.Selecciona_item();
            Tbp_Principal.SelectedIndex = 1;
            txtDescripcion_ca.ReadOnly = false;
            txtDescripcion_ca.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estadoGuarda = 0;
            txtDescripcion_ca.Text= string.Empty;
            txtDescripcion_ca.ReadOnly = true;
            this.Estado_botones_principales(true);
            this.Estado_botones_procesos(false);


            Tbp_Principal.SelectedIndex = 0;
        }

        private void dgv_Principal_DoubleClick(object sender, EventArgs e)
        {
            this.Selecciona_item();
            this.Estado_botones_procesos(false);
            Tbp_Principal.SelectedIndex = 1;
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            this.Estado_botones_procesos(false);
            Tbp_Principal.SelectedIndex = 0;
        }
    }
}
