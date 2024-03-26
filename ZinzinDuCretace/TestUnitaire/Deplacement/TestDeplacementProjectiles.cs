using IUTGame.WPF;
using IUTGame;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using Xunit;
using ZinzinDuCretace;


namespace TestUnitaire.Deplacement
{
    /// <summary>
    /// Tests sur les déplacements des projectiles
    /// </summary>
    /// <author>Laszlo</author>
    public class TestDeplacementProjectiles
    {
        /// <summary>
        /// On teste si la gravité fonctionne sur la météorite (le plus longtemps elle est en chute, le plus elle ira vite)
        /// </summary>
        /// <author>Laszlo</author>
        [Fact]
        public void TestGraviteMeteorite()
        {
            SoundStore.AllowSounds = false;
            // Initialisation
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();

            
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);


            //Creation de la meteorite
            Meteorite meteorite = new Meteorite(0, 0, jeu);
            jeu.AddItem(meteorite);
            double vitesseAvant = meteorite.VitesseY;

            //Animation de la meteorite
            
            meteorite.Animate(new TimeSpan(0, 0, 0, 0, 20));
            double vitesseApres = meteorite.VitesseY;

            // Assert
            Assert.True(vitesseApres > vitesseAvant);

        }


        /// <summary>
        /// On teste si la gravité fonctionne sur le bonus (le plus longtemps il est en chute, le plus il ira vite)
        /// </summary>
        /// <author>Laszlo</author>
        [Fact]
        public void TestGraviteBonus()
        {
            SoundStore.AllowSounds = false;
            // Initialisation
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();

            
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);


            //Creation du bonus

            Bonus bonus = new Bonus(0, 0, jeu);
            jeu.AddItem(bonus);

            double vitesseAvant = bonus.VitesseY;

            //Animation du bonus

            bonus.Animate(new TimeSpan(0, 0, 0, 0, 20));
            double vitesseApres = bonus.VitesseY;



            // Réalisation du test
            Assert.True(vitesseApres > vitesseAvant);

        }



    }
}

