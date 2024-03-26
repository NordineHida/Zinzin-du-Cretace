 using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Classe déterminant toutes les propriétés et comportements des bonus
    /// </summary>
    /// <author>Laszlo</author>
    public class Bonus : Projectiles
    {
        /// <summary>
        /// Le jeu
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
        public Bonus(double x, double y, LeJeu g) : base(x, y, g, "bonus/1.png")
        {
            jeu = g;
            VitesseY = 15 + jeu.GetScore()/10;
        }

        /// <summary>
        /// nom de l'objet
        /// </summary>
        /// <author>Laszlo</author>
        public override string TypeName =>"bonus";

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

                //Quand on touche un joueur l'objet disparait
                case "joueur":

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

            int spriteIndex = GetSpriteIndex();
            string spriteName = "bonus/";

            waitingAvantChangerSprite -= dt;

            if (waitingAvantChangerSprite.TotalMilliseconds <= 0)
            {
                spriteName += $"{spriteIndex}.png";
                ChangeSprite(spriteName);
                waitingAvantChangerSprite = new TimeSpan(0, 0, 0, 0, 41);
            }

            if (this.Bottom +55 >= GameHeight)
            {
                //Si le sound effect est activé on joue les sons
                if (jeu.Parametre.SoundEffect)
                {
                    PlaySound("bonusTouchFloor.mp3");
                }

                jeu.RemoveItem(this);
            }
        }

        /// <summary>
        /// Delai avant de changer de sprite 
        /// </summary>
        /// <author>Laszlo</author>
        private TimeSpan waitingAvantChangerSprite = new TimeSpan(0);

        /// <summary>
        /// Index du sprite actuel du dino
        /// </summary>
        /// <author>Laszlo</author>
        private int currentSpriteIndex = 1;

        /// <summary>
        /// Obtient l'index du sprite pour l'animation du dino
        /// </summary>
        /// <returns>L'index du sprite</returns>
        /// <author>Laszlo</author>
        private int GetSpriteIndex()
        {
            int spriteIndex = currentSpriteIndex;
            currentSpriteIndex = ((currentSpriteIndex + 1) % 9)+1; // Incrémente l'index et revient à 1 après 10
            return spriteIndex;
        }
    }
}
