using JeuVideo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
       JeuMemoire jeu = new JeuMemoire();
       Joueur joueurActif;
       string[] fruitsMélangés;

        public Window4()
        {
            InitializeComponent();
        }
        // Ajoute un nouveau joueur à la liste si un nom valide est saisi
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
                MessageBox.Show("La saisie du nom du joueur est obligatoire");
            }
        }
        // Lance la partie et prépare l'interface de jeu
        private void btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            GestionJoueursPanel.Visibility = Visibility.Hidden;
            JouerPanel.Visibility = Visibility.Visible;

            ReinitialiserJeu();
            jeu.Demarrer();
            string[] fruitsMélangés = jeu.ObtenirFruitsMélangés();
            for (int i = 0; i < fruitsMélangés.Length; i++)
            {
                var item = i + "-"; // Prépare les numéros disponibles
                lbxNumeroDisponibles.Items.Add(item);
            }
            joueurActif = jeu.ObtenirPremierJoueur();
            tbkJoueurActif.Text = joueurActif.Name;

            AfficherJoueursScore();

            tbxPremierNumero.Text = string.Empty;
            tbxDeuxiemeNumero.Text = string.Empty;

        }
        // Supprime un joueur sélectionné de la liste
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            int index = listeJoueurBox.Items.IndexOf(listeJoueurBox.SelectedItem);
            listeJoueurBox.Items.RemoveAt(index);
            List<Joueur> joueurs = jeu.ObtenirListeJoueurs();
            joueurs.RemoveAt(index - 1);
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            JouerPanel.Visibility = Visibility.Hidden;
            GestionJoueursPanel.Visibility = Visibility.Visible;
            ReinitialiserJeu();
        }


        private void btnRejouer_Click(object sender, RoutedEventArgs e)
        {
            btn_Valider_Click(sender, e);
        }
        // Passe au joueur suivant après validation de la saisie
        private void btnJoueurSuivant_Click(object sender, RoutedEventArgs e)
        {
            string premierNumero = tbxPremierNumero.Text;
            string deuxiemeNumero = tbxDeuxiemeNumero.Text;

            if ((premierNumero != string.Empty) && (deuxiemeNumero != string.Empty))
            {
                string message = jeu.ValiderSaisieJoueur(joueurActif, premierNumero, deuxiemeNumero); // Valide la saisie du joueur
                if (message != null) 
                {
                    MessageBox.Show(message);
                }
                else
                {
                    // Récupère et affiche le premier fruit choisi
                    int numero = Convert.ToInt32(premierNumero);
                    string premierFruit = jeu.ObtenirFruitsSaisi(numero);
                    tbkPremierFruitChoisi.Text = joueurActif.Name + " a joué " + premierFruit;

                    // Récupère et affiche le deuxième fruit choisi
                    numero = Convert.ToInt32(deuxiemeNumero);
                    string deuxiemeFruit = jeu.ObtenirFruitsSaisi(numero);
                    tbkDeuxiemeFruitChoisi.Text = joueurActif.Name + " a joué " + deuxiemeFruit;

                    joueurActif = jeu.ObtenirJoueurSuivant();
                    tbkJoueurActif.Text = joueurActif.Name;

                    tbxPremierNumero.Text = string.Empty;
                    tbxDeuxiemeNumero.Text = string.Empty;

                    if (premierFruit == deuxiemeFruit) // Si les fruits correspondent
                    {
                        lbxNumeroDisponibles.Items.Clear();
                        string[] fruitsMélangés = jeu.ObtenirFruitsMélangés();
                        for (int i = 0; i < fruitsMélangés.Length; i++)
                        {
                            var item = i + "-"; // Prépare les nouveaux numéros
                            lbxNumeroDisponibles.Items.Add(item); // Met à jour la liste
                        }
                        AfficherJoueursScore();
                        if (fruitsMélangés.Length == 0) // Si tous les fruits ont été trouvés
                        {
                            AfficherJoueursGagnant();
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("La saisie des deux numéros est obligatoire.");
            }
        }
        // Affiche le score actuel de tous les joueurs
        private void AfficherJoueursScore()
        {
            lbxScoreJoueurs.Items.Clear();
            List<Joueur> listJoueurs = jeu.ObtenirListeJoueurs();
            foreach (var joueur in listJoueurs)
            {
                string joueurScore = joueur.Name + " - " + joueur.Score;
                lbxScoreJoueurs.Items.Add(joueurScore);
            }
        }

        private void AfficherJoueursGagnant()
        {
            Joueur joueur = jeu.DéterminerJoueurGagnant();
            if (joueur != null)
            {
                string message = string.Format("Le joueur gagnant de cette partie est " + joueur.Name + " avec le score " + joueur.Score);
                tbkJoueurGagnant.Text = message;
            }
        }
        // Réinitialise l'état du jeu pour une nouvelle partie
        private void ReinitialiserJeu()
        {
            jeu.ReinitialiserJoueursScore();
            lbxNumeroDisponibles.Items.Clear();
            lbxScoreJoueurs.Items.Clear();
            tbkJoueurActif.Text = string.Empty;
            tbxDeuxiemeNumero.Text = string.Empty;
            tbxPremierNumero.Text = string.Empty;
            tbkJoueurGagnant.Text = string.Empty;
            tbkPremierFruitChoisi.Text = string.Empty;
            tbkDeuxiemeFruitChoisi.Text = string.Empty;
        }

    }
}
