using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonDiabete.Fichiers
{
    public class VariablesGlobal
    {
        /* Mesure */
        public static bool MesureIsActive { get; set; }
        public static string GlycemieMesure { get; set; }
        public static string InsulineNbUnite { get; set; }

        /* Message Alert */
        public static string MessageAlertGeneralUI { get; set; }
        public static string MessageAlertTypeGeneralUI { get; set; }
        public static Color MessageAlertColorGeneralUI { get; set; }
        public static string MessageAlertMesureUI { get; set; }
        public static string MessageAlertTypeMesurelUI { get; set; }
        public static Color MessageAlertColorMesurelUI { get; set; }

        /* Horloges */
        public static bool _isRunningMainTimer = false;
        public static bool _isRunningMesureTimer = false;
        public static string HeureProchaineMesure { get; set; }
        public static string HeureProchaineMesureMessage { get; set; }
        public static string NomMomentRefMesure { get; set; }
        public static string HeureRefMesure { get; set; }

        /* notifications */
        public static string MessageNotification { get; set; }

        /* Infos Utilisateur */
        public static string ApiKey { get; set; }
        public static string DateNaissance { get; set; }
        public static string Gsm { get; set; }
        public static string GsmContact { get; set; }
        public static string HeureMatin { get; set; }
        public static string HeureMidi { get; set; }
        public static string HeureSoir { get; set; }
        public static string Id { get; set; }
        public static string Mail { get; set; }
        public static string MailContact { get; set; }
        public static string Nom  { get; set; }

        public static string NomContact { get; set; }
        public static string Prenom { get; set; }
        public static string PrenomContact { get; set; }
        public static string Recorded { get; set; }


        /* Infos Glycemie */
        public static string Glycemie101A150Matin { get; set; }
        public static string Glycemie101A150Midi { get; set; }
        public static string Glycemie101A150Soir { get; set; }

        public static string Glycemie151A200Matin { get; set; }
        public static string Glycemie151A200Midi { get; set; }
        public static string Glycemie151A200Soir { get; set; }

        public static string Glycemie201A250Matin { get; set; }
        public static string Glycemie201A250Midi { get; set; }
        public static string Glycemie201A250Soir { get; set; }

        public static string Glycemie251A300Matin { get; set; }
        public static string Glycemie251A300Midi { get; set; }
        public static string Glycemie251A300Soir { get; set; }

        public static string Glycemie70A100Matin { get; set; }
        public static string Glycemie70A100Midi { get; set; }
        public static string Glycemie70A100Soir { get; set; }
        
        public static string GlycemieMoins70Matin { get; set; }
        public static string GlycemieMoins70Midi { get; set; }
        public static string GlycemieMoins70Soir { get; set; }

        public static string GlycemiePlus300Matin { get; set; }
        public static string GlycemiePlus300Midi { get; set; }
        public static string GlycemiePlus300Soir { get; set; }

        public static string GlycemieConfigRecorded { get; set; }
        
    }
}
