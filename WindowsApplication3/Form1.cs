using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        static List<Observation> lobs = new List<Observation>();
        static public Random random;
        private Graphics g;	// Objet graphique placé en global
        private Bitmap bmp;
        private Pen pen;		// Crayon placé en global
        private int nbcol;      // nb de colonnes de la carte de Kohonen
        private int nblignes;   // nb de lignes de la carte

        CarteSOM SOM;
        static public List<Classe> listclasses = new List<Classe>();

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage( pictureBox1.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nbcol = Convert.ToInt32(textBox1.Text);
            nblignes = Convert.ToInt32(textBox2.Text);
            bmp = (Bitmap)pictureBox1.Image;
            pen = new Pen(Color.White, 1);
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            double u1, u2,r1, r2 ;
            int hasard;

            // Creation des données
            lobs.Clear();
            for (int i=0; i<1000; i++)
            {
                double x, y;
              
              /*  x = random.Next(bmp.Width);
                y = random.Next(bmp.Height);
                lobs.Add(new Observation(x, y));
            */

                
                  hasard = random.Next(10);
                  if (hasard > 5)
                  {
                      do {
                          u1 = random.NextDouble();
                          u2 = random.NextDouble();
                          r1 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                          u1 = random.NextDouble();
                          u2 = random.NextDouble();
                          r2 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                          // r1 et r2 variables gaussiennes centrées réduites (donc moyenne 0, écart-type 1)
                          x = r1 * 10 + bmp.Width / 2;    // x variable gaussienne de moyenne bmp.width /2 et d'écart-type 50
                          y = r2 * 20 + bmp.Height / 4;  // y variable gaussienne de moyenne bmp.height/4 et d'écart-type 10
                      }
                      while ((x <0) || x>=bmp.Width || y>=bmp.Height);
                      lobs.Add( new Observation( x, y));
                  }  
                  else if (hasard > 2)
                   {  do {
                       u1 = random.NextDouble();
                       u2 = random.NextDouble();
                       r1 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                       u1 = random.NextDouble();
                       u2 = random.NextDouble();
                       r2 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                       x = r1 * 50 + bmp.Width / 2;  // x variable gaussienne de moyenne bmp.width /2 et d'écart-type 50
                       y = r2 * 20 + 3 * bmp.Height / 4;  // y variable gaussienne de moyenne 3*bmp.height/4 et d'écart-type 20
                      }
                      while ((x <0)|| x>=bmp.Width || y>=bmp.Height);
                      lobs.Add( new Observation( x, y));
                  }
                  else
                   { do {
                       u1 = random.NextDouble();
                       u2 = random.NextDouble();
                       r1 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                       u1 = random.NextDouble();
                       u2 = random.NextDouble();
                       r2 = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                       x = r1 * 20 + bmp.Width / 5; // x variable gaussienne de moyenne bmp.width /5 et d'écart-type 20
                       y = r2 * 30 + 3 * bmp.Height / 5;  // y variable gaussienne de moyenne 3*bmp.height/5 et d'écart-type 30
                      }
                      while (x <0 || x>=bmp.Width || y>=bmp.Height);
                      lobs.Add( new Observation( x, y));
               }  
            }
                 

            // Creation de la carte SOM
            SOM = new CarteSOM(nbcol, nblignes, 2, bmp.Width);

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
            for (int i = 0; i < nbcol; i++)
                for (int j = 0; j < nblignes; j++)
                {
                    x = Convert.ToInt32(SOM.GetNeurone(i,j).GetPoids(0));
                    y = Convert.ToInt32(SOM.GetNeurone(i,j).GetPoids(1));
                    g.DrawEllipse(pen, x - 2, y - 2, 4, 4);

                }
            pictureBox1.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SOM.AlgoKohonen(lobs, Convert.ToDouble(textBox3.Text));
            pen.Color = Color.White;
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            AfficheDonnees();
            AfficheCarteSOM();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listclasses.Clear();
            // Regroupement pour obtenir 2 classes
            SOM.regroupement(lobs,2 );
            pen.Color = Color.White;
            g.FillRectangle(pen.Brush, 0, 0, bmp.Width, bmp.Height);
            AfficheDonnees();

            // Affichage final des 2 classes
            int x, y;
            pen.Color = Color.Blue;
            foreach (Neurone n in listclasses[0].GetNeurones())
                {
                    x = Convert.ToInt32(n.GetPoids(0));
                    y = Convert.ToInt32(n.GetPoids(1));
                    g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
                }

            pen.Color = Color.Yellow;
            foreach (Neurone n in listclasses[1].GetNeurones())
                {
                    x = Convert.ToInt32(n.GetPoids(0));
                    y = Convert.ToInt32(n.GetPoids(1));
                    g.DrawEllipse(pen, x - 2, y - 2, 4, 4);
                }

            pictureBox1.Refresh();  
        }

       
    }
}