using MonDiabete.Fichiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonDiabete.Class
{
    public class Notifications
    {
        public void Sendnotification(string vue) {
            Console.WriteLine("Infos", "VariablesGlobal.MessageNotification = " + VariablesGlobal.MessageNotification + " -- Vue = " + vue, "ok");
        }
    }
}
