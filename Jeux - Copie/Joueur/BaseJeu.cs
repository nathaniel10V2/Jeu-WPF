using System;

namespace JeuVideo
{
    public abstract class BaseJeu
    {
        protected List<Joueur> listeJoueurs = new List<Joueur>();

        protected int nombreJoueurs = 0;

        private Joueur joueurActif = null;


        public BaseJeu()
        { }
        // Pour faire la liste des joueurs on cree une methode pour que la dimmension de la liste des joueurs dépende du nombre de joueurs(avec le paramètre nombreJoueurs)
        public void InitialiserListeJoueurs(int nombreJoueurs)
        {
            this.nombreJoueurs = nombreJoueurs;

            if (nombreJoueurs > 0)
            {
                listeJoueurs.Capacity = nombreJoueurs;
            }
        }
        // On cree une methode pour ajouter des éléments à listeJoueurs avec le paramètre joueur de type Joueur
        public void AjouterJoueur(Joueur joueur)
        {
            listeJoueurs.Add(joueur);
        }
        // On cree une methode de type Joueur pour renvoie la valeur de joueurActif qui est definie comme étant le premier element de listeJoueurs 
        public Joueur ObtenirPremierJoueur()
        {
            if (listeJoueurs != null)
            {
                joueurActif = listeJoueurs.First();
            }
            return joueurActif;
        }

        public Joueur ObtenirJoueur(int index)
        {
            Joueur joueur = null;
            if (listeJoueurs != null)
            {
                joueur = listeJoueurs.ElementAt(index);
            }
            return joueur;
        }

        // On cree une methode qui renvoie un nouveau joueurActif en fonction de son index dans listeJoueurs
        public Joueur ObtenirJoueurSuivant()
        {
            if (listeJoueurs != null)
            {
                if (joueurActif != null)
                {
                    int index = listeJoueurs.IndexOf(joueurActif);
                    if (index < listeJoueurs.Count - 1)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                    joueurActif = listeJoueurs[index];
                }
            }
            return joueurActif;
        }


        public int ObtenirNombreJoueurs()
        {
            if(listeJoueurs != null)
            {
                return this.listeJoueurs.Count();
            }
            return 0;
        }

        public List<Joueur> ObtenirListeJoueurs()
        {
            return this.listeJoueurs;
        }

        public void ReinitialiserJoueursScore()
        {
            foreach (var joueur in listeJoueurs)
            {
                joueur.Score = 0;
            }
        }

    }
}