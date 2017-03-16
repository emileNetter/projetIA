using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projettaquin
{
    class Chariot
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

    }
}
