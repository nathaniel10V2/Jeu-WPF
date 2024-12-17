using JeuVideo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace WPF
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        JeuDeCartes jeu = new JeuDeCartes();
        int nombreJoueurs;
        List<Joueur> listJoueurs;
        public Window2()
        {
            InitializeComponent();
        }
        // Ajoute un joueur à la liste des joueurs si les conditions sont remplies
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string nomJoueur = tbxSaisie.Text;
            if ((nomJoueur != null) && (nomJoueur.Length > 0))
            {
                if (listeJoueurBox.Items.Count < 5)
                {
                    JoueurCarte joueur = new JoueurCarte(nomJoueur);
                    jeu.AjouterJoueur(joueur);
                    listeJoueurBox.Items.Add(joueur.Name);
                    tbxSaisie.Clear();
                }
                else
                {
                    // Affiche un message si le nombre maximal de joueurs est atteint
                    MessageBox.Show("Le nombre maximale de joueur est 4");
                }
            }
            else
            {
                // Affiche un message si le champ est vide
                MessageBox.Show("La saisie du nom du joueur est obligatoire");
            }
        }
        // Supprime un joueur sélectionné dans la liste
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = listeJoueurBox.Items.IndexOf(listeJoueurBox.SelectedItem);
            listeJoueurBox.Items.RemoveAt(index);
            listJoueurs = jeu.ObtenirListeJoueurs();
            listJoueurs.RemoveAt(index - 1);
        }
        // Démarre une partie si les conditions sont remplies (au moins 2 joueurs)
        private void btn_Commencer_Click(object sender, RoutedEventArgs e)
        {
            nombreJoueurs = jeu.ObtenirNombreJoueurs();
            listJoueurs = jeu.ObtenirListeJoueurs();
            if (nombreJoueurs < 2)
            {
                MessageBox.Show("Il faut au moins 2 joueurs");
            }
            else
            {
                // Change l'affichage pour montrer la grille du jeu
                GestionJoueursGrille.Visibility = Visibility.Hidden;
                JeuGrille.Visibility = Visibility.Visible;

                ReinitialiserJeu();
                // Configure les éléments visuels en fonction du nombre de joueurs
                switch (nombreJoueurs)
                {
                    case 2:
                        GrilleJoueur3.Visibility = Visibility.Hidden;
                        GrilleJoueur4.Visibility = Visibility.Hidden;
                        AfficherJoueurs(0, tbkPremierJoueur);
                        AfficherJoueurs(1, tbkDeuxièmeJoueur);
                        btnJouer2.IsEnabled = false;
                        break;
                    case 3:
                        GrilleJoueur3.Visibility = Visibility.Visible;
                        GrilleJoueur4.Visibility = Visibility.Hidden;
                        AfficherJoueurs(0, tbkPremierJoueur);
                        AfficherJoueurs(1, tbkDeuxièmeJoueur);
                        AfficherJoueurs(2, tbkTroisièmeJoueur);
                        btnJouer2.IsEnabled = false;
                        btnJouer3.IsEnabled = false;
                        break;
                    case 4:
                        GrilleJoueur3.Visibility = Visibility.Visible;
                        GrilleJoueur4.Visibility = Visibility.Visible;
                        AfficherJoueurs(0, tbkPremierJoueur);
                        AfficherJoueurs(1, tbkDeuxièmeJoueur);
                        AfficherJoueurs(2, tbkTroisièmeJoueur);
                        AfficherJoueurs(3, tbkQuatrièmeJoueur);
                        btnJouer2.IsEnabled = false;
                        btnJouer3.IsEnabled = false;
                        btnJouer4.IsEnabled = false;
                        break;
                    default:
                        break;
                }

                AfficherScoreJoueurs();
            }
        }

        private void btnJouer1_Click(object sender, RoutedEventArgs e)
        {
            btnJouer2.IsEnabled = true;
            btnJouer1.IsEnabled = false;
            JouerCarte(0);
        }

        private void btnJouer2_Click_1(object sender, RoutedEventArgs e)
        {
            if (nombreJoueurs < 3)
            {
                btnJouer1.IsEnabled = true;
            }
            else
            {
                btnJouer3.IsEnabled = true;
            }
            btnJouer2.IsEnabled = false;
            JouerCarte(1);
        }

        private void btnJouer3_Click_1(object sender, RoutedEventArgs e)
        {
            if (nombreJoueurs < 4)
            {
                btnJouer1.IsEnabled = true;
            }
            else
            {
                btnJouer4.IsEnabled = true;
            }
            btnJouer3.IsEnabled = false;
            JouerCarte(2);
        }

        private void btnJouer4_Click_1(object sender, RoutedEventArgs e)
        {
            btnJouer1.IsEnabled = true;
            btnJouer4.IsEnabled = false;
            JouerCarte(3);
        }
        // Gère la logique lorsqu’un joueur joue une carte
        private void JouerCarte(int index)
        {
            JoueurCarte joueur = (JoueurCarte)jeu.ObtenirJoueur(index);
            if (joueur != null)
            {
                if (joueur.cartes.Count < 7 && index == 0)
                {
                    imgCarteJ2.Source = null;
                    imgCarteJ3.Source = null;
                    imgCarteJ4.Source = null;
                }
                int carteJoué = jeu.ObtenirCarteJoué(joueur);
                AfficherCarte(index,carteJoué);
                nombreJoueurs = jeu.ObtenirNombreJoueurs();

                if (index == nombreJoueurs - 1) // Si c'est le dernier joueur
                {
                    jeu.AugmenterScoreJoueurs();
                    AfficherScoreJoueurs(); // Met à jour les scores
                    if (joueur.cartes.Count == 0) // Si un joueur n'a plus de cartes
                    {
                        btnJouer1.IsEnabled = false;
                        btnJouer2.IsEnabled = false;
                        btnJouer3.IsEnabled = false;
                        btnJouer4.IsEnabled = false;
                        // Affiche le joueur gagnant
                        string message = jeu.DeterminerJoueurGagnant();
                        if (message != null)
                        {
                            tbkJoueurGagnant.Text = message;
                        }
                    }
                }
            }
        }

        private void AfficherJoueurs(int index, TextBlock tbk)
        {
            Joueur joueur = jeu.ObtenirJoueur(index);
            if (joueur != null)
            {
                tbk.Text = joueur.Name;
            }
        }

        private void AfficherScoreJoueurs()
        {
            lbxScoreJoueurs.Items.Clear();
            foreach (var joueur in listJoueurs)
            {
                string joueurScore = joueur.Name + " = " + joueur.Score;
                lbxScoreJoueurs.Items.Add(joueurScore);
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            JeuGrille.Visibility = Visibility.Hidden;
            GestionJoueursGrille.Visibility = Visibility.Visible;
            ReinitialiserJeu();
        }

        private void btnRejouer_Click(object sender, RoutedEventArgs e)
        {
            btn_Commencer_Click(sender, e);
        }

        private void ReinitialiserJeu()
        {
            jeu.ReinitialiserJoueursScore();
            foreach (JoueurCarte joueur in listJoueurs)
            {
                joueur.cartes.Clear(); // Vide les cartes des joueurs
            }
            jeu.AttribuerCartes(); // Réattribue les cartes
            lbxScoreJoueurs.Items.Clear();

            imgCarteJ1.Source = null;
            imgCarteJ2.Source = null;
            imgCarteJ3.Source = null;
            imgCarteJ4.Source = null;

            btnJouer1.IsEnabled = true;
            btnJouer2.IsEnabled = true;
            btnJouer3.IsEnabled = true;
            btnJouer4.IsEnabled = true;

            tbkJoueurGagnant.Text = string.Empty;
        }

        private void AfficherCarte(int index, int numero)
        {
            string nomImage = null;
            // Met à jour l'image correspondante
            switch (numero)
            {
                case 1:
                    nomImage = "/image/carte_1.png";
                    break;
                case 2:
                    nomImage = "/image/carte_2.png";
                    break;
                case 3:
                    nomImage = "/image/carte_3.png";
                    break;
                case 4:
                    nomImage = "/image/carte_4.png";
                    break;
                case 5:
                    nomImage = "/image/carte_5.png";
                    break;
                case 6:
                    nomImage = "/image/carte_6.png";
                    break;
                default:
                    nomImage = "/image/carte_7.png";
                    break;
            }
            if (nomImage != null)
            {
                Uri fileUri = new Uri(nomImage, UriKind.Relative);
                switch(index)
                {
                    case 0:
                        imgCarteJ1.Source = new BitmapImage(fileUri);
                        break;
                    case 1:
                        imgCarteJ2.Source = new BitmapImage(fileUri);
                        break;
                    case 2:
                        imgCarteJ3.Source = new BitmapImage(fileUri);
                        break;
                    default :
                        imgCarteJ4.Source = new BitmapImage(fileUri);
                        break;
                }
            }
        }
    }
}
