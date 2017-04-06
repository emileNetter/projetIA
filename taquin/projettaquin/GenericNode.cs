using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace projettaquin
{
    // classe abstraite, il est donc impératif de créer une classe qui en hérite
    // pour résoudre un problème particulier en y ajoutant des infos liées au contexte du problème
    abstract public class GenericNode
    {
       // protected string Name;              // DOIT ETRE UNIQUE POUR CHAQUE genericnode !!
        protected double GCost;               //coût du chemin du noeud initial jusqu'à ce noeud
        protected double HCost;               //estimation heuristique du coût pour atteindre le noeud final
        protected double TotalCost;           //coût total (g+h)
        protected GenericNode ParentNode;     // noeud parent
        protected List<GenericNode> Enfants;  // noeuds enfants
        public static int[,] tabEntrepot = new int[25, 25];


        public GenericNode()
        {
            ParentNode = null;
            Enfants = new List<GenericNode>();
        }


        public static int[,] InitialiserEntrepot()
        {
            for (int l = 0; l < tabEntrepot.GetLength(0); l++)
                for (int m = 0; m < tabEntrepot.GetLength(1); m++) { tabEntrepot[l, m] = 0; }

            for (int i = 2; i < 24; i += 2)
            {

                for (int premiereLigne = 2; premiereLigne < 11; premiereLigne++) { tabEntrepot[premiereLigne, i] = -1; }
                for (int deuxiemeLigne = 14; deuxiemeLigne < 23; deuxiemeLigne++) { tabEntrepot[deuxiemeLigne, i] = -1; }

            }

            for (int j = 0; j < 25; j++)
            {
                tabEntrepot[0, j] = -2;
            }
            return tabEntrepot;
        }

        public double GetGCost()
        {
            return GCost;
        }

        public void SetGCost(double g)
        {
            GCost = g;
        }

        public double Estimation()
        {
            return HCost;
        }

        public void SetEstimation(double h)
        {
            HCost = h;
        }


        public double Cout_Total
        {
            get { return TotalCost; }
            set { TotalCost = value; }
        }

        public List<GenericNode> GetEnfants()
        {
            return Enfants;
        }

        public GenericNode GetNoeud_Parent()
        {
            return ParentNode;
        }

        public void SetNoeud_Parent(GenericNode g)
        {
            ParentNode = g;
            g.Enfants.Add(this);
        }

        public void Supprime_Liens_Parent()
        {
            if (ParentNode == null) return;
            ParentNode.Enfants.Remove(this);
            ParentNode = null;
        }

        public void calculCoutTotal()
        {
            TotalCost = GCost + HCost;
        }

  

        // Méthodes abstrates, donc à surcharger obligatoirement avec override dans une classe fille
        public abstract bool IsEqual(GenericNode N2);
        public abstract double GetArcCost(GenericNode N2);
        public abstract bool EndState(Objet obj);
        public abstract List<GenericNode> GetListSucc();
        public abstract void CalculeHCost(Objet objet);
        // On peut aussi penser à surcharger ToString() pour afficher correctement un état
        // c'est utile pour l'affichage du treenode
    }
}
