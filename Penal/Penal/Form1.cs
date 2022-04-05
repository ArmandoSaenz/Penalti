using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AN;
namespace Penal
{
    public partial class Form1 : Form
    {
        double[] t, vx, vy, x, y;
        double g = 9.81d;
        int x0 = 19, y0 = 420;

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            pbBalon.Location = new Point(x0, y0);
            dgvDatos.Rows.Clear();
        }

        double xb, yb;
        public Form1()
        {
            InitializeComponent();
        }

        private void MostrarDatos()
        {
            dgvDatos.Rows.Clear();
            dgvDatos.Rows.Add(100);
            foreach(DataGridViewRow arg in dgvDatos.Rows)
            {
                arg.Cells[0].Value = t[arg.Index];
                arg.Cells[1].Value = vx[arg.Index];
                arg.Cells[2].Value = vy[arg.Index];
                arg.Cells[3].Value = x[arg.Index];
                arg.Cells[4].Value = y[arg.Index];
            }
        }
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            double v0 = double.Parse(tbV0.Text), ang = double.Parse(tbAngulo.Text);
            t = new double[100];
            vx = new double[100];
            vy = new double[100];
            for (int i = 0; i < 100; i++)
            {
                t[i] = i * 0.1d;
                vx[i] = v0 * Math.Cos(ang);
                vy[i] = v0 * Math.Sin(ang) - t[i]*g;
            }
            x = Calculo.Integrar(t, vx);
            y = Calculo.Integrar(t, vy);
            MostrarDatos();
            foreach(DataGridViewRow row in dgvDatos.Rows)
            {
                xb = (double)row.Cells[3].Value;
                yb = (double)row.Cells[4].Value;
                if (yb < 0)
                    break;
                pbBalon.Location = new Point(x0 + (int)(xb*45.45), y0 - (int)(yb*45.45));
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
