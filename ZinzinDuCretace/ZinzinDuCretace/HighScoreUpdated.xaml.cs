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
using static System.Formats.Asn1.AsnWriter;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Logique d'interaction pour HighScoreUpdated.xaml
    /// </summary>
    /// <author>Laszlo/Nordine</author>
    public partial class HighScoreUpdated : Window
    {

        /// <summary>
        /// Constructeur de HighScoreUpdate
        /// </summary>
        /// <author>Laszlo/Nordine</author>
        public HighScoreUpdated(GameScore scoreJoueur)
        {
            InitializeComponent();
            //Récupere les scores sérialisés (le fichier existe forcement puisque pour acceder ici le joueur a forcement saisi un nom, donc creer un fichier score.json)
            List<(string nom, double score)> highScoreUpdatedSorted = scoreJoueur.DeserialiserScores();

            //Afiche les 6 premiers
            for (int i = 0; i < highScoreUpdatedSorted.Count; i++)
            {
                if (i <= 6)
                {
                    //Affichage des noms
                    string labelName = "nom" + i.ToString();
                    var label = FindName(labelName) as Label;

                    if (label != null)
                    {
                        label.Content = highScoreUpdatedSorted.ElementAt(i).nom;
                    }

                    //Affichage de score
                    string labelScore = "score" + i.ToString();
                    var labelscore = FindName(labelScore) as Label;

                    if (labelscore != null)
                    {
                        labelscore.Content = highScoreUpdatedSorted.ElementAt(i).score;
                    }
                }

            }
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Laszlo</author>
        private void Fermer(object sender, RoutedEventArgs e)
        {
            this.Close();
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
