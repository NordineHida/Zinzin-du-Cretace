using IUTGame;
using ZinzinDuCretace;

namespace TestUnitaire.Collision
{
    public class TestCollision
    {
        /// <summary>
        /// Test la collision du joueur contre météorite (verifie si le jeu se termine et que la météorite disparait)
        /// </summary>
        /// <author>Nordine/Laszlo</author>
        [Fact]
        public void TestCollisionJoueurContreMeteorite()
        {
            SoundStore.AllowSounds = false;

            // Initialisation
            DAOParametre dao1 = new DAOParametre();
            Parametre parametre1 = dao1.CreerParametre();
            FakeScreen screen1 = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu1 = new LeJeu(screen1, parametre1, fake,fake);

            Joueur joueur1 = new Joueur(0, 0, jeu1);

            Meteorite meteorite = new Meteorite(0,0, jeu1);
            jeu1.AddItem(joueur1); jeu1.AddItem(meteorite);


            //Le joueur doit survivre au deux première météorite le jeu est fini
            joueur1.CollideEffect(meteorite);
            meteorite.CollideEffect(joueur1);
            // Verification collision 1
            Assert.False(jeu1.Fini);

            joueur1.CollideEffect(meteorite);
            meteorite.CollideEffect(joueur1);
            // Verification collision 2
            Assert.False(jeu1.Fini);

            joueur1.CollideEffect(meteorite);
            meteorite.CollideEffect(joueur1);
            // Verification collision 3 -> fin du jeu
            Assert.True(jeu1.Fini);
        }



        /// <summary>
        /// Test la collision du joueur contre le bonus (verifie que le jeu est pas fini et que le nombre de bonus augmente de 1)
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestCollisionJoueurContreBonus()
        {
            SoundStore.AllowSounds = false;
            

            // Initialisation
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake,fake);

            Joueur joueur = new Joueur(0, 0, jeu);

            Bonus bonus = new Bonus(0, 0, jeu);
            jeu.AddItem(joueur); jeu.AddItem(bonus);


            //Colision
            joueur.CollideEffect(bonus);
            bonus.CollideEffect(joueur);

            // Verification que le jeu continue correctement
            Assert.False(jeu.Fini);

            //Verification que le nombre de bonus a augmenter
            Assert.Equal(1,jeu.GetNombreBonus());

        }

        /// <summary>
        /// Test la collision du joueur contre le bonusPV (verifie que le joueur récupere ses hp)
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestCollisionJoueurContreBonusPV()
        {
            // Initialisation
            SoundStore.AllowSounds = false;
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake,fake);

            Joueur joueur = new Joueur(0, 0, jeu);
            BonusPV bonusPV = new BonusPV(0, 0, jeu);
            Meteorite meteorite = new Meteorite(0,0, jeu);
            jeu.AddItem(joueur); jeu.AddItem(bonusPV); jeu.AddItem(meteorite);


            // Verification que le joueur a bien 3 hp au debut du jeu
            Assert.Equal(3, joueur.Pv);

            //Colision
            joueur.CollideEffect(bonusPV);
            bonusPV.CollideEffect(joueur);

            // Verification que le joueur ne depasse pas les 3 hp max
            Assert.Equal(3, joueur.Pv);

            //Colision
            joueur.CollideEffect(meteorite);
            joueur.CollideEffect(bonusPV);

            // Verification que le joueur récupere le pv qu'il a perdu
            Assert.Equal(3, joueur.Pv);

            //Colision
            joueur.CollideEffect(meteorite);
            joueur.CollideEffect(meteorite);
            joueur.CollideEffect(bonusPV);
            // Verification que le joueur récupere 1 pv sur les 2 perdu (3-2+1 = 2)
            Assert.Equal(2, joueur.Pv);

        }

        /// <summary>
        /// Test la collision du joueur contre le bonusPV (verifie que le joueur récupere ses hp)
        /// </summary>
        /// <author>Nordine</author>
        [Fact]
        public void TestCollisionJoueurContreBonusInvincible()
        {
            // Initialisation
            SoundStore.AllowSounds = false;
            DAOParametre dao = new DAOParametre();
            Parametre parametre = dao.CreerParametre();
            FakeScreen screen = new FakeScreen();
            FakeMainWindow fake = new FakeMainWindow();
            LeJeu jeu = new LeJeu(screen, parametre, fake, fake);

            Joueur joueur = new Joueur(0, 0, jeu);
            BonusInvincible bonusInvincible = new BonusInvincible(0, 0, jeu);
            Meteorite meteorite = new Meteorite(0, 0, jeu);
            jeu.AddItem(joueur); jeu.AddItem(bonusInvincible); jeu.AddItem(meteorite);


            //Verification que le joueur ne perd pas de vie quand il est invincible
            joueur.CollideEffect(bonusInvincible);
            joueur.CollideEffect(meteorite);
            Assert.Equal(3,joueur.Pv);

            //Verification que le joueur perd son invincibilité
            joueur.Animate(new TimeSpan(0, 0, 0, 10));
            joueur.Animate(new TimeSpan(0, 0, 0, 0,8000));
            joueur.CollideEffect(meteorite);
            Assert.Equal(2, joueur.Pv);
        }

    }



}

