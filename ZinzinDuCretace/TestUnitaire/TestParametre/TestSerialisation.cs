using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZinzinDuCretace;

namespace TestUnitaire.TestParametre
{
    /// <summary>
    /// Test des serialisation
    /// </summary>
    /// <author>Nordine</author>
    public class TestSerialisation
    {
        /// <summary>
        /// Test de la méthode TryName de la classe GameScore
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestGameScoreTryName()
        {
            // Initialisation
            SoundStore.AllowSounds = false;
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);
            GameScore gameScore = new GameScore(jeu);

            //Si le fichier existe on le supprime pour repartir sur un fichier vierge
            if (File.Exists("score.json"))
                File.Delete("score.json");



            //Test nom vide
            Assert.False(gameScore.TryName(""));
            //Test nom correct
            Assert.True(gameScore.TryName("toto"));
            //Test nom pas saisi (=placeholder)
            Assert.False(gameScore.TryName(ZinzinDuCretace.Res.Strings.SaisirNom));

            //On va recuperer la serialisation
            List<(string nom, double score)> listScoreTest = gameScore.DeserialiserScores();
            //On verifie que la liste contienne bien le "toto" créer précédement
            Assert.Contains(("toto", 0), listScoreTest);
        }

        /// <summary>
        /// Verifie que la méthode DeserialiserScores de la classe GameScore range bien les scores dans l'ordres décroissant du score
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestTriDecroissantDuScore()
        {
            // Initialisation
            SoundStore.AllowSounds = false;
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);
            GameScore gameScore = new GameScore(jeu);

            //Si le fichier existe on le supprime pour repartir sur un fichier vierge
            if (File.Exists("score.json"))
                File.Delete("score.json");

            //On simule trois partie avec differents nom/score en ne les ajoutant pas dans l'ordre

            gameScore.Score = 3;
            gameScore.TryName("Deuxieme");

            gameScore.Score = 6;
            gameScore.TryName("Premier");

            gameScore.Score = 0;
            gameScore.TryName("Dernier");

            gameScore.Score = 1;
            gameScore.TryName("Troisieme");



            //On va recuperer la serialisation
            List<(string nom, double score)> listScoreTest = gameScore.DeserialiserScores();
            //On verifie qu'il sont bien triée par ordre décroissant du score
            Assert.True(listScoreTest[0] == ("Premier", 6) && listScoreTest[1] == ("Deuxieme", 3) && listScoreTest[2] == ("Troisieme", 1) && listScoreTest[3] == ("Dernier", 0));

        }
    }
}
