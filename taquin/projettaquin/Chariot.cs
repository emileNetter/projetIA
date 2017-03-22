using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
      
    }
}
