using IUTGame;
using IUTGame.WPF;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using Xunit;
using ZinzinDuCretace;

namespace TestUnitaire.Deplacement
{
    public class TestJoueur
    {
        /// <summary>
        /// Verifie que le joueur reste bien à sa position Y (hauteur) meme quand il se deplace
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestPositionY()
        {
            // Initialisation
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);
            double expectedPositionY = 10;

            //Creation joueur
            Joueur joueur = new Joueur(0, expectedPositionY, jeu);
            double actualPositionY = joueur.PositionY;

            //Animation du joueur
            joueur.KeyDown(Key.Right);
            joueur.Animate(new TimeSpan(0, 0, 0, 0, 20));
            joueur.KeyDown(Key.Left);
            joueur.Animate(new TimeSpan(0, 0, 0, 0, 20));


            // Assert
            Assert.Equal(expectedPositionY, actualPositionY);
        }


    }  
}
