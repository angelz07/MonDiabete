using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Media;

namespace MonDiabete.Droid.Class
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            // Uri uri = RingtoneManager.GetDefaultUri(RingtoneManager.TYPE_ALARM);
            // Ringtone ringtone = RingtoneManager.getRingtone(context, uri);
            // ringtone.play();

            // context.StartActivity(new Intent(context, typeof(MainActivity)));
            Console.WriteLine("************** ALARM ******************** ");
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            Console.WriteLine("message = " + message);

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            var manager = NotificationManagerCompat.From(context);
           
          //  var style = new NotificationCompat.BigTextStyle();
          //  style.BigText(message);


            long[] Pattern = new long[] { 1000, 1000, 1000, 1000, 1000 };
           
            var builder = new NotificationCompat.Builder(context)
                .SetContentIntent(contentIntent)
                .SetSmallIcon(Resource.Drawable.design_ic_visibility_off)
                .SetContentTitle(title)
                .SetContentText(message)
              //  .SetStyle(style)
                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                .SetAutoCancel(true)
                .SetVibrate(Pattern)
                .SetLights(Color.Red, 3000, 3000)
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetFullScreenIntent(contentIntent, true)
                .SetAutoCancel(true);

            var notification = builder.Build();
            manager.Notify(1243, notification);
           


            Android.Content.Intent start = new Android.Content.Intent(context, typeof(MainActivity));
            start.AddFlags(ActivityFlags.NewTask);
            start.AddFlags(ActivityFlags.FromBackground);
            context.ApplicationContext.StartActivity(start);
        }
    }
}