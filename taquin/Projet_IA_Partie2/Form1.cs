using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_IA_Partie2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lancer_simulation_Click(object sender, EventArgs e)
        {
            int nb = Convert.ToInt32( numericUpDown1.Value);
            Perceptron p = new Perceptron();
            p.Traitement(nb);
            textBoxX.Text = p.GetPoids(0).ToString();
            textBoxY.Text = p.GetPoids(1).ToString();
            tb_nbErreurs.Text = p.nbErreurs.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
