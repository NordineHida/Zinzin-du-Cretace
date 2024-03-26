using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Interface qui fait le lien entre le jeu et la mainwindow
    /// </summary>
    /// <author>Nordine</author>
    public interface IAfficherFenetre
    {
        /// <summary>
        /// Affiche une pop up indiquant qu'on a perdu le jeu
        /// </summary>
        /// <author>Nordine</author>
        void AfficherPopUpLoose();

        /// <summary>
        /// Affiche la fenetre demandant le nom du joueur pour le highscore
        /// </summary>
        /// <author>Nordinez</author>
        void AfficherFenetreHighScore(GameScore gameScore);



           
    }
}
