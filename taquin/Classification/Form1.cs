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
        static Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            p = new Perceptron();
            p.InitialiseInputs();
            p.InitialiseOutputsSupervise();
            p.NormaliseEntrees();
        }

        private void button_initialise_Click(object sender, EventArgs e)
        {
            reseau = new Reseau(Convert.ToInt32(textBox_caches.Text),Convert.ToInt32(textBox_nbCouches.Text));
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
                vect.Add(p.inputs[i,2]);
                lvecteursentrees.Add(vect);
                // Pour la sortie, idem, en général, on la récupère dans le fichier 
                // de données; ici on la crée de toute pièce à partir d'une fonction
                // modèle
                lsortiesdesirees.Add(p.outputs[i,0]);
            }

            reseau.backprop(lvecteursentrees, lsortiesdesirees,
                               Convert.ToDouble(textBox_coeff.Text),
                               Convert.ToInt32(textBox_iterations.Text));

            Test(bmp);
            pictureBox_test.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox_test.Image;           
        }

        private void Test(Bitmap bmp)
        {
            int y, x;
            double z2;
                

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
                    vect.Add(x/800.0); //On normalise les entrées ausis ici 
                    vect.Add(y/800.0);
                    lvecteursentrees.Add(vect);
                }                               
                
            }

            lsortiesobtenues = reseau.ResultatsEnSortie(lvecteursentrees);

            // Affichage
            for (x = 0; x < lsortiesobtenues.Count; x++)
            {
                int r = x % 800; //reste de la div (correspond au x de l'mg)
                int ligne = x / 800; // correspond au numéro de ligne de l'img 
                z2 = lsortiesobtenues[x];
                if (z2 >= 0.75)
                {
                    bmp.SetPixel(r , ligne, Color.Blue); //Classe A en bleu
                }
                else if (z2<=0.2)
                {
                    bmp.SetPixel(r, ligne, Color.Yellow);
                }
                else
                {
                    bmp.SetPixel(r, ligne, Color.Black);
                }
                
                // z2 valeur attendu entre 0 et 1 ; conversion pour z qui est retenu pour l'affichage

            }

            // oncolorie les pixels de l'image avec les données du tableau 
            for(int k=0;k<p.inputs.GetLength(0);k++)
            {
                if (p.inputs[k,0]<1500)
                {
                    bmp.SetPixel(Convert.ToInt32(p.inputs[k, 1] *798 ), Convert.ToInt32(p.inputs[k, 2]*798), Color.White);
                }
                else
                {
                    bmp.SetPixel(Convert.ToInt32(p.inputs[k, 1]*798), Convert.ToInt32(p.inputs[k, 2]*798), Color.Black);
                }

            }

        }
        
    }
}
