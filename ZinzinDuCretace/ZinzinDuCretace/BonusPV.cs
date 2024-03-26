using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Bonus de point de vie
    /// </summary>
    /// <author>Laszlo</author>
    public class BonusPV : Projectiles
    {
        /// <summary>
        /// le jeu
        /// </summary>
        /// <author>Laszlo</author>
        private LeJeu jeu;

        /// <summary>
        /// Constructeur de Bonus
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="g"></param>
        /// <author>Laszlo</author>
        public BonusPV(double x, double y, LeJeu g) : base(x, y, g, "bonusPv/bonusPv.png")
        {
            jeu = g;
            VitesseY = 15 + jeu.GetScore() / 10;
        }

        /// <summary>
        /// nom de l'objet "bonusPV"
        /// </summary>
        /// <author>Laszlo</author>
        public override string TypeName => "bonusPV";

        /// <summary>
        /// Comportement au moment de la collision avec un autre GameItem collideable
        /// </summary>
        /// <param name="other"></param>
        /// <author>Laszlo</author>
        public override void CollideEffect(GameItem other)
        {

            switch (other.TypeName)
            {
                //Quand le bonus touche une météroite on active le sound et le bonus disparait
                case "meteorite":

                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("bonusTouchFloor.mp3");
                    }
                    jeu.RemoveItem(this);

                    break;

                //Quand on touche un bonus on mets le sound et on enlève l'objet
                case "bonus":

                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("bonusTouchFloor.mp3");
                    }

                    jeu.RemoveItem(this);

                    break;

                //Quand on touche un bonusPV on mets le sound et on enlève l'objet
                case "bonusPV":

                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("bonusTouchFloor.mp3");
                    }

                    jeu.RemoveItem(this);

                    break;

                //Quand on touche un bonusInvincible on mets le sound et on enlève l'objet
                case "bonusInvincible":

                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("bonusTouchFloor.mp3");
                    }

                    jeu.RemoveItem(this);

                    break;

                //Quand on touche un joueur l'objet disparait
                case "joueur":

                    jeu.RemoveItem(this);

                    break;

            }
        }

        /// <summary>
        /// définit commant va bouger le bonus au fil du temps (en plus du fait qu'il bougera comme un projectile)
        /// </summary>
        /// <param name="dt"></param>
        /// <author>Laszlo</author>
        public override void Animate(TimeSpan dt)
        {
            if (this != null) base.Animate(dt);

            if (this.Bottom + 55 >= GameHeight)
            {
                //Si le sound effect est activé on joue les sons
                if (jeu.Parametre.SoundEffect)
                {
                    PlaySound("bonusTouchFloor.mp3");
                }

                jeu.RemoveItem(this);
            }
        }
    }
}
