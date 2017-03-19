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
        int hForm;
        int lForm;

        public FormViewGlobal()
        {
            InitializeComponent();
            hForm = this.Height;
            lForm = this.Width;
            setView();
        }

        public void setView()
        {
            string color = "";
            //Définition de la position des cases 
            for (int i = 1; i < 26; i++)
            {
                Label labelX = new Label();
                Label labelY = new Label();

                labelX.Parent = this;
                labelX.Location = new Point((i+1) * (lForm / 25), hForm / 25);
                labelX.BorderStyle = BorderStyle.FixedSingle;
                labelX.Text = i.ToString();
                labelX.TextAlign = ContentAlignment.MiddleCenter;
                labelX.Size=new Size(lForm/25,hForm/25);
                labelX.Name = "labelX" + i;
                this.Controls.Add(labelX);

               
                labelY.Parent = this;
                labelY.BorderStyle = BorderStyle.FixedSingle;
                labelY.Location = new Point((lForm / 25), (i+1)*hForm / 25);
                labelY.Text = i.ToString();
                labelY.TextAlign = ContentAlignment.MiddleCenter;
                labelY.Size = new Size(lForm / 25, hForm / 25);
                labelY.Name = "labelY" + i;
                this.Controls.Add(labelY);


            }
            

            for (int i = 0; i < tabEntrepot.GetLength(0); i++)
            {
                for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                {
                    int positionX = (i+2)* (lForm/25);
                    int positionY = (j+2)* (hForm/25);
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
            initialiseChariots(e.Graphics);
        }  

        protected void initialiseChariots(Graphics g)
        {
            Chariot c = new Chariot();
            Pen p = new Pen(Color.Black);          
            g.DrawEllipse(p, c.posX, c.posY, 15, 15);
            g.Dispose();

        

        }

        /*private void FormViewGlobal_Resize(object sender, EventArgs e)
        {
            hForm = this.Height;
            lForm = this.Width;
        }*/
    }
}
