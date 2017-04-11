using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        private double[,] inputs = new double[3000, 3];
        private double minInputs;
        private double maxInputs;

        CarteAutoOrg CAO;
        public static List<Classe> listclasses = new List<Classe>();

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            InitialiseInputs();

        }

        //Ecris les lignes du ficher dataset dans un tableau de 3000 lignes et 3 colonnes
        // Permutation des lignes de manière aléatoire
        // Normalisation des valeurs entre 0 et 1
        private void InitialiseInputs()
        {           
            using (StreamReader sr = new StreamReader("datasetclassif.txt"))
            {
                for (int i = 0; i < 3000; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Read the stream to a string, and write the string to the console.
                        inputs[i, j] = Convert.ToDouble(sr.ReadLine());
                        if (j != 0 && inputs[i, j] > maxInputs) maxInputs = inputs[i, j];
                        if (j != 0 && inputs[i, j] < minInputs) minInputs = inputs[i, j];
                    }
                }
                sr.Close();
                RandomizeInputs();
                NormaliserEntrees();
            }

        }

        private void RandomizeInputs()
        {
            random = new Random();
            int k = 0;
            foreach (int i in Enumerable.Range(0, 3000).OrderBy(x => random.Next()))
            {
                for (int j = 0; j < 3; j++)
                {
                    inputs[k, j] = inputs[i, j];
                }
                k++;
            }
        }

        public void NormaliserEntrees()
        {
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                for (int j = 1; j < inputs.GetLength(1); j++)
                {
                    inputs[i, j] = inputs[i, j].Remap(minInputs, maxInputs, 0, 1);
                }
            }

        }

        private void InitialiseObs()
        {

        }

        private void button_initialise_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;
            pen = new Pen(Color.White, 1);
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            pictureBox1.Refresh();
        }
    }

    public static class ExtensionMethods
    {
        // Map des valeurs d'un interval sur un autre
        public static double Remap(this double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }
}
