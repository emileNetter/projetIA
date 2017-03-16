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
        private int posX { get; set; }
        private int posY { get; set; }
        private enum orientation { nord,sud};
        private int hauteur;
        

        public Chariot (int _posX, int _posY)
        {
            posX = _posX;
            posY = _posY;
        }

        public Chariot()
        {
            posX = 0;
            posY = 0;
        }
    }
}
