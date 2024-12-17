using System;

namespace JeuVideo
{
    public class Joueur
    {
        public string Name { get; set; }
        public string ?Color { get; set; }
        public int Score { get; set; }

        public Joueur(string name)
        {
            Name = name;
            Score = 0;
        }
        public Joueur(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public Joueur(string name, string color, int score)
        {
            Name = name;
            Color = color;
            Score = score;
        }
    }
}