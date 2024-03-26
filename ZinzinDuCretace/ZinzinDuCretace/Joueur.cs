using System;
using System.Security;
using System.Windows.Input;
using IUTGame;
using IUTGame.WPF;
using System.ComponentModel;


namespace ZinzinDuCretace
{
    public class Joueur : GameItem, IAnimable, IKeyboardInteract
    {
        /// <summary>
        /// Le jeu
        /// </summary>
        /// <author>Nordine</author>
        private LeJeu jeu;

        //Attributs pour le deplacement du joueur
        //<author>Nordine</author>
        private double force = 0;
        private const double masse = 50;
        private double vitesseX = 0;
        private double positionY;
        private const double ConstFrotement = 1;

        /// <summary>
        /// Point du vie du joueur
        /// </summary>
        /// <author>Nordine</author>
        private int pv;

        /// <summary>
        /// Point de vie du dino
        /// </summary>
        public int Pv { get { return pv; } set { pv = value; } }

        /// <summary>
        /// quand le dino est au sol parce qu'il a touché une météorite
        /// </summary>
        /// <author>Nordine</author>
        private bool auSol = false;

        /// <summary>
        /// Indique si le joueur est invicible
        /// </summary>
        /// <author>Nordine</author>
        private bool estInvincible = false;


        /// <summary>
        /// La position verticale (y) du joueur
        /// </summary>
        /// <author>Daouda/Nordine</author>
        public double PositionY { get { return positionY; } set { positionY = value; } }

        /// <summary>
        /// Conserve le sens du dino
        /// </summary>
        /// <author>Nordine</author>
        private bool regardeDroite=true;

        /// <summary>
        /// Constructeur de joueur
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="g">le jeu</param>
        /// <author>Nordine</author>
        public Joueur(double x, double y, LeJeu g) : base(x, y, g, "dinosD/walk0.png")
        {
            this.positionY = y;
            this.jeu = g;
            this.pv = 3;
            
        }

        /// <summary>
        /// Type name du joueur "joueur"
        /// </summary>
        /// <author>Nordine</author>
        public override string TypeName => "joueur";

        /// <summary>
        /// Anime le joueur
        /// </summary>
        /// <param name="dt">délai depuis le dernier appel</param>
        /// <author>Nordine/Laszlo</author>
        public void Animate(TimeSpan dt)
        {
            if (waitingInvincible.TotalMilliseconds <= 0)
            {
                estInvincible = false;
                this.Orientation = 0;
            }
            else
            {
                waitingInvincible-=dt;  
            }
            //Si on est pas au sol on bouge
            if (!auSol)
            {
                //Déplacement
                double frotement = (1 - ConstFrotement / masse * dt.TotalMilliseconds);
                if (frotement < 0) frotement = 0;
                if (frotement > 1) frotement = 1;
                this.vitesseX = force / masse * dt.TotalMilliseconds + this.vitesseX * frotement;

                if (Left < 0) PutXY(0, positionY);
                else if (Right > GameWidth) PutXY(GameWidth - (Right - Left), positionY);
                else MoveXY(vitesseX, 0);
            }
            //Si on est au sol et que le timer est pas activé, alors on l'active
            else if (auSol && waitingAvantSeLever.TotalMilliseconds<=0)
            {
                waitingAvantSeLever = new TimeSpan(0,0,0,0,250);
            }

            if (waitingAvantSeLever.TotalMilliseconds > 0) 
            { 
                waitingAvantSeLever -= dt;
                if (waitingAvantSeLever.TotalMilliseconds <= 0) auSol = false; 
            }

            waitingAvantChangerSprite -= dt;

            //Si le timer de changement de sprite est fini et que le temps au sol est fini alors on peut bouger
            if (waitingAvantChangerSprite.TotalMilliseconds <=0 && waitingAvantSeLever.TotalMilliseconds <= 0)
            {
                string spriteName;
                //Si le joueur est pas invincible
                if (!estInvincible)
                {
                    // Changement de sprite uniquement si le dino est en mouvement
                    spriteName = regardeDroite ? "dinosD/" : "dinosG/";

                    if (force != 0)
                    {
                        int spriteIndex = GetSpriteIndex();
                        spriteName += $"walk{spriteIndex}.png";

                        //Si le sound effect est activé on joue les sons
                        if (jeu.Parametre.SoundEffect)
                        {
                            //Sound des bruits de pas
                            PlaySound("walk.mp3");
                        }
                        ChangeSprite(spriteName);
                        waitingAvantChangerSprite = new TimeSpan(0, 0, 0, 0, 41);
                    }
                    else
                    {
                        if (regardeDroite) ChangeSprite("dinosD/walk0.png");
                        else ChangeSprite("dinosG/walk0.png");
                        waitingAvantChangerSprite = new TimeSpan(0, 0, 0, 0, 41);
                    }
                }

                //Si le joueur est invicible
                else
                {
                    waitingInvincible -= dt;
                    // Rotation uniquement si le dino est en mouvement
                    spriteName = regardeDroite ? "dinosInvincible/D.png" : "dinosInvincible/G.png";
                    ChangeSprite(spriteName);
                    if (force != 0)
                    {

                        if (regardeDroite) this.Orientation += 25;
                        else this.Orientation -= 25;
                        waitingAvantChangerSprite = new TimeSpan(0, 0, 0, 0, 41);
                    }
                    else
                    {
                        waitingAvantChangerSprite = new TimeSpan(0, 0, 0, 0, 41);
                    }
                }
            }
        }

