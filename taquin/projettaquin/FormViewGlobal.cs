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
        public int[,] tabEntrepot;
        private static List<GenericNode> Lres;

        private Objet objet;
        private Graph g;
        private NodeDistance N0;

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

            if (tabEntrepot==null) { tabEntrepot = NodeDistance.InitialiserEntrepot(); }
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
                        NodeDistance node = (NodeDistance)n;
                        int positionX = 25+25*node.posX;
                        int positionY =25+ 25*node.posY;
                        FormeRectangle objet = new FormeRectangle("red", positionX, positionY);
                        FormeRectangle.creationFormeColorée(objet, this);
                    }
                }
            }

        }  

      

        private void button3_Click(object sender, EventArgs e) // Bouton placement manuel
        {
            comboBoxManuel.Items.Clear();
            tabEntrepot = NodeDistance.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                tabChariot[i] = new Chariot(1, 1);//récupérer position du chariot 
                comboBoxManuel.Items.Add(tabChariot[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e) // Bonton placement aléaoire chariot
        {

            comboBox1.Items.Clear();
            tabEntrepot = NodeDistance.InitialiserEntrepot(); // On initialise le tableau "source"

            Random rd = new Random();
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                while ( NodeDistance.tabEntrepot[posX-1,posY-1] < 0) // -1 car le tableau est décalé
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
                }
                tabChariot[i] = new Chariot(posX, posY);
                NodeDistance.tabEntrepot[posX-1, posY-1] = -1; //on utilise le tab du Node distance !
            }

            foreach (Chariot c in tabChariot)
            {
                comboBox1.Items.Add("Chariot : x="+c.posX+" y="+c.posY);
            }
        }

        private void btn_valider_Click(object sender, EventArgs e) // Bouton valider
        {
            if (tabChariot != null && tabObjet != null)
            {
                btn_valider.Enabled = false;
                objet = new Objet(tabObjet[0].posX - 1, tabObjet[0].posY - 1, tabObjet[0].orientation, 5);
                g = new Graph(objet);
                N0 = new NodeDistance(tabChariot[0].posX - 1, tabChariot[0].posY - 1);
                Lres = g.RechercheSolutionAEtoile(N0);
                if (Lres.Count > 1)
                {
                    Lres.RemoveAt(0); //On supprime le premier noeud correspondant à la position du chariot
                }
                
                reinitialiserView();
                setViewEntrepot();
            }
            else
            {
                label_error.Visible = true;
            }

            

        }

        private void comboBoxManuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chariot c = (Chariot)comboBoxManuel.SelectedItem;
            textBoxX.Text = Convert.ToString(c.posX);
            textBoxY.Text = Convert.ToString(c.posY);
        }

        private void btn_ValiderPos_Click(object sender, EventArgs e) // bouton ok
        {
            Chariot c = (Chariot)comboBoxManuel.SelectedItem;
            c.posX = Convert.ToInt32(textBoxX.Text);
            c.posY = Convert.ToInt32(textBoxY.Text);
        }

        private void button2_Click(object sender, EventArgs e) // placement aléatoire des objets
        {
            comboBoxAleatoire.Items.Clear();
            label_error.Visible = false;           
            Random rd = new Random();
            int NBO = Convert.ToInt32(numericUpDown2.Value);
            tabObjet = new Objet[NBO];

            for (int i = 0; i < tabObjet.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                int orientation = rd.Next(0, 2);
                while (NodeDistance.tabEntrepot[posX-1, posY-1] != -1) //on utilise le tab du Node distance !
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
            label_error.Visible = false;
            groupBox2.Visible = false;
            button_nouveau1.Visible = true;
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
            tabEntrepot = NodeDistance.InitialiserEntrepot();
            label_error.Visible = false;
            btn_valider.Enabled = true;
            groupBox2.Visible = true;
            button_nouveau1.Visible = false;
        }

        
    }
}
