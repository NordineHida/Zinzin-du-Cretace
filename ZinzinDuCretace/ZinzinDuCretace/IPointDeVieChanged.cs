using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Interface qui fait le lien entre le joueur et la mainwindow pour l'affichage des points de vie
    /// </summary>
    /// <author>Nordine</author>
    public interface IPointDeVieChanged
    {
        /// <summary>
        /// Quand le joueur gagner un point de vie
        /// </summary>
        /// <author>Nordine</author>
        void AddPointDeVie();

        /// <summary>
        /// Quand le joueur perd un point de vie
        /// </summary>
        /// <author>Nordine</author>
        void RemovePointDeVie();
    }
}
