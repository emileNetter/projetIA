using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classification3._2
{
    public partial class Form1 : Form
    {
        public static Random random;
        static List<Observation> lobs = new List<Observation>();
        private Graphics g;	// Objet graphique placé en global
        private Bitmap bmp;
        private Pen pen;		// Crayon placé en global
        private int nbcol;      // nb de colonnes de la carte de Kohonen
        private int nblignes;   // nb de lignes de la carte

        CarteAutoOrg CAO;
        public static List<Classe> listclasses = new List<Classe>();

        public Form1()
        {
            InitializeComponent();
            random = new Random();

        }
    }
}
