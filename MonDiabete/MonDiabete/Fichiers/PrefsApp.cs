using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MonDiabete.Fichiers
{
    public class PrefsApp
    {
        public static string fileUserPref = "preferences.json";
        public static string fileConfigGlycemie = "glycemie.json";
        public static string fileMatinGlycemie = "HistoriqueMatinGlycemie.json";
        public static string fileMidiGlycemie = "HistoriqueMidiGlycemie.json";
        public static string fileSoirGlycemie = "HistoriqueSoirGlycemie.json";
        public static IFormatProvider cultureApp = new CultureInfo("fr-FR", true);
        public static string ApiAddress = "http://web2.telecom4all.be/diabete_assistant/www/api";

    }
}
