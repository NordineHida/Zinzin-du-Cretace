using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Classe déterminant toutes les propriétés et comportements des météorites
    /// </summary>
    /// <author>Laszlo</author>
    public class Meteorite : Projectiles
    {
        /// <summary>
        /// Le jeu
        /// </summary>
        /// <author>Laszlo</author>
        private LeJeu jeu;

        /// <summary>
        /// Constructeur de météorite
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="g"></param>
        /// <author>Laszlo</author>
        public Meteorite(double x, double y, LeJeu g) : base(x, y, g, "meteorite/meteorite.png")
        {
            jeu = g;
            VitesseY = 8 + jeu.GetScore()/10;
        }

        /// <summary>
        /// Type name de la météorite ("meteorite")
        /// </summary>
        /// <author>Laszlo</author>
        public override string TypeName => "meteorite";

        /// <summary>
        /// Comportement au moment de la collision avec un autre GameItem collideable
        /// </summary>
        /// <param name="other"></param>
        /// <author>Laszlo</author>
        public override void CollideEffect(GameItem other)
        {
            switch (other.TypeName)
            {
                //Quand la meteorite touche une autre météroite on active le sound et la meteorite disparait
                case "meteorite":

                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        PlaySound("meteoriteBreak.mp3");
                    }

                    jeu.RemoveItem(this);
                    break;

                //Quand on touche un joueur la météorite disparait
                case "joueur":


                    jeu.RemoveItem(this);

                    break;
            }
        }

        /// <summary>
        /// Détermine comment la météorite va bouger (en plus du fait qu'elle bougera comme un projectile)
        /// </summary>
        /// <param name="dt"></param>
        /// <author>Laszlo</author>
        public override void Animate(TimeSpan dt)
        {
            if (this != null) 
            {
                waitingAvantRotation -= dt;
                Random r = new Random();

                //Fait tourner la météorite
                if (waitingAvantRotation.TotalMilliseconds<=0)
                {
                    if(r.Next(0,2)==0) this.Orientation += 20;
                    else this.Orientation -= 20;

                    waitingAvantRotation = new TimeSpan(0, 0, 0, 0, 41);
                }

                base.Animate(dt); 
                if(this.Bottom >= GameHeight)
                {
                    //Si le sound effect est activé on joue les sons
                    if (jeu.Parametre.SoundEffect)
                    {
                        int random = r.Next(0, 3);
                        if (random == 0) PlaySound("meteoriteTouchFloor.mp3");
                        else if (random == 1) PlaySound("meteoriteTouchFloor2.mp3");
                        else PlaySound("meteoriteTouchFloor3.mp3");
                    }

                    jeu.RemoveItem(this);
                }
            }
        }

        /// <summary>
        /// Temps avant de tourner le sprite
        /// </summary>
        /// <author>Nordine</author>
        private TimeSpan waitingAvantRotation = new TimeSpan(0);


    }
}
