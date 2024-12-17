using JeuVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
       JeuMemoire jeu = new JeuMemoire();
        public Window4()
        {
            InitializeComponent();
        }

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

        private void btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            GestionJoueursPanel.Visibility = Visibility.Hidden;
            JouerPanel.Visibility = Visibility.Visible;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            listeJoueurBox.Items.RemoveAt(listeJoueurBox.Items.IndexOf(listeJoueurBox.SelectedItem));
        }
    }
}
