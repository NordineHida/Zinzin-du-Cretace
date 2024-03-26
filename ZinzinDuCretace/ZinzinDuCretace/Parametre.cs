using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ZinzinDuCretace
{
    /// <summary>
    /// Les parametres du jeu
    /// </summary>
    /// <author>Daouda/Nordine</author>
    [DataContract]
    public class Parametre 
    {
        /// <summary>
        /// Bool qui indique si la case sound effect est activée
        /// </summary>
        /// <author>Daouda</author>
        [DataMember]
        private bool soundEffect;

        /// <summary>
        /// Bool qui indique si la case de fullscreen est activée
        /// </summary>
        /// <author>Daouda</author>
        [DataMember]
        private bool fullscreen;

        /// <summary>
        /// Get Bool qui indique si la case sound effect est activée
        /// </summary>
        /// <author>Daouda</author>
        public bool SoundEffect { get { return soundEffect; } set { soundEffect = value; } }

        /// <summary>
        /// Get Bool qui indique si la case de fullscreen est activée
        /// </summary>
        /// <author>Daouda</author>
        public bool Fullscreen { get { return fullscreen; } set { fullscreen = value; } }

        /// <summary>
        /// Volume du jeu
        /// </summary>
        /// <author>Daouda</author>
        [DataMember]
        private double volume;

        /// <summary>
        /// le jeu
        /// </summary>
        /// <author>Nordine/Daouda</author>
        private LeJeu jeu;

        /// <summary>
        /// Le jeu
        /// </summary>
        /// <author>Nordine/Daouda</author>
        public LeJeu Jeu { get { return jeu; } set { this.jeu = value; } }

        /// <summary>
        /// Get volume du jeu
        /// </summary>
        /// <author>Daouda</author>
        public double Volume { get { return volume; } set { volume = value; } }

        /// <summary>
        /// Constructeur de parametre
        /// </summary>
        /// <author>Daouda/Nordine</author>
        public Parametre() 
        {
            this.jeu = null;
            soundEffect = true;
            fullscreen = false;
            volume = 0.6;
        }

        /// <summary>
        /// Change le volume en fonction de la valeur du slider
        /// </summary>
        /// <param name="ValeurSlider">valeur du slider du volume</param>
        /// <author>Daouda/Nordine</author>
        public void ChangeVolume(double ValeurSlider)
        {
            volume = ValeurSlider;
            if (this.jeu != null)
            {
                jeu.BackgroundVolume = ValeurSlider;
            }

        }

    }
}
