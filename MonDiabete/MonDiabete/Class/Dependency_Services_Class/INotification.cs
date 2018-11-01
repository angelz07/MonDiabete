using System;
using System.Collections.Generic;
using System.Text;

namespace MonDiabete.Class.Dependency_Services_Class
{
    public interface INotification
    {
        void SendAlarmNotif(DateTime dateTime, string title, string message);
      //  void SendAlarmNotifMessage(DateTime dateTime, string title, string message);

    }

}
