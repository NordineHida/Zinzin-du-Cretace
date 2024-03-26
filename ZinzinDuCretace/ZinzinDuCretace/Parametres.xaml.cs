using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    /// <author>DIACK Daouda/ HIDA Nordine</author>
    public partial class Parametres : Window
    {
        /// <summary>
        /// Le Jeu
        /// </summary>
        /// <author>Daouda/Nordine</author>
        private LeJeu jeu;

        /// <summary>
        /// Parametre du jeu (de la main window
        /// </summary>
        /// <author>Nordine/Daouda</author>
        private Parametre parametre;

        /// <summary>
        /// Ecran du jeu
        /// </summary>
        /// <author>Nordine</author>
        private MainWindow mainWindow;

        /// <summary>
        /// Constructeur de parametre
        /// </summary>
        /// <param name="parametre">parametre de la mainWindow</param>
        /// <author>Daouda/Nordine</author>
        public Parametres(MainWindow mainWindow)
        {
            //On récupere les éléments sauvegarder dans la mainWindow
            this.mainWindow = mainWindow;
            this.jeu = mainWindow.Jeu;
            this.parametre = mainWindow.Parametre; 

            InitializeComponent();
            //On mets a jour le status des checkbox selon les parametres du jeu
            this.SoundCheckBox.IsChecked = parametre.SoundEffect;
            this.FullscreenCheckBox.IsChecked = parametre.Fullscreen;
            this.SliderVolume.Value = parametre.Volume;

        }

        /// <summary>
        /// Quand la valeur du slider change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parametre.Volume = Math.Round(e.NewValue, 2); // Arrondir à 2 décimales
            jeu.BackgroundVolume = parametre.Volume;
        }

        /// <summary>
        /// Quand on clique sur le bouton valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void ValiderParametre(object sender, RoutedEventArgs e)
        {
            //On reprend le jeu
            jeu.ResumeTimer();
            jeu.Resume();

            //Ecriture en JSON
            using (FileStream stream = File.OpenWrite("parametre.json"))
            {
                DataContractJsonSerializer SaveParametre = new DataContractJsonSerializer(typeof(Parametre));
                SaveParametre.WriteObject(stream, this.parametre);
            }

            //On ferme la fenetre et on repasse sur l'écran de jeu
            this.Close();
            mainWindow.PutFenetreParametresNull();
            mainWindow.Activate();
        }



        /// <summary>
        /// Quand on coche la case Fullscreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void FullscreenOn(object sender, RoutedEventArgs e)
        {
            parametre.Fullscreen = true;
            mainWindow.FullscreenOn();
            this.Activate();
        }

        /// <summary>
        /// Quand on décoche la case fullscreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void FullscreenOff(object sender, RoutedEventArgs e)
        {
            parametre.Fullscreen =false;
            mainWindow.FullscreenOff();
            this.Activate();
        }

        /// <summary>
        /// Quand on coche les effets sonores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void SoundEffectOn(object sender, RoutedEventArgs e)
        {
            parametre.SoundEffect =true ;
        }

        /// <summary>
        /// Quand on décoche les effets sonores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Daouda/Nordine</author>
        private void SoundEffectOff(object sender, RoutedEventArgs e)
        {
            parametre.SoundEffect = false;
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
