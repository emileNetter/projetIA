using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Graphics g;
        static Bitmap bmp;
        Random rnd = new Random();
        static double[,] tabValeurs = new double[3000, 3];
        Reseau reseau;
        
        // On initialise un tableau avec les valeurs des triplets de datasetregression
        double[,] initialiserValeur(string chemin)
        {            
            string[] lines = System.IO.File.ReadAllLines(chemin);
            int cpt = -1;
            for (int k=0; k<lines.GetLength(0);k+=4)
            {
                cpt++;
                tabValeurs[cpt, 0] = Convert.ToDouble(lines[k+1]);
                tabValeurs[cpt, 1] = Convert.ToDouble(lines[k+2]);
                tabValeurs[cpt, 2] = Convert.ToDouble(lines[k+3]);
                
            }
            return tabValeurs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            initialiserValeur("datasetregression.txt");
            TestsTriplet(g, bmp, tabValeurs);
            pictureBox1.Invalidate();
            // Initialisation d'un réseau de neurones avec le nombre d'entrées, 
            // le nombre de couches et le nbre de neurones par couches
            reseau = new Reseau(Convert.ToInt32(textBoxnbentrees.Text),
                                        Convert.ToInt32(textBoxnbcouches.Text),
                                        Convert.ToInt32(textBoxnbneurcouche.Text));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // En entrée on a une liste de k valeurs réelles correspondant aux k neurones
            // de la 1ère couche dite couche des entrées ou entrées tout court
            // On dispose en général d'une base de données de vecteurs d'entrées
            // c'est pour cela qu'on a une liste de vecteurs, donc une liste de liste 
            List<List<double>> lvecteursentrees = new List<List<double>>();

            // On a 1 seule sortie associée à chaque vecteur d'entrée
            // donc on a seulement 1 liste de réels
            // Attention, on suppose ici que le nième élément de cette liste est
            // la sortie désirée du nième vecteur de levecteurentrees
            List<double> lsortiesdesirees = new List<double>();
            for (int i = 0; i < 1000; i++)
            {
                List<double> vect = new List<double>();
                double x = rnd.NextDouble();
                vect.Add(x); // Une seule valeur ici pour ce vecteur 
                             // EN général, un vecteur sera récupéré dans un fichier de données
                lvecteursentrees.Add(vect);
                // Pour la sortie, idem, en général, on la récupère dans le fichier 
                // de données; ici on la crée de toute pièce à partir d'une fonction
                // modèle
                lsortiesdesirees.Add(fonctionmodele(x));
            }

            reseau.backprop(lvecteursentrees, lsortiesdesirees,
                               Convert.ToDouble(textBoxalpha.Text),
                               Convert.ToInt32(textBoxnbiter.Text));
            Tests(g, bmp);
            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            reseau.AfficheInfoNeurone(Convert.ToInt32(textBoxnumcouche.Text),
                                       Convert.ToInt32(textBoxnumneur.Text),
                                       listBox1);

        }
        /*****************************************************************/
        // Attention, la fonction à apprendre doit fournir des valeurs entre 0 et 1 !!!
        double fonctionmodele(double x)
        {
            // return Math.Sin(x * 20) / 2.5 + 0.5;
            if (x < 0.2 || x > 0.8) return 0.8;
            else return 0.2;
        }

        public void TestsTriplet(Graphics g, Bitmap bmp, double[,] tab)
        {
            for(int k=0; k<tab.GetLength(0);k++)
            {
                int larg = bmp.Width;
                int haut = bmp.Height;
                int intensite = Convert.ToInt32(Math.Floor(tab[k, 2] * 255));

                int x, y;
                x =Convert.ToInt32( 0.4*tab[k, 0]); // a corriger en fonction du max de datasetregression !
                y =Convert.ToInt32( 0.4*tab[k, 1]);
                bmp.SetPixel(x, y, Color.FromArgb(intensite, intensite, intensite));
            }

        }

        /**********************************************************************/
        public void Tests(Graphics g, Bitmap bmp)
        {
            int x, z, zdesire;
            double x2, z2;
            for (x = 0; x < bmp.Width; x++)
                for (z = 0; z < bmp.Height; z++)
                    bmp.SetPixel(x, z, Color.Black);

            List<List<double>> lvecteursentrees = new List<List<double>>();
            List<double> lsortiesdesirees = new List<double>();
            List<double> lsortiesobtenues;

            // EN général, on reprend ici les données récupérées du fichier base de données
            // mais pour illustrer le fonctionnement, on se propose ici de tester 200 valeurs
            // de x (dimension 1 pour les entrées ici) entre 0 et 1, ramenées entre 0 et 200
            // idem pour la sortie, pour permettre l'affichage dans une image.
            for (x = 0; x < 200; x++)
            {
                x2 = x / 200.0;
                // Initialisation des activations  ai correspondant aux entrées xi
                // Le premier neurone est une constante égale à 1
                List<double> vect = new List<double>();
                vect.Add(x2); // Une seule valeur ici pour ce vecteur 
                lvecteursentrees.Add(vect);
                lsortiesdesirees.Add(fonctionmodele(x2));
            }

            lsortiesobtenues = reseau.ResultatsEnSortie(lvecteursentrees);

            // Affichage
            for (x = 0; x < 200; x++)
            {
                z2 = lsortiesobtenues[x];

                // z2 valeur attendu entre 0 et 1 ; conversion pour z qui est retenu pour l'affichage
                z = (int)(z2 * 200);
                zdesire = (int)(lsortiesdesirees[x] * 200);
                bmp.SetPixel(x, bmp.Height - z - 1, Color.Yellow);

                bmp.SetPixel(x, bmp.Height - zdesire - 1, Color.White);
            }

        }

    }
}
