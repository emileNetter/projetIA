using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace projettaquin
{
    public partial class FormViewGlobal : Form
    {   
        private Objet[] tabObjet = null;
        private Chariot[] tabChariot = null;
        private FormeRectangle[,] tabForme = null;
        public int[,] tabEntrepot = null;
        private static List<GenericNode> Lres;
        public Trajectoire t;

        private Objet objet;
        private Graph g;
        private NodeEntrepot N0;

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
            Refresh();
            
            this.Height = 800;
            this.Width = 1000;
            tabForme = new FormeRectangle[25, 25];
            string color = "";
            //Définition de la position des cases 
            for (int i = 0; i < 26; i++)
            {
                Label labelX = new Label();
                Label labelY = new Label();

                labelX.Parent = this;
                labelX.Location = new Point(25*i , 0);
                labelX.BorderStyle = BorderStyle.FixedSingle;
                labelX.Text = i.ToString();
                labelX.TextAlign = ContentAlignment.MiddleCenter;
                labelX.Size=new Size(25,25);
                labelX.Name = "labelX" + i;
                this.Controls.Add(labelX);

               
                labelY.Parent = this;
                labelY.BorderStyle = BorderStyle.FixedSingle;
                labelY.Location = new Point(0,25*i);
                labelY.Text = i.ToString();
                labelY.TextAlign = ContentAlignment.MiddleCenter;
                labelY.Size = new Size(25,25);
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
                        int positionX = 25+i*25;
                        int positionY = 25+j*25;
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
                    int positionX= 25*c.posX;
                    int positionY= 25*c.posY;
                    FormeRectangle chariot = new FormeRectangle("black", positionX, positionY);
                    FormeRectangle.creationFormeColorée(chariot, this);
                }
                foreach(Objet o in tabObjet)
                {
                    int positionX = 25 * o.posX;
                    int positionY = 25 * o.posY;
                    FormeRectangle objet = new FormeRectangle("orange", positionX, positionY);
                    FormeRectangle.creationFormeColorée(objet, this);
                }
                // Colorie le chemin du noeud initial jusqu'à l'objet
                if (Lres != null)
                {
                    foreach (GenericNode n in Lres)
                    {
                        NodeEntrepot node = (NodeEntrepot)n;
                        int positionX = 25+25*node.posX;
                        int positionY =25+ 25*node.posY;
                        FormeRectangle objet = new FormeRectangle("black", positionX, positionY);
                        FormeRectangle.creationFormeColorée(objet, this);
                    }
                }
            }

        }  

      

        private void button3_Click(object sender, EventArgs e) // Bouton placement manuel
        {
            tabEntrepot = NodeEntrepot.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                tabChariot[i] = new Chariot(1, 1);
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
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                while ( tabEntrepot[posX-1,posY-1] < 0) // -1 car le tableau est décalé
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
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
            btn_valider.Enabled = false;
            objet = new Objet(tabObjet[0].posX - 1, tabObjet[0].posY - 1, tabObjet[0].orientation, 5);
            g = new Graph(objet);
            N0 = new NodeEntrepot(tabChariot[0].posX - 1, tabChariot[0].posY - 1);
            Lres = g.RechercheSolutionAEtoile(N0);
            t = new Trajectoire(Lres,objet); // on passe la liste de generic node à la trajectoire
            t.calculeTemps(); // on calcule le temps mis pour ce chemin

            setViewEntrepot();
            reinitialiserView();
            if (tabChariot.Length != 0 && tabObjet.Length !=0) { btn_LancerSimulation.Enabled = true; }
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

        private void button2_Click(object sender, EventArgs e) // placement aléatoire des objets
        {
            tabEntrepot = NodeEntrepot.InitialiserEntrepot(); 
            Random rd = new Random();
            int NBO = Convert.ToInt32(numericUpDown2.Value);
            tabObjet = new Objet[NBO];

            for (int i = 0; i < tabObjet.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                int orientation = rd.Next(0, 2);
                while (tabEntrepot[posX-1, posY-1] != -1)
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
                }
                if(orientation==0) { tabObjet[i] = new Objet(posX, posY, Objet.Orientation.Nord, 0); }
                else tabObjet[i] = new Objet(posX, posY,Objet.Orientation.Sud,0);
            }

            foreach (Objet o in tabObjet)
            {
                comboBoxAleatoire.Items.Add("Objet : x=" + o.posX + " y=" + o.posY + " orientation=" + o.orientation);
            }
        }


        private void reinitialiserView()
        {
            /*foreach (Control l in this.Controls.OfType<Control>())
            {
                if(l is Label)
                {
                    if(l.Name == "label1" || l.Name == "label2" || l.Name == "label3" || l.Name == "label4")
                    {
                        l.Visible = false;                    }
                }
                else l.Visible = false;
            }*/
            groupBox1.Visible = false;
            button_nouveau.Visible = true;
        }

        private void button_nouveau_Click(object sender, EventArgs e)
        {
            /*foreach (Control l in this.Controls.OfType<Control>())
            {
                if (l is Label)
                {
                    if (l.Name == "label1" || l.Name == "label2" || l.Name == "label3" || l.Name == "label4")
                    {
                        l.Visible = true;
                    }
                }
                else l.Visible = true;
            }*/
            btn_valider.Enabled = true;
            groupBox1.Visible = true;
            button_nouveau.Visible = false;
        }

        
    }
}
