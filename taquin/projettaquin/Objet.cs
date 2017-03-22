﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projettaquin
{

    public class Objet
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public enum Orientation { Nord, Sud };
        public Orientation orientation { get; set; }
        private int hauteur { get; set; }

        public Objet(int posX, int posY, Orientation orientation, int hauteur )
        {
            this.posX = posX;
            this.posY = posY;
            this.orientation = orientation;
            this.hauteur = hauteur;
        }
    }
}
