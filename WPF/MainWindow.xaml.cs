using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void btnCarte_Click(object sender, RoutedEventArgs e)
        {
            Window2 jeu1 = new Window2();
            jeu1.Show();
        }

        private void btnHasard_Click(object sender, RoutedEventArgs e)
        {
            Window3 jeu2 = new Window3 ();
            jeu2.Show();
        }

        private void btnMémoire_Click(object sender, RoutedEventArgs e)
        {
            Window4 jeu3 = new Window4();
            jeu3.Show();
        }
    }
}
    

