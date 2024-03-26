using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Génère dans le jeu les différents types de projectiles
    /// </summary>
    /// <author>Laszlo</author>
    public class GenerateurProjectiles : GameItem, IAnimable
    {
        /// <summary>
        /// Temps avant de créer une nouvelle météorite
        /// </summary>
        private TimeSpan timeToCreateMeteorite;


        /// <summary>
        /// temps avant de créer un autre bonus
        /// </summary>
        private TimeSpan timeToCreateBonus;

        /// <summary>
        /// temps avant de créer un autre bonus
        /// </summary>
        private TimeSpan timeToCreateBonusPV;

        /// <summary>
        /// temps avant de créer un autre bonus
        /// </summary>
        private TimeSpan timeToCreateBonusInvincible;

        /// <summary>
        /// Le jeu (permet d'acceder au score entre autre)
        /// </summary>
        /// <author>Nordine</author>
        private LeJeu jeu;


        /// <summary>
        /// Constructeur de générateur de projectiles
        /// </summary>
        /// <param name="g"></param>
        /// <author>Laszlo</author>
        public GenerateurProjectiles(LeJeu g) : base(0, 0, g)
        {
            timeToCreateMeteorite = new TimeSpan(0, 0, 2);
            timeToCreateBonus = new TimeSpan(0, 0, 5);
            timeToCreateBonusPV = new TimeSpan(0, 0, 11);
            timeToCreateBonusInvincible = new TimeSpan(0, 0, 17);
            this.jeu = g;
        }

        /// <summary>
        /// nom de l'objet
        /// </summary>
        public override string TypeName => "generateurProjectiles";

        /// <summary>
        /// Détermine comment bouge et fonctionne le générateur au fil du temps
        /// </summary>
        /// <param name="dt"></param>
        /// <author>Laszlo</author>
        public void Animate(TimeSpan dt)
        {
            Random r = new Random();

            //Meteorite
            timeToCreateMeteorite -= dt;
            if (timeToCreateMeteorite.TotalMilliseconds < 0)
            {
                
                double x = r.NextDouble() * GameWidth;
                double y = 0;


                Meteorite m = new Meteorite(x, y, jeu);
                jeu.AddItem(m);
                double ms = r.Next(500) + 500 / jeu.GetScore() * 3 ;
                timeToCreateMeteorite = new TimeSpan(0, 0, 0, 0, (int)ms);
            }

            //Bonus
            timeToCreateBonus -= dt;
            if (timeToCreateBonus.TotalMilliseconds < 0)
            {
                
                double xbonus = r.NextDouble() * GameWidth;
                double ybonus = 0;

                Bonus b = new Bonus(xbonus, ybonus, jeu);
                jeu.AddItem(b);
                double msbonus = r.Next(3500) + 7000 / jeu.GetScore() * 3;
                timeToCreateBonus = new TimeSpan(0, 0, 0, 0, (int)msbonus);
            }

            //Bonus de PV
            timeToCreateBonusPV -= dt;
            if (timeToCreateBonusPV.TotalMilliseconds < 0)
            {
                
                double xbonusPV = r.NextDouble() * GameWidth;
                double ybonusPV = 0;

                BonusPV bPV = new BonusPV(xbonusPV, ybonusPV, jeu);
                jeu.AddItem(bPV);
                double msbonusPV = r.Next(3000) + 15000;
                timeToCreateBonusPV = new TimeSpan(0, 0, 0, 0, (int)msbonusPV);
            }

            //Bonus d'invincibilité
            timeToCreateBonusInvincible -= dt;
            if (timeToCreateBonusInvincible.TotalMilliseconds < 0)
            {
                ;
                double xbonusInvincible = r.NextDouble() * GameWidth;
                double ybonusInvincible = 0;

                BonusInvincible bInvincible = new BonusInvincible(xbonusInvincible, ybonusInvincible, jeu);
                jeu.AddItem(bInvincible);
                double msbonusInvincible = r.Next(5000) + 20000;
                timeToCreateBonusInvincible = new TimeSpan(0, 0, 0, 0, (int)msbonusInvincible);
            }
        }

        /// <summary>
        /// renvoie si l'objet peut-être touché par les autres ou non
        /// </summary>
        /// <param name="other"></param>
        /// <returns>vrai si l'objet peut etre touché par un autre, faux sinon</returns>
        /// <author>Laszlo</author>
        public override bool IsCollide(GameItem other)
        {
            return false;
        }

        /// <summary>
        /// Effet lors de collision (vide car pas collisionable)
        /// </summary>
        /// <author>Laszlo</author>
        /// <param name="other"></param>
        public override void CollideEffect(GameItem other)
        {
            
        }
    }
}
