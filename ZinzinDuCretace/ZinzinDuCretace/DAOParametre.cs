using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ZinzinDuCretace
{
    /// <summary>
    /// "Factory" de parametre
    /// </summary>
    /// <author>Nordine</author>
    public class DAOParametre
    {
        /// <summary>
        /// Constructeur de "Factory" de parametre
        /// </summary>
        /// <author>Nordine</author>
        public DAOParametre() 
        {
            
        }

        /// <summary>
        /// Créer un parametre (ou récupere les données s'il y a une sauvegarde)
        /// </summary>
        /// <returns>parametre</returns>
        /// <author>Nordine</author>
        public Parametre CreerParametre()
        {
            Parametre newParametre;

            //On récupere (si il existe) la sauvegarde des paramètres
            if (File.Exists("parametre.json"))
            {
                newParametre = DeserialiserParametre();
            }
            //Sinon on creer des parametre avec les valeurs par défauts
            else
            {
                newParametre = new Parametre();
            }

            return newParametre;
        }

        /// <summary>
        /// Deserialise le fichier de parametre
        /// </summary>
        /// <returns>Parametre deserialisé</returns>
        /// <author>Nordine</author>
        private Parametre DeserialiserParametre()
        {
            //Lecture du JSON
            using (FileStream stream = File.OpenRead("parametre.json"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Parametre));
                Parametre saveParametre = ser.ReadObject(stream) as Parametre;
                return saveParametre;
            }
        }
    }
}
