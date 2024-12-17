using System;

namespace JeuVideo
{
    public  class JoueurCarte : Joueur
    {
        public List<int> cartes = new List<int>();

        public int carteJoué = 0;

        public JoueurCarte(string name) : base(name)
        {

        }
    } 
}