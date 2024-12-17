using System;

namespace JeuVideo
{
    public class JeuMemoire : BaseJeu
    {

        string[] fruits = null;

        string[] fruitsMélangés = null;

        string[] fruitsTourJoueur = null;

        Random rdn = new Random();


        static void Main(string[] args)
        {
            JeuMemoire jeu = new JeuMemoire();
            jeu.Demarrer();
        }

        public void Demarrer()

        {
            DefinirNombreFruits();
            MélangerFruits();
        }
        private void DefinirNombreFruits()
        {
            int nombreJoueurs = ObtenirNombreJoueurs();
            Console.WriteLine("nombre de joueurs:" + nombreJoueurs);
            if (nombreJoueurs <= 2)
            {
                fruits = new string[10]{"pomme","cerise","poire","banane","orange",
                "pomme","cerise","poire","banane","orange"};
            }
            else
            {
                fruits = new string[16]{"pomme","cerise","poire","banane","orange",
                "pomme","cerise","poire","banane","orange","fraise","ananas","melon","fraise","ananas","melon"};
            }
        }

        private void MélangerFruits()
        {
            for (int i = 0; i < fruits.Length; i++)
            {
                var item = fruits[i];
                var message = i + "-" + item;
                Console.WriteLine(message);
            }

            fruitsMélangés = fruits.OrderBy(x => rdn.Next()).ToArray();
            Console.WriteLine(" ");

            for (int i = 0; i < fruitsMélangés.Length; i++)
            {
                var item = fruitsMélangés[i];
                var message = i + "-";
                Console.WriteLine(message);
            }
        }
        /* méthode pour vérifier que l'utilisateur saisie un numéro compris dans les index du tableau 
        fruitsMélangés (sinon il recommence)*/
        private int SaisirNuméro(string message)
        {
            Console.WriteLine(message);
            var choix = Convert.ToInt32(Console.ReadLine());
            if ((choix >= 0) && (choix < fruitsMélangés.Length))
            {
                return choix;
            }
            else
            {
                Console.WriteLine("Le numéro saisi n'est pas dans la plage autorisé");
                Console.WriteLine("Le numéro saisi doit être compris entre 0 et " + (fruitsMélangés.Length - 1));
                return SaisirNuméro(message);
            }
        }

        public string ValiderSaisieJoueur(Joueur joueur, Object premierNumero, Object deuxiemeNumero)
        {
            var choix1 = Convert.ToInt32(premierNumero);
            var choix2 = Convert.ToInt32(deuxiemeNumero);
            var message = VerifierNumeroSaisi(choix1);
            if (message != null)
            {
                return message;
            }
            message = VerifierNumeroSaisi(choix2);
            if (message != null)
            {
                return message;
            }
            if (choix1 == choix2)
            {
                message = "Les deux numéros saisie doivent être différents";
                return message;
            }
            if (choix1 != choix2)
            {
                fruitsTourJoueur = fruitsMélangés;

                if (fruitsMélangés[choix1] == fruitsMélangés[choix2])
                {
                    joueur.Score++;
                    fruitsMélangés = fruitsMélangés.Where(e => e != fruitsMélangés[choix1]).ToArray();
                }
            }
            return null;
        }

        public string[] ObtenirFruitsMélangés()
        {
            return fruitsMélangés;
        }
        public string ObtenirFruitsSaisi(int numero)
        {
            return fruitsTourJoueur[(int)numero];
        }

        private string VerifierNumeroSaisi(int numero)
        {

            if ((numero >= 0) && (numero < fruitsMélangés.Length))
            {
                return null;
            }
            else
            {
                var message = "Le numéro saisi n'est pas dans la plage autorisé, ce numéro saisi doit être compris entre 0 et " + (fruitsMélangés.Length - 1);
                return message;
            }
        }

        private void TourJoueurs()
        {
            try
            {
                for (int i = 0; i < listeJoueurs.Count(); i++)
                {
                    if (fruitsMélangés.Length == 0)
                    {
                        break;
                    }
                    Console.WriteLine("Tour de: " + listeJoueurs[i].Name);
                    var choix1 = SaisirNuméro("Choisissez le premier numéro ?");
                    Console.WriteLine(listeJoueurs[i].Name + " a choisi: " + fruitsMélangés[choix1]);
                    var choix2 = SaisirNuméro("Choisissez le deuxième numéro ?");

                    if (choix1 == choix2)
                    {
                        while (choix1 == choix2)
                        {
                            choix2 = SaisirNuméro("Choisissez le deuxième numéro ?");
                        }
                    }
                    if (choix1 != choix2)
                    {
                        Console.WriteLine(listeJoueurs[i].Name + " a choisi: " + fruitsMélangés[choix2]);
                        if (fruitsMélangés[choix1] == fruitsMélangés[choix2])
                        {
                            listeJoueurs[i].Score++;
                            Console.WriteLine(listeJoueurs[i].Name + " = " + listeJoueurs[i].Score);
                            fruitsMélangés = fruitsMélangés.Where(e => e != fruitsMélangés[choix1]).ToArray();
                            for (int j = 0; j < fruitsMélangés.Length; j++)
                            {
                                var item = fruitsMélangés[j];
                                var message = j + "-";
                                Console.WriteLine(message);
                            }
                        }
                    }
                }
                if (fruitsMélangés.Length > 0)
                {
                    TourJoueurs();
                }
                else
                {
                    DéterminerJoueurGagnant();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Le numéro saisi n'est pas dans la plage autorisé");
                Console.WriteLine("Le numéro saisi doit être compris entre 0 et " + (fruitsMélangés.Length - 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Joueur DéterminerJoueurGagnant()
        {
            Joueur[] listeRangJoueurs = listeJoueurs.OrderByDescending(x => x.Score).ToArray();
            Joueur joueurGagnant = listeRangJoueurs[0];
            return joueurGagnant;
        }
    }
}