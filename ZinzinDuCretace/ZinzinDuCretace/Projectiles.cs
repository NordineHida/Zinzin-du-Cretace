using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Classe qui gère tous les types de projectiles
    /// </summary>
    /// <author>Laszlo</author>
    public abstract class Projectiles : GameItem, IAnimable
    {
        /// <summary>
        /// Nombre random
        /// </summary>
        /// <author>Laszlo</author>
        private Random r = new Random();

        /// <summary>
        /// vitesse horizontale du projectile
        /// </summary>
        /// <author>Laszlo</author>
        private double vitesseX;

        /// <summary>
        /// vitesse verticale du projctile
        /// </summary>
        /// <author>Laszlo</author>
        private double vitesseY;

        /// <summary>
        /// Vitesse verticale du projectile (get/set)
        /// </summary>
        /// <author>Laszlo</author>
        public double VitesseY { get { return vitesseY; } set { vitesseY = value; } }

        /// <summary>
        /// constante de gravité
        /// </summary>
        /// <author>Laszlo</author>
        public const double gravite = 0.03;

        /// <summary>
        /// Type name du projectile "projectile"
        /// </summary>
        /// <author>Laszlo</author>
        public override string TypeName => "projectile";

        /// <summary>
        /// Constructeur de Projectiles
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="g"></param>
        /// <param name="sprite"></param>
        /// <author>Laszlo</author>
        public Projectiles(double x, double y, LeJeu g , string sprite) : base(x,y,g, sprite)
        {
            if (x < GameWidth / 2) vitesseX = r.Next(0, 20); ///si le projectile spawn à gauche de l'écran, il se déplacera vers la droite et inversement (pour limiter les sorties de l'écran)
            else vitesseX = r.Next(-20, 1);
        }

        /// <summary>
        /// Comportement au moment de la collision avec un autre GameItem collideable
        /// </summary>
        /// <param name="other"></param>
        /// <author>Laszlo</author>
        public override void CollideEffect(GameItem other)
        {
           
        }

        /// <summary>
        /// Détermine comment un projectiles se déplacera au fil du temps
        /// </summary>
        /// <param name="dt">temps écoulé depuis le dernier mouvement</param>
        /// <author>Laszlo</author>
        public virtual void Animate(TimeSpan dt)
        {
            if (this != null)
            {
                this.vitesseY += gravite * dt.TotalMilliseconds;
                this.MoveXY(vitesseX, vitesseY);
            }
            
        }
    }
}
