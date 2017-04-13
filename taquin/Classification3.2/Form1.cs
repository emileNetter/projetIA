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
        private int nbCol;     // nb de colonnes de la carte de Kohonen
        private int nbLignes;  // nb de lignes de la carte

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
                    inputs[i, j] = inputs[i, j].Remap(minInputs, maxInputs, 0, 799);
                }
            }

        }

        private void InitialiseObs()
        {
            lobs.Clear();
            for (int i =0; i < inputs.GetLength(0); i++)
            {
                lobs.Add(new Observation(inputs[i, 1], inputs[i, 2]));
            }
        }

        private void button_initialise_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;
            pen = new Pen(Color.White, 1);
            nbCol = Convert.ToInt32(textBox_colonnes.Text);
            nbLignes = Convert.ToInt32(textBox_lignes.Text);
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);

            // Initialisation de la liste d'Observations
            InitialiseObs();

            CAO = new CarteAutoOrg(nbLignes,nbCol,2,bmp.Width);
            AfficheDonnees();
            AfficheCarteSOM();

            pictureBox1.Refresh();
        }
        public void AfficheDonnees()
        {
            for (int i = 0; i < lobs.Count; i++)
            {
                bmp.SetPixel(Convert.ToInt32(lobs[i].Getx()), Convert.ToInt32(lobs[i].Gety()), Color.Red);
            }
        }

        private void AfficheCarteSOM()
        {

            int x, y;

            pen.Color = Color.Blue;
            for (int i = 0; i < nbCol; i++)
                for (int j = 0; j < nbLignes; j++)
                {
                    x = Convert.ToInt32(CAO.GetNeurone(i, j).GetPoids(0));
                    y = Convert.ToInt32(CAO.GetNeurone(i, j).GetPoids(1));
                    g.DrawEllipse(pen, x - 2, y - 2, 4, 4);

                }
            pictureBox1.Refresh();
        }

        private void button_algoKohonen_Click(object sender, EventArgs e)
        {
            CAO.AlgoKohonen(lobs, Convert.ToDouble(textBox_coeff.Text));
            pen.Color = Color.White;
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            AfficheDonnees();
            AfficheCarteSOM();
        }

        private void button_regroupement_Click(object sender, EventArgs e)
        {
            listclasses.Clear();
            // Regroupement pour obtenir 6 classes
            CAO.regroupement(lobs, 6);
            pen.Color = Color.White;
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            AfficheDonnees();

            // Affichage final des 6 classes
            int x, y;
            pen.Color = Color.Blue;
            foreach (Neurone n in listclasses[0].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pen.Color = Color.Green;
            foreach (Neurone n in listclasses[1].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pen.Color = Color.Gray;
            foreach (Neurone n in listclasses[2].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pen.Color = Color.DeepPink;
            foreach (Neurone n in listclasses[3].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pen.Color = Color.BlueViolet;
            foreach (Neurone n in listclasses[4].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pen.Color = Color.Black;
            foreach (Neurone n in listclasses[5].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
            }

            pictureBox1.Refresh();
            //Test();
        }

        private void Test()
        {
            List<Observation> testObs = new List<Observation>();
            for(int i =0; i < bmp.Height; i++)
            {
                for(int j=0; j < bmp.Width; j++)
                {
                    testObs.Add(new Observation(j,i));
                }
            }

            CAO.AlgoKohonen(testObs, Convert.ToDouble(textBox_coeff.Text));
            CAO.regroupement(testObs, 6);
            int x, y;

            foreach (Neurone n in listclasses[0].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.Blue);
           }

            foreach (Neurone n in listclasses[1].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.Green);
            }

            foreach (Neurone n in listclasses[2].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.Gray);
            }

            foreach (Neurone n in listclasses[3].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.DeepPink);
            }

            foreach (Neurone n in listclasses[4].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.BlueViolet);
            }

            foreach (Neurone n in listclasses[5].GetNeurones())
            {
                x = Convert.ToInt32(n.GetPoids(0));
                y = Convert.ToInt32(n.GetPoids(1));
                bmp.SetPixel(x, y, Color.Black);
            }

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
