using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace projettaquin
{
    public class Chariot
    {
        public int posX { get; set; }
        public int posY { get; set; }
        private enum orientation { nord,sud};
        private int hauteur;
        Random rd = new Random();
        

        public Chariot (int _posX, int _posY)
        {
            posX = _posX;
            posY = _posY;
            NodeEntrepot.tabEntrepot[posX, posY] = -1;
        }

        // Constructeur par défaut (positionné sur la première colonne)
        public Chariot()
        {
            posX = 0;
            posY = 0;
        }

        public override string ToString()
        {
            return "x : "+posX+" y : "+posY;
        }



        public static void initialiseAffichageChariot(Form f, Chariot chariot)
        {
            Pen p = new Pen(Color.Black);
            int largeur = f.Width / 25;
            int hauteur = f.Height / 25;

            Graphics formGraphics = f.CreateGraphics();

            //Debug.WriteLine((chariot.posX + 1) * (Width / 25));
            //g.FillEllipse(new SolidBrush(Color.Black), 100, 100, 50, 50);
            //g.FillEllipse(new SolidBrush(Color.Black), (chariot.posX + 1) * (Height / 25), (chariot.posY + 1) * (Width /25), 100, 100);
            //valeurs tests
            formGraphics.DrawEllipse(p,((chariot.posX) * (largeur)), (chariot.posY) * (hauteur), largeur/2, hauteur/2);
            formGraphics.Dispose();

        }
    }
}
