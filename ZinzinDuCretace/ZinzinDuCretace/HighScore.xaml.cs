using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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

namespace ZinzinDuCretace
{
    /// <summary>
    /// Logique d'interaction pour HighScore.xaml
    /// </summary>
    /// <author>Laszlo / Nordine</author>
    [DataContract]
    public partial class HighScore : Window
    {
        /// <summary>
        /// Gamescore du joeur
        /// </summary>
        private GameScore gameScore;

        /// <summary>
        /// Score du joueur
        /// </summary>
        private double score;

        /// <summary>
        /// Constructeur de HighScore
        /// </summary>
        /// <param name="scoreJoueur"></param>
        /// <author>Laszlo</author>
        public HighScore(GameScore scoreJoueur)
        {

            this.gameScore = scoreJoueur;
            this.score = scoreJoueur.Score;
            InitializeComponent();
            this.scorePartie.Content = score;
        }


        /// <summary>
        /// Prend le nom marqué dans la textBox, l'ajoute au dictionnaire avec le score, sérialise, ferme la fenetre et ouvre le tableau trié
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo/Nordine</author>
        private void Valider(object sender, RoutedEventArgs e)
        {
            string nom = TextInput.Text;

            if (gameScore.TryName(nom))
            {
                this.Close();
                HighScoreUpdated hsu = new HighScoreUpdated(this.gameScore);
                // Centrer la fenêtre
                hsu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                hsu.Show();
            }
            else
            {
                // Afficher un message d'erreur ou effectuer une autre action appropriée
                MessageBox.Show(Res.Strings.VeuillezSaisirNom,Res.Strings.NomVide , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 
        

        /// <summary>
        /// Quand on prend le focus sur la textbox, le placeholder disparait et on ecrit en noir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void TextInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TextInput.Text = "";
            TextInput.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Quand on charge la textbox, on met un text faisant office de placeholder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void TextInput_Loaded(object sender, RoutedEventArgs e)
        {
            TextInput.Text = Res.Strings.SaisirNom;
            TextInput.Foreground = Brushes.LightGray;
        }

        /// <summary>
        /// Permet de deplacer la fenetre en maintenant le clique gauche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
