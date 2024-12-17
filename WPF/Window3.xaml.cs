using JeuVideo;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        Hasard jeu = new Hasard();
        Joueur joueurActif;

        public Window3()
        {
            InitializeComponent();
        }
        // Ajoute un joueur à la liste des joueurs si un nom valide est fourni
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string nomJoueur = tbxSaisie.Text;
            if ((nomJoueur != null) && (nomJoueur.Length > 0))
            {
                Joueur joueur = new Joueur(nomJoueur);
                jeu.AjouterJoueur(joueur);
                listeJoueurBox.Items.Add(joueur.Name);
                tbxSaisie.Clear();
            }
            else
            {
                // Affiche un message si la saisie est vide
                MessageBox.Show("La saisie du nom du joueur est obligatoire");
            }
        }
        // Supprime le joueur sélectionné dans la liste
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = listeJoueurBox.Items.IndexOf(listeJoueurBox.SelectedItem);
            listeJoueurBox.Items.RemoveAt(index);
            List<Joueur> joueurs = jeu.ObtenirListeJoueurs();
            joueurs.RemoveAt(index-1);
        }
        // Commence une nouvelle partie
        private void btn_Commencer_Click(object sender, RoutedEventArgs e)
        {
            GestionJoueursGrille.Visibility = Visibility.Hidden;
            JeuGrille.Visibility = Visibility.Visible;

            ReinitialiserJeu();

            joueurActif = jeu.ObtenirPremierJoueur();
            tbkJoueurActif.Text = joueurActif.Name;

            AfficherJoueursCaseActuelle(); // Met à jour l'affichage des positions des joueurs
        }
        // Gère le clic sur le bouton pour lancer le dé
        private void btnLancerDé_Click(object sender, RoutedEventArgs e)
        {
            btnLancerDé.IsEnabled = false;
            btnSuivant.IsEnabled = true;
            if (!jeu.FinDeJeu())
            {
                int numeroDé = jeu.LancerDé();
                AfficherDé(numeroDé); // Affiche l'image correspondant au numéro obtenu
                jeu.AugmenterScoreJoueur(joueurActif,numeroDé);
                AfficherJoueursCaseActuelle();
            }
            if (jeu.FinDeJeu())
            {
                btnLancerDé.IsEnabled = false;
                btnSuivant.IsEnabled = false;
                string message = jeu.DeterminerJoueurGagnant();
                if (message != null ) 
                {
                    tbkGagnant.Text = message;
                }
            }
        }
        // Passe au joueur suivant
        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            joueurActif = jeu.ObtenirJoueurSuivant();
            tbkJoueurActif.Text = joueurActif.Name;
            btnLancerDé.IsEnabled = true;
            btnSuivant.IsEnabled = false;
            imgNuméroDé.Source = null;

        }
        // Affiche la case actuelle de chaque joueur
        private void AfficherJoueursCaseActuelle()
        {
            lbxJoueursCases.Items.Clear();
            List<Joueur> listJoueurs = jeu.ObtenirListeJoueurs();
            foreach (var joueur in listJoueurs)
            {
                string joueurScore = joueur.Name + " est à la case " + joueur.Score;
                lbxJoueursCases.Items.Add(joueurScore);
            }
        }
        // Réinitialise l'état du jeu
        private void ReinitialiserJeu()
        {
            tbkGagnant.Text = string.Empty;
            jeu.ReinitialiserJoueursScore();
            AfficherJoueursCaseActuelle();
            if (joueurActif != null)
            {
                jeu.VerifierScore(joueurActif);
            }
            imgNuméroDé.Source = null;
            btnLancerDé.IsEnabled = true;
            btnSuivant.IsEnabled = false;

        }

        private void btnRejouer_Click(object sender, RoutedEventArgs e)
        {
            ReinitialiserJeu();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            JeuGrille.Visibility = Visibility.Hidden;
            GestionJoueursGrille.Visibility = Visibility.Visible;
            ReinitialiserJeu();
        }

        private void AfficherDé(int numeroDé)
        {
            string nomImage = null;
            switch (numeroDé)
            {
                case 1:
                    nomImage = "/image/dé_Numéro_1.png";
                    break;
                case 2:
                    nomImage = "/image/dé_Numéro_2.png";
                    break;
                case 3:
                    nomImage = "/image/dé_Numéro_3.png";
                    break;
                case 4:
                    nomImage = "/image/dé_Numéro_4.png";
                    break;
                case 5:
                    nomImage = "/image/dé_Numéro_5.png";
                    break;
                case 6:
                    nomImage = "/image/dé_Numéro_6.png";
                    break;                
                default:
                    break;
            }
            if (nomImage != null)
            {
                Uri fileUri = new Uri(nomImage, UriKind.Relative);
                imgNuméroDé.Source = new BitmapImage(fileUri); // Affiche l'image correspondante
            }
        }
    }
}