        /// <summary>
        /// delai avant de se lever (apres avoir toucher une météorite)
        /// </summary>
        /// <author>Nordine</author>
        private TimeSpan waitingAvantSeLever = new TimeSpan(0);

        /// <summary>
        /// délai avant de changer de sprite (evite le bug de collision)
        /// </summary>
        /// <author>Nordine</author>
        private TimeSpan waitingAvantChangerSprite = new TimeSpan(0) ;

        /// <summary>
        /// Delai d'invincibilité
        /// </summary>
        /// <author>Nordine</author>
        private TimeSpan waitingInvincible = new TimeSpan(0);

        /// <summary>
        /// Index du sprite actuel du dino
        /// </summary>
        /// <author>Nordine</author>
        private int currentSpriteIndex = 0;

        /// <summary>
        /// Obtient l'index du sprite pour l'animation du dino
        /// </summary>
        /// <returns>L'index du sprite</returns>
        /// <author>Nordine</author>
        private int GetSpriteIndex()
        {
            int spriteIndex = currentSpriteIndex;
            currentSpriteIndex = (currentSpriteIndex + 1) % 10; // Incrémente l'index et revient à 0 après 9
            return spriteIndex;
        }


        /// <summary>
        /// Reaction lors d'une collision du joueur avec d'autre objet
        /// </summary>
        /// <param name="other">objet touché</param>
        /// <author>Nordine/Laszlo</author>
        public override void CollideEffect(GameItem other)
        {
            Random r = new Random();

            switch (other.TypeName)
            {
                //Quand on touche une météroite on active le sound et on perd la partie
                case "meteorite":
                    if (!estInvincible)
                    {
                        //Si le sound effect est activé on joue les sons
                        if (jeu.Parametre.SoundEffect)
                        {
                            if (r.Next(0, 2) == 1) PlaySound("getTouched.mp3");
                            else PlaySound("getTouched1.mp3");
                        }
                        ChangeSprite("dinosD/dead.png");

                        //si il ne restais qu'un point de vie on perd, sinon on perd un point de vie
                        if (pv == 1) jeu.Loose();
                        else
                        {
                            jeu.LosePV();
                            pv--;
                            auSol = true;
                        }
                    }
                    break;

                //Quand on touche un bonus on mets le sound et on gagne 10 score
                case "bonus":
                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("getBonus.mp3");
                    }
                    jeu.AddBonus();
                    break;

                //Quand on touche un bonus on mets le sound et on gagne une vie
                case "bonusPV":
                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("getBonusPV.mp3");
                    }
                    //si le joueur a déja 3 vies on ne lui en rajoute pas
                    if (pv <=2)
                    {
                        pv++;
                        jeu.AddPV();
                    }
                    break;

                //Quand on touche un bonus on mets le sound et on gagne 10 score
                case "bonusInvincible":
                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("getInvincible.mp3");
                    }
                    estInvincible = true;
                    waitingInvincible = new TimeSpan(0,0,0,5,800);

                    break;

            }
        }


        
        /// <summary>
        /// Quand une touche est pressé (deplace le dino si droite/gauche)
        /// </summary>
        /// <param name="key">touche pressée</param>
        /// <author>Nordine</author>
        public void KeyDown(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    force = -25;
                    regardeDroite=false;
                    break;


                case Key.Right:
                    force = 25;
                    regardeDroite = true;
                    break;
            }
        }

        /// <summary>
        /// Quand on relache la touche
        /// </summary>
        /// <param name="key">touche préssée</param>
        /// <author>Nordine</author>
        public void KeyUp(Key key)
        {
            this.force = 0;
        }
    }
}
