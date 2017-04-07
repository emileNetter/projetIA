using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classification
{
    public partial class Form1 : Form
    {
        Perceptron p;
        Reseau reseau;
        static Graphics g;
        static Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            p = new Perceptron();
            p.InitialiseInputs();
            reseau = new Reseau(Convert.ToInt32(textBox_caches.Text));
        }

        private void button_initialise_Click(object sender, EventArgs e)
        {
            List<List<double>> lvecteursentrees = new List<List<double>>();

            // On a 1 seule sortie associée à chaque vecteur d'entrée
            // donc on a seulement 1 liste de réels
            // Attention, on suppose ici que le nième élément de cette liste est
            // la sortie désirée du nième vecteur de levecteurentrees
            List<double> lsortiesdesirees = new List<double>();
            for (int i = 0; i < p.inputs.GetLength(0); i++)
            {

                List<double> vect = new List<double>();
                vect.Add(p.inputs[i,1]);
                vect.Add(p.inputs[i, 2]);
                lvecteursentrees.Add(vect);
                // Pour la sortie, idem, en général, on la récupère dans le fichier 
                // de données; ici on la crée de toute pièce à partir d'une fonction
                // modèle
                lsortiesdesirees.Add(p.outputs[i,0]);
            }

            reseau.backprop(lvecteursentrees, lsortiesdesirees,
                               Convert.ToDouble(textBox_coeff.Text),
                               Convert.ToInt32(textBox_iterations.Text));

            Test(g, bmp);
            pictureBox_test.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox_test.Image;           
        }

        private void Test(Graphics g , Bitmap bmp)
        {
            int y, x, z, zdesire;
            double x2, z2;
                

            List<List<double>> lvecteursentrees = new List<List<double>>();
            List<double> lsortiesobtenues;

            // EN général, on reprend ici les données récupérées du fichier base de données
            // mais pour illustrer le fonctionnement, on se propose ici de tester 200 valeurs
            // de x (dimension 1 pour les entrées ici) entre 0 et 1, ramenées entre 0 et 200
            // idem pour la sortie, pour permettre l'affichage dans une image.
            for (y = 0; y < bmp.Height; y++)
            {
                for(x=0; x < bmp.Width; x++)
                {
                    List<double> vect = new List<double>();
                    vect.Add(x); // Une seule valeur ici pour ce vecteur 
                    vect.Add(y);
                    lvecteursentrees.Add(vect);
                }                               
                
            }

            lsortiesobtenues = reseau.ResultatsEnSortie(lvecteursentrees);

            // Affichage
            for (x = 0; x < 400; x++)
            {
                z2 = lsortiesobtenues[x];

                // z2 valeur attendu entre 0 et 1 ; conversion pour z qui est retenu pour l'affichage
                z = (int)(z2 * 400);               
                bmp.SetPixel(x, bmp.Height - z - 1, Color.Yellow);
            }

        }
        
    }
}
