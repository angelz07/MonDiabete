using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Fichiers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MonDiabete.Class
{
    public class Notifications
    {
        Tools Tools = new Tools();
        public void Sendnotification(string vue, string message) {
            DateTime dateTimeToConvert = DateTime.ParseExact(VariablesGlobal.HeureProchaineMesure, "H:mm", null, System.Globalization.DateTimeStyles.None);
            DateTime DateNotif = dateTimeToConvert.AddSeconds(20);
            // DateTime DateNotif = Tools.ConvertTimeStringToTime(VariablesGlobal.HeureProchaineMesure);
            // var dateTimeToConvert = DateTime.ParseExact(messageLocal, "H:mm", null, System.Globalization.DateTimeStyles.None);
            // DateTime dateNow = DateTime.Now.AddSeconds(30);
          string TimetringNotif = DateNotif.ToString("H:mm");
            var TimeNotif = DateTime.ParseExact(TimetringNotif, "H:mm", null, System.Globalization.DateTimeStyles.None);
            try
            {
                DependencyService.Get<INotification>().SendAlarmNotif(TimeNotif, "Mon Diabète", message);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERREUR   ===========  " + e.Message);
               
            }
           
          //  Console.WriteLine("Infos", "VariablesGlobal.MessageNotification = " + VariablesGlobal.MessageNotification + " -- Vue = " + vue, "ok");
        }
    }
}
