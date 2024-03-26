using IUTGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZinzinDuCretace
{
    public class LeJeu : IUTGame.Game
    {
        private bool fini = false; 

        /// <summary>
        /// Est ce que le jeu est fini
        /// </summary>
        /// <author>Nordine</author>
        public bool Fini { get { return fini; } set { fini = value; } }

        /// <summary>
        /// Score du jeu
        /// </summary>
        /// <author>Nordine</author>
        private GameScore gameScore;

        /// <summary>
        /// Nombre de bonus collectés
        /// </summary>
        /// <author>Nordine</author>
        private int nombreBonus = 0;

        /// <summary>
        /// Timer depuis que le jeu est lancé
        /// </summary>
        /// <author>Nordine</author>
        private Stopwatch timerJeu = new Stopwatch();

        /// <summary>
        /// Parametre du jeu
        /// </summary>
        /// <author>Daouda / Nordine</author>
        private Parametre parametre;

        /// <summary>
        /// Parametre du jeu
        /// </summary>
        /// <author>Daouda / Nordine</author>
        public Parametre Parametre { get { return parametre; } }

        private IAfficherFenetre interfaceAfficherFenetre;
        private IPointDeVieChanged interfacePointDeVie;
        /// <summary>
        /// Constructeur de jeu
        /// </summary>
        /// <param name="screen">écran de jeu</param>
        /// <author>Nordine</author>
        public LeJeu(IScreen screen, Parametre parametre, IAfficherFenetre interfaceAfficherFenetre, IPointDeVieChanged interfacePointDeVie) : base(screen, "Sprites", "Sounds")
        {
            this.parametre = parametre;
            this.BackgroundVolume = parametre.Volume;
            this.gameScore = new GameScore(this);
            this.interfaceAfficherFenetre = interfaceAfficherFenetre;
            this.interfacePointDeVie = interfacePointDeVie;
        }

        /// <summary>
        /// Initialisation des gameItems
        /// </summary>
        /// <author>Nordine/Laszlo/Daouda</author>
        protected override void InitItems()
        {
            //Initialisation des items ici !
            timerJeu.Restart();

            //Score
            AddItem(this.gameScore);

            //Joueur
            double y = this.Screen.Height-132;
            double x = this.Screen.Width / 2;
            Joueur joueur = new Joueur(x, y, this);
            AddItem(joueur);

            //Generateur de projectile
            GenerateurProjectiles generateurProjectiles = new GenerateurProjectiles(this);
            AddItem(generateurProjectiles);

            //Lancement de la musique
            PlayBackgroundMusic("backgroundSound.mp3");

            //Changement du volume de la musique
            BackgroundVolume = parametre.Volume;
        }

        /// <summary>
        /// Quand on perd le jeu
        /// </summary>
        /// <author>Nordine</author>
        protected override void RunWhenLoose()
        {
            //On arrete le timer
            timerJeu.Stop();

            //On arrete la musique du jeu
            this.StopBackgroundMusic();

            //Le jeu est fini
            this.fini = true;

            //Pv = 0
            interfacePointDeVie.RemovePointDeVie();

            interfaceAfficherFenetre.AfficherPopUpLoose();
            interfaceAfficherFenetre.AfficherFenetreHighScore(gameScore);

        }

        /// <summary>
        /// Quand on gagne le jeu
        /// </summary>
        /// <author>Nordine</author>
        protected override void RunWhenWin()
        {
            //On ne peut pas "gagner" dans le jeu
        }

        /// <summary>
        /// Renvoi le score du jeu
        /// </summary>
        /// <returns>le score du jeu (double)</returns>
        /// <author>Nordine</author>
        public double GetScore()
        {
            return this.gameScore.Score;
        }


        /// <summary>
        /// Ajoute un au nombre de bonus collectés par le joueur
        /// </summary>
        /// <author>Nordine</author>
        public void AddBonus()
        {
            nombreBonus++;
        }

        /// <summary>
        /// Renvoi le timer du jeu
        /// </summary>
        /// <returns>le timer du jeu</returns>
        /// <author>Nordine</author>
        public Stopwatch GetTimer()
        {
            return timerJeu;
        }

        /// <summary>
        /// Renvoi le nombre de bonus collectés
        /// </summary>
        /// <returns>int nombreBonus</returns>
        /// <author>Nordine</author>
        public int GetNombreBonus()
        {
            return nombreBonus;
        }


        /// <summary>
        /// Mets en pause le timer de jeu 
        /// </summary>
        /// <author>Nordine</author>
        public void PauseTimer()
        {
            timerJeu.Stop();
        }

        /// <summary>
        /// Reprend le compte du timer
        /// </summary>
        /// <author>Nordine</author>
        public void ResumeTimer()
        {
            timerJeu.Start();
        }

        /// <summary>
        /// Ajout de PV sur l'affichage
        /// </summary>
        /// <author>Nordine</author>
        public void AddPV()
        {
            interfacePointDeVie.AddPointDeVie();
        }

        /// <summary>
        /// Suppresion d'un PV sur l'affichage
        /// </summary>
        /// <author>Nordine</author>
        public void LosePV()
        {
            interfacePointDeVie.RemovePointDeVie();
        }


    }
}
