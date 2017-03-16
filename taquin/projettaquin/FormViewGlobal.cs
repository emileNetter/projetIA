using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projettaquin
{
    public partial class FormViewGlobal : Form
    {
        FormeRectangle[,] tabForme = new FormeRectangle[25, 25];
        public int[,] tabEntrepot = NodeEnrepot.InitialiserEntrepot();

        public FormViewGlobal()
        {
            InitializeComponent();
            setView();

        }
        

        public void setView()
        {
            string color = "";
            for (int i = 0; i < tabEntrepot.GetLength(0); i++)
            {
                for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                {
                    int positionX = 20 * i ;
                    int positionY = 20 * j;
                    int value = tabEntrepot[i, j];
                    switch (value)
                    {
                        case 0:
                            color = "white";
                            break;
                        case -1:
                            color = "blue";
                            break;
                        case -2:
                            color = "green";
                            break;


                    }
                    FormeRectangle r = new FormeRectangle(color, positionX, positionY);
                    tabForme[i, j] = r;


                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < tabEntrepot.GetLength(0); i++)
            {
                for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                {
                    FormeRectangle.creationFormeColorée(tabForme[i, j], this);

                }
            }




        }
    }
}
