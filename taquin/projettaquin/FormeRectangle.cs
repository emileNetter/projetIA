using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace projettaquin
{
    class FormeRectangle
    {
        public string couleur { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }


        public FormeRectangle(string c, int posX, int posY)
        {
            couleur = c;
            positionX = posX;
            positionY = posY;

        }

        public static void creationFormeColorée(FormeRectangle f, Form form) // la mettre dans la classe forme et la généraliser avec celle de perception
        {
            int largeur = form.Width/25;
            int hauteur = form.Height/25;
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush greenBrush = new SolidBrush(Color.Green);

            Pen blackPen = new Pen(Brushes.Black);
            blackPen.Width = 1.0F;

            Graphics formGraphics = form.CreateGraphics();
            
            Rectangle rect=new Rectangle(f.positionX, f.positionY, largeur, hauteur);

            switch (f.couleur)
            {
                case "blue":
                    

                    formGraphics.FillRectangle(blueBrush, rect);
                    formGraphics.DrawRectangle(blackPen, rect);
                    blueBrush.Dispose();
                    formGraphics.Dispose();
                    break;

                case "white":

                    formGraphics.FillRectangle(whiteBrush, rect);
                    formGraphics.DrawRectangle(blackPen, rect);
                    whiteBrush.Dispose();
                    formGraphics.Dispose();
                    break;

                case "green":
                    formGraphics.FillRectangle(greenBrush, rect);
                    formGraphics.DrawRectangle(blackPen, rect);
                    greenBrush.Dispose();
                    formGraphics.Dispose();
                    break;
            }
        }
    }  
}
