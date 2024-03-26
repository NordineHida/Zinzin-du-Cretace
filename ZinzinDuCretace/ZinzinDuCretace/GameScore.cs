using IUTGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Formats.Asn1.AsnWriter;

namespace ZinzinDuCretace
{

    /// <summary>
    /// Score du jeu
    /// </summary>
    /// <author>Nordine</author>
    /// [DataContract]
    public class GameScore : GameItem, IAnimable
    {

        /// <summary>
        /// Score du jeu
        /// </summary>
        /// <author>Nordine</author>
        private double score;

        /// <summary>
        /// Score du jeu (Get/set)
        /// </summary>
        /// <author>Nordine</author>
        public double Score { get { return score; } set { score = value; } }

        /// <summary>
        /// Typename "score"
        /// </summary>
        /// <author>Nordine</author>
        public override string TypeName => "score";

        /// <summary>
        /// Jeu auquel est associé le score
        /// </summary>
        /// <author>Nordine</author>
        private LeJeu jeu;

        /// <summary>
        /// Constructeur de score
        /// </summary>
        /// <param name="g">Jeu</param>
        /// <author>Nordine</author>
        public GameScore(LeJeu g) :base (0,0,g)
        {
            jeu = g;
        }

        /// <summary>
        /// Est-ce qu'il peut y avoir une collision (non)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>false</returns>
        /// <author>Nordine</author>
        public override bool IsCollide(GameItem other)
        {
            return false;
        }

        /// <summary>
        /// Effet lors de collision (pas de collision donc vide)
        /// </summary>
        /// <param name="other"></param>
        /// <author>Nordine</author>
        public override void CollideEffect(GameItem other)
        {
            
        }

        /// <summary>
        /// actualise le score
        /// </summary>
        /// <param name="dt">temps depuis le dernier appel</param>
        /// <author>Nordine</author>
        public void Animate(TimeSpan dt)
        {
            Stopwatch timer = jeu.GetTimer();

            score = timer.ElapsedMilliseconds/1000 + jeu.GetNombreBonus()*10;

        }


        /// <summary>
        /// Liste de couple nom/score
        /// </summary>
        [DataMember]
        private List<(string nom,double score)> scores = new List<(string nom,double score)> ();

        /// <summary>
        /// Verifie que le nom saisi est correct, si oui renvoi true et serialise le couple nom/score, si non renvoi false
        /// </summary>
        /// <param name="nom">nom saisi par le joueur à tester</param>
        /// <author>Laszlo/Nordine</author>
        public bool TryName(string nom)
        {
            bool nomPossible;

            // Verifie que le nom saisi est valide avant de pouvoir valider
            if (!string.IsNullOrWhiteSpace(nom) && nom != Res.Strings.SaisirNom)
            {
                (string nom, double score) newScore = (nom, score);
                scores.Add(newScore);
                SaveScore();
                nomPossible = true;
            }
            else
            {
                nomPossible = false;
            }
            return nomPossible;
        }

        /// <summary>
        /// Sérialize la liste de scores dans un fichier json
        /// </summary>
        /// <author>Laszlo/Nordine</author>
        private void SaveScore()
        {
            // Créez une instance de JsonSerializer
            var serializer = new DataContractJsonSerializer(typeof(List<(string nom, double score)>));

            // Vérifiez si le fichier existe déjà
            bool fileExists = File.Exists("score.json");

            // Si le fichier existe, on recupere les anciennes donnée
            if (fileExists)
            {
                List<(string nom, double score)> oldHighScores = DeserialiserScores();
                //pour les scores, s'ils ne sont pas dans la sauvegarde precedente, on les rajoutes.
                foreach((string nom, double score) s in scores)
                {
                    if (!oldHighScores.Contains(s))
                    {
                        oldHighScores.Add(s);
                    }
                }
                // puis on reserialise le tout
                using (FileStream stream = File.Open("score.json", FileMode.Create))
                {
                    // Sérialise la liste de scores dans le fichier JSON
                    serializer.WriteObject(stream, oldHighScores);
                }
            }
            else
            {
                // Si le fichier n'existe pas, créez un nouveau fichier et sérialisez les données
                using (FileStream stream = File.Create("score.json"))
                {
                    // Sérialise la liste de scores dans le fichier JSON
                    serializer.WriteObject(stream, scores);
                }
            }
        }

        /// <summary>
        /// Deserialise le ficher de score
        /// </summary>
        /// <returns>La liste des scores du fichier sérialisé</returns>
        /// <author>Nordine</author>
        public List<(string nom, double score)> DeserialiserScores()
        {
            //Lecture du JSON
            using (FileStream stream = File.OpenRead("score.json"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<(string nom, double score)>));
                List<(string nom, double score)> saveScores = ser.ReadObject(stream) as List<(string nom, double score)>;
                saveScores = saveScores.OrderByDescending(score => score.score).ToList();
                return saveScores;
            }
        }

    }
}
