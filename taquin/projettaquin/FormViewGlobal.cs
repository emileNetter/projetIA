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
        private Objet[] tabObjet = null;//permet de stocker les différents objets saisies dans le form
        private Chariot[] tabChariot = null;// permet de stocker les chariots saisis dans le form
        private FormeRectangle[,] tabForme = null;// permet de stocker les formes de la grille
        public int[,] tabEntrepot; //on stocke les données de la grille 
        private static int compteurCharMan = 0;// compteur du nombre de chariots choisis
        private static int compteurObjMan = 0;// compteur du nbre d'objets choisis

        private static Chariot charChoisi;
        private static Objet objChoisi;

        private static List<GenericNode> Lres;//Liste finale des noeuds appartenant au chemin optimal pour récupérer l'objet
        private static List<GenericNode> TrajectoireF = new List<GenericNode>(); // Liste des noeuds amenant à une case verte
        private static List<List<GenericNode>> EnsembleTrajectoiresF = new List<List<GenericNode>>();// Stockage de l'ensemble des listes de noeuds amenant à une case verte
        List<GenericNode> bestTrajectoire = new List<GenericNode>();// Liste finale des noeds appartenant au chemin optimal pour retourner sur la case verte

        private Objet objet;
        private Graph g;
        private GenericNode Ninit; // Noeud correspondant à la position du chariot
        private GenericNode Nobj;//Noeud correspondant à la position de l'objet

      
        

        private static Boolean nodeTemps;//Boolean pour savoir si il s'agit d'un nodeTemps ou d'un nodeDistance. Pas meme traitement en fonction
       

        public FormViewGlobal()
        {
            InitializeComponent();
            numericUpDown1.Value = 3;
            numericUpDown2.Value = 1;
            
           
        }


        public void setViewEntrepot()//fonction permettant la mise en place de la view de l'entrepot
        {
            Refresh();
            
            this.Height = 800;
            this.Width = 1000;
            
            //Définition de la position des cases 
            setCaseView();
            setGrilleView(); //Definition des couleurs de la grille

            
        } 

        protected override void OnPaint(PaintEventArgs e) // fonction dessinant l'ensemble de la grille et des chemins parcourus par le chariot 
        {
            base.OnPaint(e);
            if (tabForme != null)
            {
                for (int i = 0; i < tabEntrepot.GetLength(0); i++)//Pour toute les formes dans tabForme
                {
                    for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                    {
                        FormeRectangle.creationFormeColorée(tabForme[i, j], this);//On dessine la forme avec sa taille et sa couleur passée au préalable dans setGrilleView()
                    }
                }
                foreach(Chariot c in tabChariot)//Pour tout les chariots on les dessine sur la grille
                {
                    int positionX= 25*c.posX;
                    int positionY= 25*c.posY;
                    FormeRectangle chariot = new FormeRectangle("black", positionX, positionY);//Un chariot est un carré noir sur la grille.
                    FormeRectangle.creationFormeColorée(chariot, this);
                }
                foreach(Objet o in tabObjet)//Pour tout les objets on les dessine sur la grille
                {
                    int positionX = 25 * o.posX;
                    int positionY = 25 * o.posY;
                    FormeRectangle objet = new FormeRectangle("orange", positionX, positionY);// Un objet est un carré orange sur la grille
                    FormeRectangle.creationFormeColorée(objet, this);
                }
                // Colorie le chemin du noeud initial jusqu'à l'objet
                
                    if (Lres != null)
                    {
                        foreach (GenericNode n in Lres)
                        {
                            int positionX = 0;
                            int positionY = 0;
                            if (nodeTemps)// Si on gère des nodeTemps ou des NodeDistance 
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


                            FormeRectangle objet = new FormeRectangle("red", positionX, positionY);//colorie la case de la grille correspondnat à chaque noeud en rouge; le chemin suivi par le chariot apparait tout en rouge
                            FormeRectangle.creationFormeColorée(objet, this);
                        //}
                    }

                   //Colore le chemin retour : de l'objet à une des cases 1 la plus proche de la grille
                    if (bestTrajectoire != null)
                    {
                        foreach (GenericNode n in bestTrajectoire)
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
                            FormeRectangle objet = new FormeRectangle("purple", positionX, positionY);
                            FormeRectangle.creationFormeColorée(objet, this);
                        }
                    }
                }
            }

        }  

      

        

        private void setCaseView()//Dessine les cases des abscisses et ordonnes 
        {
            for (int i = 0; i < 26; i++)
            {
                Label labelX = new Label();
                Label labelY = new Label();

                labelX.Parent = this;
                labelX.Location = new Point(25 * i, 0);
                labelX.BorderStyle = BorderStyle.FixedSingle;
                labelX.Text = i.ToString();
                labelX.TextAlign = ContentAlignment.MiddleCenter;
                labelX.Size = new Size(25, 25);
                labelX.Name = "labelX" + i;
                this.Controls.Add(labelX);


                labelY.Parent = this;
                labelY.BorderStyle = BorderStyle.FixedSingle;
                labelY.Location = new Point(0, 25 * i);
                labelY.Text = i.ToString();
                labelY.TextAlign = ContentAlignment.MiddleCenter;
                labelY.Size = new Size(25, 25);
                labelY.Name = "labelY" + i;
                this.Controls.Add(labelY);


            }
        }

        private void setGrilleView()// Dessine les cases de la grille
        {
            tabForme = new FormeRectangle[25, 25];
            string color = "";
            if (tabEntrepot == null) { tabEntrepot = GenericNode.InitialiserEntrepot(); }
            else
            {

                for (int i = 0; i < tabEntrepot.GetLength(0); i++)// On récupère tout les éléments du tableau et suivant les valeurs passées dans GenericNode.InitialiserEntrepot on définit la couleur correspondante à la case.
                {
                    for (int j = 0; j < tabEntrepot.GetLength(1); j++)
                    {
                        int positionX = 25 + i * 25;
                        int positionY = 25 + j * 25;
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

        private void reinitialiserView()// On appelle cette méthode lorqu'on appuie sur un des boutons de calcul. Permet de faire disparaitre tout les boutons de base du formulaire
        {

            label_error.Visible = false;
            groupBox2.Visible = false;
            button_nouveau1.Visible = true;
            if (nodeTemps)/// Si on cherche à trouve le temps le plus court, on affiche le temps total.
            {
                label17.Visible = true;
                lbTimeTot.Visible = true;
            }
        }

        private double CalculTempsTot()//Calcul du temps total 
        {
            
            int b = bestTrajectoire.Count();
            double TempsTot = bestTrajectoire[b-1].Cout_Total+ objet.hauteur * 2 + 10;//On récupère le temps total mis(stocké sur le dernier noeud de la liste) puis on y ajoute le temps du retour, puis le temps mis pour récupérer l'objet
            return TempsTot;
        }


        private void button_placement_manuel_chariot_Click(object sender, EventArgs e) //Traitement Bouton placement manuel chariot
        {
            textBoxPosChar.Clear();
            listBoxChar.Items.Clear();
            textBoxX.Enabled = true;
            textBoxY.Enabled = true;
            btn_ajout_chariot.Enabled = true;
            label8.Visible = true;
            countNbCharMan.Visible = true;
            btn_calcul_temps.Enabled = false;
            btn_calcul_distance.Enabled = false;
            countNbCharMan.Text = (compteurCharMan + 1).ToString();
            tabEntrepot = GenericNode.InitialiserEntrepot(); // On initialise le tableau "source"
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];//On définit le nombre de chariot à rentrer


        }

        private void button_placement_alea_chariot_Click(object sender, EventArgs e) // Bonton placement aléaoire chariot
        {
            textBoxPosChar.Clear();
            btn_calcul_temps.Enabled = true;
            btn_calcul_distance.Enabled = true;
            listBoxChar.Items.Clear();
            tabEntrepot = GenericNode.InitialiserEntrepot(); // On initialise le tableau "source"

            Random rd = new Random();
            int NBC = Convert.ToInt32(numericUpDown1.Value);
            tabChariot = new Chariot[NBC];//On initialise le tableau avec la taille de NBC 

            for (int i = 0; i < tabChariot.Length; i++)
            {
                int posX = rd.Next(1, 25);
                int posY = rd.Next(1, 25);
                //On définit des position aléatoires tant que les valeurs correspondantes aux positions ne sont pas égales à 0 : un chariot ne doit pas être sur un obstacle 
                while (GenericNode.tabEntrepot[posX - 1, posY - 1] < 0) // -1 car le tableau est décalé
                {
                    posX = rd.Next(1, 25);
                    posY = rd.Next(1, 25);
                }
                tabChariot[i] = new Chariot(posX, posY);//On crée un nouveau chariot que l'on stocke dans le tableau
                GenericNode.tabEntrepot[posX - 1, posY - 1] = -1; //on remplace la valeur dans notre tableau global de l'entrepot correspondante aux positions du chariot  par -1 pour pas qu'un chariot puisse apparaitre sur un chariot prédéfini et que tout chariot soit considéré comme un obstacle
            }

            foreach (Chariot c in tabChariot)
            {
                listBoxChar.Items.Add(c);//On ajoute chaque chariot à la listBox pour pouvoir par la suite en séléctionner un.
            }
        }

        private void btn_valider_Click(object sender, EventArgs e) // Methode pour bouton Calcul
        {


            if (objChoisi != null & charChoisi != null)
            {


                objet = new Objet(objChoisi.posX - 1, objChoisi.posY - 1, objChoisi.orientation, objChoisi.hauteur);//On définit l'objet avec les valeurs récupérés dans l'éditText (objet choisi)
                g = new Graph(objet);

                if (sender.Equals(btn_calcul_distance))//Si on clique sur le bouton calcul distance
                {
                    Ninit = new NodeDistance(charChoisi.posX - 1, charChoisi.posY - 1); // On définit la position du chariot choisi comme la position Initiale d'un Noeud distance
                    nodeTemps = false;

                }
                else if (sender.Equals(btn_calcul_temps))
                {
                    Ninit = new NodeTemps(charChoisi.posX - 1, charChoisi.posY - 1, new Point(0, 0));
                    nodeTemps = true;
                }

                Lres = g.RechercheSolutionAEtoile(Ninit);//On calcule la liste des noeuds répondant à la distance la plus courte ou au temps le plus court

                if (nodeTemps)// Nobj sera un nodeTemps, on va chercher à avoir le cout en temps 
                {
                    // Trajet vers la zone finale
                    Nobj = (NodeTemps)Lres[Lres.Count - 1]; //Noeud sur lequel est le chariot lorsqu'il prend l'objet
                }
                else//Nobj sera un nodeDistance, on va chercher à avoir le cout en distance
                {
                    Nobj = (NodeDistance)Lres[Lres.Count - 1]; //Noeud sur lequel est le chariot lorsqu'il prend l'objet
                }

                List<Objet> zoneFinale = new List<Objet>(tabEntrepot.GetLength(0));
                for (int k = 0; k < tabEntrepot.GetLength(0) - 1; k++)
                {
                    Objet o = new Objet(0, k - 1, Objet.Orientation.Sud, 0);//On imagine un objet correspondant à la ligne 1.
                    zoneFinale.Add(o);//On ajoute à la liste des zones finales.
                }

                foreach (Objet o in zoneFinale)
                {
                    Graph g = new Graph(o);
                    TrajectoireF = g.RechercheSolutionAEtoile(Nobj);//On stocke la liste de noeuds correspondant à chaque chemin pour aller vers une des zones finales et on le stocke dans la liste
                    EnsembleTrajectoiresF.Add(TrajectoireF);//On ajoute cette trajectoire à l'ensemble des trajectoires
                }

                bestTrajectoire = EnsembleTrajectoiresF[0];
                double cout = 1000000;
                foreach (List<GenericNode> l in EnsembleTrajectoiresF)
                {
                    double c = l[l.Count - 1].Cout_Total;// Pour chaque liste de chemins dans l'ensemble des trajectoires, on récupère le cout total.
                    if (c < cout)
                    {
                        cout = c;
                        bestTrajectoire = l;//On garde la trajectoire où le cout est le plus faible
                    }


                }

                if (nodeTemps)
                {
                    lbTimeTot.Text = CalculTempsTot().ToString() + " secondes";// On appelle la méthode pour calculer le temps et on l'affiche dans un label.
                }


                if (Lres.Count > 1)
                {
                    Lres.RemoveAt(0); //On supprime le premier noeud correspondant à la position du chariot
                }
                reinitialiserView();// On reinitialise la View
                setViewEntrepot();// On dessine la grille





            }

            else
            {
                label_error.Visible = true;
            }



        }


        private void listBoxChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxChar.SelectedItem != null)
            {
                textBoxPosChar.Text = listBoxChar.SelectedItem.ToString();
                label_error.Visible = false;
                charChoisi = (Chariot)listBoxChar.SelectedItem;
            }

        }// Methode pour sélection du chariot dans la listBox

        private void btn_ajout_chariot_Click(object sender, EventArgs e) // bouton AjoutChariot
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
                btn_ajout_chariot.Enabled = false;
                textBoxX.Enabled = false;
                textBoxY.Enabled = false;
                countNbCharMan.Text = (compteurCharMan).ToString();
            }

        }

        private void button_placement_alea_obj_Click(object sender, EventArgs e) // placement aléatoire des objets
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

        private void button_nouveau_Click(object sender, EventArgs e)
        {
            listBoxChar.Items.Clear();
            textBoxPosChar.Clear();
            listBoxObj.Items.Clear();
            tbObjChois.Clear();
            tabEntrepot = GenericNode.InitialiserEntrepot();
            GenericNode Ninit; // Noeud correspondant à la position du chariot
            GenericNode Nobj;//Noeud correspondant à la position de l'objet
            List<GenericNode> Lres;
            List<GenericNode> bestTrajectoire;
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

        }// Bouton pour pouvoir faire une nouvelle simulation

        private void listBoxObj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxObj.SelectedItem!=null)
            {
                tbObjChois.Text = listBoxObj.SelectedItem.ToString();
                objChoisi = (Objet)listBoxObj.SelectedItem;
                label_error.Visible = false;
            }
            
        }// Methode pour séléection de l'objet dans la listBox

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

        private void button_placement_manuel_obj_Click(object sender, EventArgs e)//bouton placement objet manuel
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
