using IUTGame;
using IUTGame.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZinzinDuCretace.Res;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <author>Nordine</author>
    public partial class MainWindow : Window , IAfficherFenetre, IPointDeVieChanged
    {
        /// <summary>
        /// le jeu
        /// </summary>
        /// <author>Nordine</author>
        private LeJeu jeu;

        /// <summary>
        /// Le jeu (lecture seule)
        /// </summary>
        /// <author>Nordine</author>
        public LeJeu Jeu { get { return jeu; } }


        /// <summary>
        /// Permet de récuperer le score du joueur du jeu
        /// </summary>
        /// <author>Nordine</author>
        public double Score
        {
            get { return jeu.GetScore(); }
        }

        /// <summary>
        /// Parametres du jeu (fullscreen + sound effects)
        /// </summary>
        /// <author>Nordine</author>
        private Parametre parametre;

        /// <summary>
        /// Parametres du jeu (fullscreen + sound effects) (Lecture seule)
        /// </summary>
        /// <author>Nordine</author>
        public Parametre Parametre { get { return parametre; } }

        /// <summary>
        /// Mets a jour l'affichage du score
        /// </summary>
        /// <author>Nordine</author>
        private DispatcherTimer scoreUpdateTimer;

        public MainWindow()
        {
            InitializeComponent();
            // Centrer la fenêtre
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DAOParametre dao = new DAOParametre();
            this.parametre = dao.CreerParametre();
            if (parametre.Fullscreen) { FullscreenOn(); }

            //Affichage point de vie
            SpritePointDeVie = 3;
            UptadeSpritePointDeVie();
        }

        /// <summary>
        /// Lance le jeu + creer le score
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void Jouer(object sender, RoutedEventArgs e)
        {
            if (jeu == null || jeu.Fini)
            {
                WPFScreen screen = new WPFScreen(canvas);
                this.jeu = new LeJeu(screen, this.parametre,this,this);
                this.parametre.Jeu = jeu;
                jeu.Run();

                // Effectuer la liaison de données
                scoreLabel.DataContext = this;
                scoreLabel.SetBinding(ContentProperty, new Binding("Score"));

                // Mettre à jour l'affichage du score
                scoreLabel.Content = Score;

                // Initialiser et démarrer le DispatcherTimer pour la mise à jour du score
                scoreUpdateTimer = new DispatcherTimer();
                scoreUpdateTimer.Interval = TimeSpan.FromMilliseconds(50);
                scoreUpdateTimer.Tick += ScoreUpdateTimer_Tick;
                scoreUpdateTimer.Start();

                //Affichage point de vie
                SpritePointDeVie = 3;
                UptadeSpritePointDeVie();
            }
        }

        /// <summary>
        /// Mets en pause le jeu quand on appuie sur le bouton 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void Pause(object sender, RoutedEventArgs e)
        {
            if (jeu != null)
            {
                if (sender is ToggleButton btn)
                {
                    if (btn.IsChecked == true)
                    {
                        jeu.Pause();
                        jeu.PauseTimer();
                    }

                    else
                    {
                        jeu.ResumeTimer();
                        jeu.Resume();
                    }
                }
                // Mettre à jour l'affichage du score lorsqu'on met pause
                scoreLabel.Content = Score;
            }

        }

        /// <summary>
        /// Ticker qui actualise la valeur du score 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void ScoreUpdateTimer_Tick(object sender, EventArgs e)
        {
            scoreLabel.Content = Score;
        }


        /// <summary>
        /// Lorsque le joueur clique sur le canvas, il récupere le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Focus();
        }

        /// <summary>
        /// Ouvre une fenetre de parametre et met le jeu en pause.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine/Daouda</author>
        private void OpenParametre(object sender, RoutedEventArgs e)
        {
            if (this.jeu != null)
            {
                jeu.Pause();
                jeu.PauseTimer();

                //Si une fenetre est déjà ouvert, on bascule dessu, sinon on en creer une
                if (fenetreParametres == null)
                {
                    fenetreParametres = new Parametres(this);
                    
                    // Centrer la fenêtre
                    fenetreParametres.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                    fenetreParametres.Show();
                }
                else if (fenetreParametres != null)
                {
                    fenetreParametres.Activate();
                }

            }
        }

        /// <summary>
        /// fenetre des parametres
        /// </summary>
        /// <author>Nordine</author>
        private Parametres fenetreParametres = null;

        /// <summary>
        /// Remet la fenetreParametre a null car elle a été fermée 
        /// </summary>
        /// <author>Nordine</author>
        public void PutFenetreParametresNull()
        {
            fenetreParametres = null;
        }


        /// <summary>
        /// Mets la fenetre du jeu en plein écran
        /// </summary>
        /// <author>Nordine/Daouda</author>
        public void FullscreenOn()
        {
            this.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Desactive le plein écran du jeu
        /// </summary>
        /// <author>Nordine/Daouda</author>
        public void FullscreenOff()
        {
            this.WindowState = WindowState.Normal;
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

        /// <summary>
        /// Ferme la fenetre du jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Nordine</author>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AfficherPopUpLoose()
        {
            System.Windows.MessageBox.Show(Res.Strings.Perdu);

        }

        /// <summary>
        /// Affiche la fenetre demandant le nom du joueur pour le highscore
        /// </summary>
        /// <author>Nordinez</author>
        public void AfficherFenetreHighScore(GameScore gameScore)
        {
            HighScore highScore = new HighScore(gameScore);
            // Centrer la fenêtre
            highScore.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            highScore.Show();

        }

        /// <summary>
        /// Ajoute un point de vie a l'affichage
        /// </summary>
        /// <author>Nordine</author>
        public void AddPointDeVie()
        {
            SpritePointDeVie++;
            if (SpritePointDeVie <= 0) SpritePointDeVie = 0;
            if (SpritePointDeVie >= 3) SpritePointDeVie = 3;
            UptadeSpritePointDeVie();
        }

        /// <summary>
        /// Enelve un point de vie a l'affichage de la main window
        /// </summary>
        /// <author>Nordine</author>
        public void RemovePointDeVie()
        {
            SpritePointDeVie--;
            if (SpritePointDeVie <= 0) SpritePointDeVie= 0;
            if (SpritePointDeVie >= 3) SpritePointDeVie = 3;
            UptadeSpritePointDeVie();

        }

        /// <summary>
        /// Mets a jour l'affichage  des points de vie 
        /// </summary>
        /// <author>Nordine</author>
        private void UptadeSpritePointDeVie()
        {
            //Affichage des points de vie
            string imagePath = "Sprites/pointVie/" + SpritePointDeVie.ToString() + "pv.png";
            Uri imageUri = new Uri(imagePath, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(imageUri);
            PointVieAffichage.Source = bitmapImage;
        } 

        /// <summary>
        /// Sprite de point en vie en cours
        /// </summary>
        /// <author>Nordine</author>
        private int SpritePointDeVie;
    }
}
