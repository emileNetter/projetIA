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
        Chariot[] tabChariot = null;
        FormeRectangle[,] tabForme = null;
        public int[,] tabEntrepot = null;
        int hForm;
        int lForm;



        public FormViewGlobal()
        {
            InitializeComponent();
            hForm = this.Height;
            lForm = this.Width;
            numericUpDown1.Value = 1;
        }


        public void setViewEntrepot()
        {
            tabForme = new FormeRectangle[25, 25];
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

            if (tabEntrepot==null) { tabEntrepot = NodeEntrepot.InitialiserEntrepot(); }
            else
            {

                for (int i = 0; i < tabEntrepot.GetLength(0); i++)
                {
                    for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                    {
                        int positionX = (i + 2) * (lForm / 25);
                        int positionY = (j + 2) * (hForm / 25);
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

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (tabForme != null)
            {
                for (int i = 0; i < tabEntrepot.GetLength(0); i++)
                {
                    for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                    {
                        FormeRectangle.creationFormeColorée(tabForme[i, j], this);
                    }
                }
                foreach(Chariot c in tabChariot)
                {
                    initialiseAffichageChariot(e.Graphics, c);
                }
            }
        }  

        protected void initialiseAffichageChariot(Graphics g, Chariot chariot)
        {
                Pen p = new Pen(Color.Black);
                g.DrawEllipse(p, (chariot.posX + 1) * (lForm / 25), (chariot.posY + 1) * (hForm / 25), lForm / 25, hForm / 25);
                g.Dispose();

        }

        private void button3_Click(object sender, EventArgs e) // Bouton placement manuel
        {
            tabEntrepot = NodeEntrepot.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                tabChariot[i] = new Chariot(0, 0);
                comboBoxManuel.Items.Add(tabChariot[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e) // Bonton placement aléaoire
        {
            tabEntrepot = NodeEntrepot.InitialiserEntrepot(); // On initialise le tableau "source"
            Random rd = new Random();
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                int posX = rd.Next(0, 25);
                int posY = rd.Next(0, 25);
                while (tabEntrepot[posX, posY] != -1 || tabEntrepot[posX,posY] > 0) 
                {
                    posX = rd.Next(0, 25);
                    posY = rd.Next(0, 25);
                }
                tabChariot[i] = new Chariot(posX, posY);
            }

            foreach(Chariot c in tabChariot)
            {
                comboBox1.Items.Add("Chariot : x="+c.posX+" y="+c.posY);
            }
        }

        private void btn_valider_Click(object sender, EventArgs e) // Bouton valider
        {
            setViewEntrepot(); 
        }

        private void comboBoxManuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chariot c = (Chariot)comboBoxManuel.SelectedItem;
            textBoxX.Text = Convert.ToString(c.posX);
            textBoxY.Text = Convert.ToString(c.posY);
        }

        private void btn_ValiderPos_Click(object sender, EventArgs e)
        {
            Chariot c = (Chariot)comboBoxManuel.SelectedItem;
            c.posX = Convert.ToInt32(textBoxX.Text);
            c.posY = Convert.ToInt32(textBoxY.Text);
        }


       
    }
}
