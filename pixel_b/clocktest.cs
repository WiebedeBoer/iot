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


//using static Android.Widget.Toast;

namespace Domotica
{
    [Activity(Label = "clocktest")]
    public class Alarmcontroller : Activity
    {
        Toast repeating;
        //  Button oneshotAlarm;
        //Button repeatingAlarm;
        //  Button stoprepeatingAlarm;
        // AlarmReceiver alarmy = new AlarmReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.my_activity);


            // Create your application here
            FindViewById<Button>(Resource.Id.oneshotAlarm).Click += OneShotClick;

            FindViewById<Button>(Resource.Id.repeatingAlarm).Click += StartRepeatingClick;

            FindViewById<Button>(Resource.Id.stoprepeatingAlarm).Click += StopRepeatingClick;
        }


        void OneShotClick(object sender, EventArgs e)
        {
            // When the alarm goes off, we want to broadcast an Intent to our
            // BroadcastReceiver.  Here we make an Intent with an explicit class
            // name to have our own receiver (which has been published in
            // AndroidManifest.xml) instantiated and called, and then create an
            // IntentSender to have the intent executed as a broadcast.

            AlarmManager am = (AlarmManager)GetSystemService(AlarmService);
            Intent oneshotIntent = new Intent(this, typeof(OneShotAlarm));
            PendingIntent source = PendingIntent.GetBroadcast(this, 0, oneshotIntent, 0);
            // my code
            AlarmManager.AlarmClockInfo p = new AlarmManager.AlarmClockInfo(1, source);
            // end my code
            // Schedule the alarm for 10 seconds from now!


            am.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1 * 1000, source);
            //  am.SetAlarmClock(p,source);


            // Tell the user about what we did.
            if (repeating != null)
                repeating.Cancel();
            repeating = Toast.MakeText(this, Resource.String.one_shot_scheduled, ToastLength.Long);
            repeating.Show();
        }

        void StartRepeatingClick(object sender, EventArgs e)
        {
            // When the alarm goes off, we want to broadcast an Intent to our
            // BroadcastReceiver.  Here we make an Intent with an explicit class
            // name to have our own receiver (which has been published in
            // AndroidManifest.xml) instantiated and called, and then create an
            // IntentSender to have the intent executed as a broadcast.
            // Note that unlike above, this IntentSender is configured to
            // allow itself to be sent multiple times.
            var intent = new Intent(this, typeof(RepeatingAlarm));
            var source = PendingIntent.GetBroadcast(this, 0, intent, 0);

            // Schedule the alarm!
            var am = (AlarmManager)GetSystemService(AlarmService);
            am.SetRepeating(AlarmType.ElapsedRealtimeWakeup,
                    SystemClock.ElapsedRealtime() + 15 * 1000,
                    15 * 1000,
                    source);

            // Tell the user about what we did.
            if (repeating != null)
                repeating.Cancel();
            Toast.MakeText(this, "StartRepeatingClick ", ToastLength.Short).Show(); ;
            repeating.Show();
        }

        void StopRepeatingClick(object sender, EventArgs e)
        {
            // Create the same intent, and thus a matching IntentSender, for
            // the one that was scheduled.
            var intent = new Intent(this, typeof(RepeatingAlarm));
            var source = PendingIntent.GetBroadcast(this, 0, intent, 0);

            // And cancel the alarm.
            var am = (AlarmManager)GetSystemService(AlarmService);
            am.Cancel(source);

            // Tell the user about what we did.
            if (repeating != null)
                repeating.Cancel();
            Toast.MakeText(this, "StopRepeatingClick", ToastLength.Short).Show(); ;
            repeating.Show();
        }
    }
}
[BroadcastReceiver(Enabled = true)]
public class OneShotAlarm : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        //Toast.MakeText(this, Resource.String.ip_port_text, ToastLength.Short).Show();
        Toast.MakeText(context, "Onreceive activated oneshot", ToastLength.Short).Show();
    }
}

public class RepeatingAlarm : BroadcastReceiver
{
    public override void OnReceive(Context context, Intent intent)
    {
        Toast.MakeText(context, "Onreceive activated repeating", ToastLength.Short).Show(); ;
    }
}