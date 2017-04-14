using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Classification
{
    class Reseau
    {
        private int nbNeurEntrees = 3;
        private int nbNeurCouche;
        private int nbCouches;

        List<Neurone> listeneurones;     // Liste des neurones du réseau
        List<Neurone>[] tabcouches;  // tableau des couches de neurones
        double[,] tabpoids;      // Matrice d'adjacence des poids synaptiques

        public Reseau(int nbNeurCouche,int nbCouches)
        {
            this.nbNeurCouche = nbNeurCouche;
            this.nbCouches = nbCouches;
            Initialisation(this.nbNeurEntrees, this.nbCouches, this.nbNeurCouche);
        }


        private void Initialisation(int nbEntrees, int nbCouches, int nbNeurCouche)
        {
            int i, j, k, cpt;
            Neurone neurone;

            // Initialisation des listes avant de poursuivre
            listeneurones = new List<Neurone>();
            tabcouches = new List<Neurone>[nbCouches];
            for (i = 0; i < nbCouches; i++)
            {
                tabcouches[i] = new List<Neurone>();
            }
            cpt = -1;
            for (i = 0; i < nbEntrees; i++)
            {
                cpt++;
                neurone = new Neurone(cpt, 0);
                listeneurones.Add(neurone);
                tabcouches[0].Add(neurone);
            }
            // On fait les couches cachées :
            for (i = 1; i < nbCouches - 1; i++)
                for (j = 0; j < nbNeurCouche; j++)
                {
                    cpt++;
                    neurone = new Neurone(cpt, i);
                    listeneurones.Add(neurone);
                    tabcouches[i].Add(neurone);
                    // Connexion avec neurones couche précédente
                    for (k = 0; k < tabcouches[i - 1].Count; k++)
                    {
                        tabcouches[i - 1][k].sorties.Add(neurone);
                        neurone.entrees.Add(tabcouches[i - 1][k]);
                    }
                }
            // On fait le neurone de sortie
            cpt++;
            neurone = new Neurone(cpt, nbCouches - 1);
            listeneurones.Add(neurone);
            tabcouches[nbCouches - 1].Add(neurone);
            // Connexion avec neurones couche précédente
            for (k = 0; k < tabcouches[nbCouches - 2].Count; k++)
            {
                tabcouches[nbCouches - 2][k].sorties.Add(neurone);
                neurone.entrees.Add(tabcouches[nbCouches - 2][k]);
            }

            // Initialisation de la matrice des poids synap. : 0= pas de synapse
            int nbneurones = listeneurones.Count;
            tabpoids = new double[nbneurones, nbneurones];
            Random rnd = new Random();
            for (i = 0; i < nbneurones; i++)
                for (j = 0; j < nbneurones; j++)
                    tabpoids[i, j] = rnd.NextDouble() * 2 - 1;

        }

        public void ProcessLayerK(int k)
        {
            int i;
            Neurone neurone;

            for (i = 0; i < tabcouches[k].Count; i++)
            {
                neurone = tabcouches[k][i];
                // Calcul de la somme des entrées pondérées par les poids
                // synaptiques + sigmoïde.
                neurone.CalculeSortie(tabpoids);
            }
        }

        // Calcul nécessaire pour backprop
        private double sommedelta(Neurone neur)
        {
            int i;
            Neurone neuronesucc;
            double somme;

            somme = 0;
            for (i = 0; i < neur.sorties.Count; i++)
            {
                neuronesucc = neur.sorties[i];
                somme = somme + tabpoids[neur.GetNumero(), neuronesucc.GetNumero()]
                                * neuronesucc.Getdelta();
            }
            return somme;
        }

        public void backprop(List<List<double>> lvecteursentrees,
                          List<double> lsortiesdesirees, double alpha, int nbiterations)
        {
            int i, j, k;
            double z;
            Neurone neur, neursucc;
            int nbcouches = tabcouches.Length;
            Random rnd2 = new Random();

            // NbIte est le nombre d'itérations, cad le nombre d'exemples qu vont servir à l'apprentissage
            for (int nbfois = 0; nbfois < nbiterations; nbfois++)
            {
                for (int numexemple = 0; numexemple < lvecteursentrees.Count; numexemple++)
                {
                    // les entrées sont affectées aux sorties des neurones de la couche 0
                    for (i = 0; i < lvecteursentrees[0].Count; i++)
                        tabcouches[0][i].ImposeSortie(lvecteursentrees[numexemple][i]);

                    // On impose ensuite une constante sur le dernier neurone d'entrée
                    neur = tabcouches[0][2];
                    neur.ImposeSortie(1);

                    // Sortie souhaitée :
                    z = lsortiesdesirees[numexemple];

                    // Calcul de la sortie de chaque neurone, couche par couche
                    for (k = 1; k < nbcouches; k++)
                        ProcessLayerK(k);

                    // neur = tabcouches[nbcouches - 1][0]; // sortie du réseau

                    // Calcul du gradient et de la modification de chaque poids
                    //pour chaque neurone de sortie i faire; ici 1 seul neurone de sortie
                    for (i = 0; i < tabcouches[nbcouches - 1].Count; i++)
                    {
                        // ici 1 seul neurone de sortie, i varie entre 0 et 0 !
                        neur = tabcouches[nbcouches - 1][i];
                        neur.Setdelta(neur.Derivee(neur.GetSomme()) * (z - neur.GetSortie()));

                    }

                    // On redescend vers les couches les plus basses
                    for (k = nbcouches - 2; k > -1; k--)
                    {
                        // Pour chaque neurone de cette couche, on met à jour les poids
                        for (j = 0; j < tabcouches[k].Count; j++)
                        {
                            neur = tabcouches[k][j];
                            neur.Setdelta(neur.Derivee(neur.GetSomme()) * sommedelta(neur));
                            // Mise à jour des poids entre j et les neurones i d'arrivée
                            for (i = 0; i < tabcouches[k + 1].Count; i++)
                            {
                                neursucc = tabcouches[k + 1][i];
                                tabpoids[neur.GetNumero(), neursucc.GetNumero()] =
                                  tabpoids[neur.GetNumero(), neursucc.GetNumero()]
                                   + alpha * neur.GetSortie() * neursucc.Getdelta();
                            }
                        }
                    }
                }
            }

        }

        public List<double> ResultatsEnSortie(List<List<double>> lvecteursentrees)
        {
            int i, k;
            Neurone neur;

            List<double> lres = new List<double>();
            // On teste 200 exemples de x pris entre -100 et +100
            // En fait, x2 sera compris entre -100 et 100, x sera utilisé pour l'affichage entre 0 et 200
            for (int numexemple = 0; numexemple < lvecteursentrees.Count; numexemple++)
            {
                // les entrées sont affectées aux sorties des neurones de la couche 0
                for (i = 0; i < lvecteursentrees[0].Count; i++)
                    tabcouches[0][i].ImposeSortie(lvecteursentrees[numexemple][i]);

                // On impose ensuite une constante sur le dernier neurone d'entrée
                neur = tabcouches[0][2];
                neur.ImposeSortie(1);

                // Calcul de la sortie de chaque neurone
                for (k = 1; k < tabcouches.Length; k++)
                    ProcessLayerK(k);
                // Le neurone de sortie est le 1er de la dernière couche
                neur = tabcouches[tabcouches.Length - 1][0];

                lres.Add(neur.GetSortie());
            }
            return lres;
        }
    }
}
