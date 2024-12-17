using System;
using System.Diagnostics;

namespace JeuVideo
{
    public class JeuDeCartes : BaseJeu
    {

        int[] cartes = new int[7] { 1, 2, 3, 4, 5, 6, 7 };

        int carteJoué = 0;

        Random rdn = new Random();
        
        public void AttribuerCartes()
        {
            foreach (var joueur in listeJoueurs)
            {
                JoueurCarte joueurCarte = (JoueurCarte)joueur;
                int[] cartesJoueur = cartes.OrderBy(x => rdn.Next()).ToArray();
                joueurCarte.cartes.AddRange(cartesJoueur);
            }
        }

        public int ObtenirCarteJoué(JoueurCarte joueur)
        {
            if (joueur.cartes.Count > 0)
            {
                carteJoué = joueur.cartes.First();
                joueur.carteJoué = carteJoué;
                joueur.cartes.RemoveAt(0);
                return carteJoué;
            }
            return 0;
        }

        public void AugmenterScoreJoueurs()
        {

            List<Joueur> listeRangJoueurs = listeJoueurs.OrderByDescending(x => ((JoueurCarte)x).carteJoué).ToList() as List<Joueur>;
            foreach (var joueur in listeJoueurs)
            {
                Trace.WriteLine(joueur.Name + " a joué la carte: " + ((JoueurCarte)joueur).carteJoué);
                Trace.WriteLine("count = " + ((JoueurCarte)joueur).cartes.Count);
            }
            
            if (listeRangJoueurs != null)
            {
                JoueurCarte joueurGagnantPoint = (JoueurCarte)listeRangJoueurs.First();
                int carteGagnante = joueurGagnantPoint.carteJoué;
                int nombreGagnant = 0;

                foreach (var joueur in listeRangJoueurs)
                {
                    int carteJoueur = ((JoueurCarte)joueur).carteJoué;
                    if (carteGagnante == carteJoueur)
                    {
                        nombreGagnant++;
                    }
                }

                if (nombreGagnant == 1)
                {
                    joueurGagnantPoint.Score++;
                }
            }
        }
        public string DeterminerJoueurGagnant()
        {
            List<Joueur> listeRangJoueurs = listeJoueurs.OrderByDescending(x => x.Score).ToList() as List<Joueur>;
            JoueurCarte joueurGagnant = (JoueurCarte)listeRangJoueurs.First();
            int scoreGagnant = joueurGagnant.Score;
            int nombreGagnant = 0;

            foreach (var joueur in listeRangJoueurs)
            {
                int scoreJoueur = joueur.Score;
                if (scoreGagnant == scoreJoueur)
                {
                    nombreGagnant++;
                }
            }

            if (nombreGagnant > 1)
            {
                string message = "Aucun gagnant pour cette partie";
                return message;
            }
            else
            {
                string message = "Le gagnant du jeu est " + joueurGagnant.Name + " avec le score " + joueurGagnant.Score;
                return message;
            }
        }
    }
}