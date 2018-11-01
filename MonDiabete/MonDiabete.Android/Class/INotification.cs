using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using MonDiabete.Class;
using MonDiabete.Class.Dependency_Services_Class;
using MonDiabete.Droid.Class;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceNotification))]
namespace MonDiabete.Droid.Class
{
    public class DeviceNotification : INotification
    //  class INotification
    {
       // private Context context;

        public Context context = Android.App.Application.Context;

        public void SendAlarmNotif(DateTime dateTime, string title, string message)
        {

        //    android.content.Context.getPackageName()

        //    Context context = this.context;

        Intent alarmIntent = new Intent(context, typeof(AlarmReceiver));
              alarmIntent.PutExtra("message", message);
              alarmIntent.PutExtra("title", title);

              PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
              AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
             // Locale locale = new Locale("French", "Belgium");

              var tDiff = new DateTime(1970, 1, 1) - DateTime.MinValue;
              var utcAlarmTimeInMillis = dateTime.ToUniversalTime().AddSeconds(-tDiff.TotalSeconds).Ticks / 10000;


              // For KitKat and higher use SetExact for preserving battery life, 
              // for previous versions use Set:
              if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
                  alarmManager.SetExact(AlarmType.RtcWakeup, utcAlarmTimeInMillis, pendingIntent);
              else
                  alarmManager.Set(AlarmType.RtcWakeup, utcAlarmTimeInMillis, pendingIntent);

            Console.WriteLine("test 1 = " + utcAlarmTimeInMillis.ToString());
             
        }
    }
}