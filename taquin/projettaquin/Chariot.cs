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
        }

        // Constructeur par défaut (positionné sur la première colonne)
        public Chariot()
        {
            posX = 12;
            posY = rd.Next(12,800);
        }
      
    }
}
