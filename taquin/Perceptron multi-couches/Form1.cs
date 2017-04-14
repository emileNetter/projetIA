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
        static Bitmap bmp2;
        static Bitmap bmp3;
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

        

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;
            bmp2 = (Bitmap)pictureBox2.Image;
            bmp3 = (Bitmap)pictureBox2.Image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            reseau.AfficheInfoNeurone(Convert.ToInt32(textBoxnumcouche.Text),
                                       Convert.ToInt32(textBoxnumneur.Text),
                                       listBox1);

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

        public void TestsErreur(Graphics g, Bitmap bmp)
        {
            int x, z;
            double z2;
            // image blanche 
            for (x = 0; x < bmp.Width; x++)
            {
                for (z = 0; z < bmp.Height; z++)
                {
                    bmp.SetPixel(x, z, Color.White);
                }
            }

            List<List<double>> lvecteursentrees = new List<List<double>>();
            List<double> lsortiesdesirees = new List<double>();
            List<double> lsortiesobtenues;

            // EN général, on reprend ici les données récupérées du fichier base de données
            // mais pour illustrer le fonctionnement, on se propose ici de tester 200 valeurs
            // de x (dimension 1 pour les entrées ici) entre 0 et 1, ramenées entre 0 et 200
            // idem pour la sortie, pour permettre l'affichage dans une image.
            for (x = 0; x < tabValeurs.GetLength(0); x++)
            {
                // Initialisation des activations  ai correspondant aux entrées xi
                // Le premier neurone est une constante égale à 1
                List<double> vect = new List<double>();
                vect.Add(tabValeurs[x,0]/500); // Une seule valeur ici pour ce vecteur
                vect.Add(tabValeurs[x, 1]/500);
                lvecteursentrees.Add(vect);
            }

            lsortiesobtenues = reseau.ResultatsEnSortie(lvecteursentrees);

            // Affichage

            List<double> listIntensite = new List<double>();
            List<double> listIntensiteCorrigee = new List<double>();

            for (x = 0; x < tabValeurs.GetLength(0); x++)
            {
                z2 = lsortiesobtenues[x];
                listIntensite.Add(z2);                       

            }
            double min = listIntensite[0];
            double max = listIntensite[0];
            // On recherche le minimum et le maximum 
            foreach(double element in listIntensite)
            {
                if (element < min) min = element;
                else max = element;
            }

            for (int m=0; m<tabValeurs.GetLength(0);m++)
            {
                int abs = Convert.ToInt32(0.4 * tabValeurs[m, 0]); // a corriger en fonction du max de datasetregression !
                int ord = Convert.ToInt32(0.4 * tabValeurs[m, 1]);
                double intensite = listIntensite[m] * 255;//Remap( listIntensite[m], min, max, 0, 255); // il faut maper
                int i = Convert.ToInt32(Math.Floor(intensite));
                bmp.SetPixel(abs, ord, Color.FromArgb(i, i, i));
            }

        }

        public void TestsAffichage(Graphics g, Bitmap bmp)
        {
            int x, z;
            double z2;
            // image blanche 
            for (x = 0; x < bmp.Width; x++)
            {
                for (z = 0; z < bmp.Height; z++)
                {
                    bmp.SetPixel(x, z, Color.White);
                }
            }

            List<List<double>> lvecteursentrees = new List<List<double>>();
            List<double> lsortiesdesirees = new List<double>();
            List<double> lsortiesobtenues;

            // EN général, on reprend ici les données récupérées du fichier base de données
            // mais pour illustrer le fonctionnement, on se propose ici de tester 200 valeurs
            // de x (dimension 1 pour les entrées ici) entre 0 et 1, ramenées entre 0 et 200
            // idem pour la sortie, pour permettre l'affichage dans une image.
            for (x = 0; x < tabValeurs.GetLength(0); x++)
            {
                // Initialisation des activations  ai correspondant aux entrées xi
                // Le premier neurone est une constante égale à 1
                List<double> vect = new List<double>();
                vect.Add(tabValeurs[x, 0] / 500); // Une seule valeur ici pour ce vecteur
                vect.Add(tabValeurs[x, 1] / 500);
                lvecteursentrees.Add(vect);
            }

            lsortiesobtenues = reseau.ResultatsEnSortie(lvecteursentrees);

            // Affichage

            List<double> listIntensite = new List<double>();
            List<double> listIntensiteCorrigee = new List<double>();

            for (x = 0; x < tabValeurs.GetLength(0); x++)
            {
                z2 = lsortiesobtenues[x];
                listIntensite.Add(z2);

            }
            double min = listIntensite[0];
            double max = listIntensite[0];
            // On recherche le minimum et le maximum 
            foreach (double element in listIntensite)
            {
                if (element < min) min = element;
                else max = element;
            }

            for (int m = 0; m < tabValeurs.GetLength(0); m++)
            {
                int abs = Convert.ToInt32(0.4 * tabValeurs[m, 0]); // a corriger en fonction du max de datasetregression !
                int ord = Convert.ToInt32(0.4 * tabValeurs[m, 1]);
                double intensite = listIntensite[m] * 255;//Remap( listIntensite[m], min, max, 0, 255); // il faut maper
                int i = Convert.ToInt32(Math.Floor(intensite));
                bmp.SetPixel(abs, ord, Color.FromArgb(i, i, i));
            }

        }

        /************************************************************************************************************************/

        private void button4_Click(object sender, EventArgs e) // bouton apprentissage 2
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
            for (int i = 0; i < tabValeurs.GetLength(0); i++)
            {
                double x =(tabValeurs[i, 0])/500;
                double y = (tabValeurs[i, 1])/500;

                List<double> vect = new List<double>();

                vect.Add(x); vect.Add(y); // deux valeurs dans notre cas         
                // EN général, un vecteur sera récupéré dans un fichier de données
                lvecteursentrees.Add(vect);
                // Pour la sortie, idem, en général, on la récupère dans le fichier 
                // de données; ici on la crée de toute pièce à partir d'une fonction
                // modèle
                lsortiesdesirees.Add(tabValeurs[i,2]);
            }

            //Calcul du reseau 
            reseau.backprop(lvecteursentrees, lsortiesdesirees,
                               Convert.ToDouble(textBoxalpha.Text),
                               Convert.ToInt32(textBoxnbiter.Text));


            TestsAffichage(g, bmp2);
            TestsErreur(g, bmp3);

            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            
        }

            // Map des valeurs d'un interval sur un autre
            public static double Remap(double value, double from1, double to1, double from2, double to2)
            {
                return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
            }
        

    }
}
