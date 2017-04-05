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
        private static int compteurCharMan = 0;
        private static int compteurObjMan = 0;

        private static Chariot charChoisi;
        private static Objet objChoisi;

        private static List<GenericNode> Lres;
        private static List<GenericNode> TrajectoireF = new List<GenericNode>();
        private static List<List<GenericNode>> EnsembleTrajectoiresF = new List<List<GenericNode>>();
        List<GenericNode> bestTrajectoire = new List<GenericNode>();

        private Objet objet;
        private Graph g;
        private GenericNode Ninit;
        private GenericNode Nfinal;
        private GenericNode Nobj;
        

        private static Boolean nodeTemps;

        int hForm;
        int lForm;

        public FormViewGlobal()
        {
            InitializeComponent();
            hForm = this.Height;
            lForm = this.Width;
            numericUpDown1.Value = 3;
            
           
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

            if (tabEntrepot==null) { tabEntrepot = GenericNode.InitialiserEntrepot(); }
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
                        int positionX = 0;
                        int positionY = 0;
                        if (nodeTemps)
                        {
                            NodeTemps node = (NodeTemps)n;
                            positionX = 25 + 25 * node.posX;
                            positionY = 25 + 25 * node.posY;
                        }
                        else
                        {
                            NodeDistance node = (NodeDistance)n;
                            positionX = 25 + 25 * node.posX;
                            positionY = 25 + 25 * node.posY;
                        }
                        
                        
                        FormeRectangle objet = new FormeRectangle("red", positionX, positionY);
                        FormeRectangle.creationFormeColorée(objet, this);
                    }
                }
                if (bestTrajectoire != null)
                {
                    foreach (GenericNode n in bestTrajectoire)
                    {
                        NodeTemps node = (NodeTemps)n;
                        int positionX = 25 + 25 * node.posX;
                        int positionY = 25 + 25 * node.posY;
                        FormeRectangle objet = new FormeRectangle("purple", positionX, positionY);
                        FormeRectangle.creationFormeColorée(objet, this);
                    }
                }
            }

        }  

      

        private void button3_Click(object sender, EventArgs e) // Bouton placement manuel chariot
        {
            textBoxPosChar.Clear();
            listBoxChar.Items.Clear();
            textBoxX.Enabled = true;
            textBoxY.Enabled = true;
            btn_ValiderPos.Enabled = true;
            label8.Visible = true;
            countNbCharMan.Visible = true;
            btn_calcul_temps.Enabled = false;
            btn_calcul_distance.Enabled = false;
            countNbCharMan.Text = (compteurCharMan+1).ToString();
            tabEntrepot = GenericNode.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];


        }

        private void button1_Click(object sender, EventArgs e) // Bonton placement aléaoire chariot
        {
            textBoxPosChar.Clear();
            btn_calcul_temps.Enabled = true;
            btn_calcul_distance.Enabled = true;
            listBoxChar.Items.Clear();
            tabEntrepot = GenericNode.InitialiserEntrepot(); // On initialise le tableau "source"

            Random rd = new Random();
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];

            for (int i = 0; i < tabChariot.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                while ( GenericNode.tabEntrepot[posX-1,posY-1] < 0) // -1 car le tableau est décalé
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
                }
                tabChariot[i] = new Chariot(posX, posY);
                GenericNode.tabEntrepot[posX-1, posY-1] = -1; //on utilise le tab du Node distance !
            }
           
            foreach (Chariot c in tabChariot)
            {
                listBoxChar.Items.Add(c);
            }
        }

        private void btn_valider_Click(object sender, EventArgs e) // Methode pour bouton Calcul
        {

            if (textBoxPosChar.Text != "" && tbObjChois.Text != "")
            {



                objet = new Objet(objChoisi.posX - 1, objChoisi.posY - 1, objChoisi.orientation, objChoisi.hauteur);
                g = new Graph(objet);

                if (sender.Equals(btn_calcul_distance))
                {
                Ninit = new NodeDistance(charChoisi.posX - 1, charChoisi.posY - 1); 
                nodeTemps = false;
               }
               else
                {
                Ninit = new NodeTemps(charChoisi.posX - 1, charChoisi.posY - 1, new Point(0, 0));
                nodeTemps = true;
                }

                Lres = g.RechercheSolutionAEtoile(Ninit);

                if(nodeTemps)// On ne fait le trajet final que si c'est un calcul de temps 
                {
                    // Trajet vers la zone finale
                    Nobj = (NodeTemps)Lres[Lres.Count - 1]; //Noeud sur lequel est le chariot lorsqu'il prend l'objet

                    List<Objet> zoneFinale = new List<Objet>(tabEntrepot.GetLength(0));
                    for (int k = 0; k < tabEntrepot.GetLength(0) - 1; k++)
                    {
                        Objet o = new Objet(0, k - 1, Objet.Orientation.Sud, 0);
                        zoneFinale.Add(o);
                    }

                    foreach (Objet o in zoneFinale)
                    {
                        Graph g = new Graph(o);
                        TrajectoireF = g.RechercheSolutionAEtoile(Nobj);
                        EnsembleTrajectoiresF.Add(TrajectoireF);
                    }

                    bestTrajectoire = EnsembleTrajectoiresF[0];
                    double cout = 1000000;
                    foreach (List<GenericNode> l in EnsembleTrajectoiresF)
                    {
                        double c = l[l.Count - 1].Cout_Total;
                        if (c < cout)
                        {
                            cout = c;
                            bestTrajectoire = l;
                        }


                    }

                    lbTimeTot.Text=CalculTempsTot().ToString()+" secondes" ;
                }

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

        
        private void listBoxChar_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            textBoxPosChar.Text = listBoxChar.SelectedItem.ToString();
            label_error.Visible = false;
            charChoisi = (Chariot)listBoxChar.SelectedItem;

        }

        private void btn_ValiderPos_Click(object sender, EventArgs e) // bouton AjoutChariot
        {
            compteurCharMan++;
            Chariot c = new Chariot(int.Parse(textBoxX.Text), int.Parse(textBoxY.Text));
            listBoxChar.Items.Add(c);
            countNbCharMan.Text = (compteurCharMan+1).ToString();
            textBoxX.Clear();
            textBoxY.Clear();
            tabChariot[compteurCharMan-1] = c;
            GenericNode.tabEntrepot[c.posX - 1, c.posY - 1] = -1;
            if (compteurCharMan==Convert.ToInt32(numericUpDown1.Value))
            {
                btn_calcul_temps.Enabled = true;
                btn_calcul_distance.Enabled = true;
                btn_ValiderPos.Enabled = false;
                textBoxX.Enabled = false;
                textBoxY.Enabled = false;
                countNbCharMan.Text = (compteurCharMan).ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e) // placement aléatoire des objets
        {
            tbObjChois.Clear();
            listBoxObj.Items.Clear();
            btn_calcul_temps.Enabled = true;
            btn_calcul_distance.Enabled = true;      
            Random rd = new Random();
            int NBO = Convert.ToInt32(numericUpDown2.Value);
            tabObjet = new Objet[NBO];

            for (int i = 0; i < tabObjet.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                int orientation = rd.Next(0, 2);
                int hauteur = rd.Next(1, 6);
                while (GenericNode.tabEntrepot[posX-1, posY-1] != -1) //on utilise le tab du Node distance !
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
                }
                if(orientation==0) { tabObjet[i] = new Objet(posX, posY, Objet.Orientation.Nord, hauteur); }
                else tabObjet[i] = new Objet(posX, posY,Objet.Orientation.Sud,hauteur);
            }

            foreach (Objet o in tabObjet)
            {
                listBoxObj.Items.Add(o);
            }
        }


        private void reinitialiserView()
        {
            
            label_error.Visible = false;
            groupBox2.Visible = false;
            button_nouveau1.Visible = true;
            if(nodeTemps)
            {
                label17.Visible = true;
                lbTimeTot.Visible = true;
            }
        }

        private void button_nouveau_Click(object sender, EventArgs e)
        {
            listBoxChar.Items.Clear();
            textBoxPosChar.Clear();
            listBoxObj.Items.Clear();
            tbObjChois.Clear();
            tabEntrepot = GenericNode.InitialiserEntrepot();
            label_error.Visible = false;
            groupBox2.Visible = true;
            button_nouveau1.Visible = false;
            compteurCharMan = 0;
            compteurObjMan = 0;
            label8.Visible = false;
            lbCountObj.Visible = false;
            countNbCharMan.Visible = false;
            label11.Visible = false;
            label17.Visible = false;
            lbTimeTot.Visible = false;

        }

        public double CalculTempsTot()
        {
            int a = Lres.Count();
            double TempsTot = Lres[a-1].Cout_Total + objet.hauteur * 2 + 10;
            return TempsTot;
        }

        private void listBoxObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxObj.SelectedItem!=null)
            {
                tbObjChois.Text = listBoxObj.SelectedItem.ToString();
                objChoisi = (Objet)listBoxObj.SelectedItem;
                label_error.Visible = false;
            }
            
        }

        private void btnAjoutObj_Click(object sender, EventArgs e)
        {
            compteurObjMan++;
            Objet.Orientation orientation;
            if(cbOrientationObj.SelectedItem=="Nord")
            {
                orientation = Objet.Orientation.Nord;
            }
            else
            {
                orientation = Objet.Orientation.Sud;
            }
            Objet o = new Objet(int.Parse(textBoxXObj.Text), int.Parse(textBoxYObj.Text), orientation, int.Parse(tbHauteur.Text));


            listBoxObj.Items.Add(o);
            lbCountObj.Text = (compteurObjMan + 1).ToString();
            textBoxXObj.Clear();
            textBoxYObj.Clear();
            tbHauteur.Clear();
            cbOrientationObj.Items.Clear();
            tabObjet[compteurObjMan-1] = o;
            if (compteurObjMan == Convert.ToInt32(numericUpDown2.Value))
            {
                btn_calcul_temps.Enabled = true;
                btn_calcul_distance.Enabled = true;
                btnAjoutObj.Enabled = false;
                textBoxXObj.Enabled = false;
                textBoxYObj.Enabled = false;
                cbOrientationObj.Enabled = false;
                tbHauteur.Enabled = false;
                lbCountObj.Text = (compteurObjMan).ToString();
            }

        } // bouton ajout objet manuel

        private void btnManuelObj_Click(object sender, EventArgs e)//bouton placement objet manuel
        {
            tbObjChois.Clear();
            btnAjoutObj.Enabled = true;
            textBoxXObj.Enabled = true;
            textBoxYObj.Enabled = true;
            cbOrientationObj.Enabled = true;
            tbHauteur.Enabled = true;
            listBoxObj.Items.Clear();
            cbOrientationObj.Items.Clear();
            cbOrientationObj.Items.Add("Nord");
            cbOrientationObj.Items.Add("Sud");
            label11.Visible = true;
            lbCountObj.Visible = true;
            btn_calcul_temps.Enabled = false;
            btn_calcul_distance.Enabled = false;
            lbCountObj.Text = (compteurObjMan + 1).ToString();
            tabEntrepot = GenericNode.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown2.Value);
            tabObjet = new Objet[NBC];
        }
    }
}
