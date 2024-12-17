using System;
using System.Diagnostics;
using System.Reflection;

namespace JeuVideo
{
    public class Hasard : BaseJeu
    {
        bool finJeu = false;

        int scoreVainqueur = 20;

        Joueur? joueurVainqueur = null;

        static void Main(string[] args) { }

        public int LancerDé()
        {
            Random dé = new Random();
            int numéroDé = dé.Next(1, 7);
            return numéroDé;
        }

        public void VerifierScore(Joueur joueur)
        {
            if (joueur.Score >= scoreVainqueur)
            {
                joueur.Score = scoreVainqueur;
                joueurVainqueur = joueur;
                finJeu = true;
            }
            else if (joueur.Score < scoreVainqueur)
            {
                finJeu = false;
            }
        }

        public void AugmenterScoreJoueur(Joueur joueur, int numéroDé)
        {
            joueur.Score = joueur.Score + numéroDé;
            VerifierScore(joueur);
        }

        public bool FinDeJeu()
        {
            return finJeu;
        }

        public string DeterminerJoueurGagnant()
        {
            if (joueurVainqueur != null)
            {
                var message = "Le vainqueur du jeu est: " + joueurVainqueur.Name;
                return message;
            }
            return null;
        }
    }
}
