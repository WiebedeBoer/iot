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



//using static Android.Widget.Toast;

namespace Domotica
{
    [Activity(Label = "clocktest" ,MainLauncher = true)]
    public class Alarmcontroller : Activity
    {
       // public static Alarmcontroller instance = null;

        Toast repeating;

        EditText timertext;
        TimePicker timeselector;
        //  Button oneshotAlarm;
        //  Button repeatingAlarm;
        //  Button stoprepeatingAlarm;

        PowerManager.WakeLock wl;
        Vibrator vibro;



        protected override void OnCreate(Bundle savedInstanceState)
        {
           


            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.my_activity);

            FindViewById<Button>(Resource.Id.oneshotAlarm).Click += OneShotClick;

           // FindViewById<Button>(Resource.Id.repeatingAlarm).Click += StartRepeatingClick;
            //FindViewById<Button>(Resource.Id.stoprepeatingAlarm).Click += StopRepeatingClick;

           timeselector = FindViewById<TimePicker>(Resource.Id.timePicker);

            timertext = FindViewById<EditText>(Resource.Id.timertext);
           // PowerManager pw = (PowerManager)GetSystemService(PowerService);
            // wl = pw.NewWakeLock(WakeLockFlags.Full | WakeLockFlags.AcquireCausesWakeup, "Tagged");

            vibro = (Vibrator)GetSystemService(Context.VibratorService);

        }


        void OneShotClick(object sender, EventArgs e)
        {
            // When the alarm goes off, we want to broadcast an Intent to our
            // BroadcastReceiver.  Here we make an Intent with an explicit class
            // name to have our own receiver (which has been published in
            // AndroidManifest.xml) instantiated and called, and then create an
            // IntentSender to have the intent executed as a broadcast.

            int timeHour = Convert.ToInt32(timeselector.Hour);
            int timeMinutes = Convert.ToInt32(timeselector.Minute);        

            long q = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            AlarmManager am = (AlarmManager)GetSystemService(AlarmService);
            Intent oneshotIntent = new Intent(this, typeof(OneShotAlarm));
            PendingIntent source = PendingIntent.GetBroadcast(this, 0, oneshotIntent, 0);

            vibro.Vibrate(500);


            Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
            calendar.Set(CalendarField.HourOfDay, timeHour);
            calendar.Set(CalendarField.Minute, timeMinutes);
            am.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, source);

            // Tell the user about what we did.
            if (repeating != null)
                repeating.Cancel();
            repeating = Toast.MakeText(this, Resource.String.one_shot_scheduled, ToastLength.Long);
            repeating.Show();


        }

        public  void WakeMeUpInside()
        {
            // Make a powermanager, set the wakelocks defined earlier
            // Be carefull moving this, moving anything outside this funciton can break it. Don't ask why. Just Xamarin
            PowerManager pw = (PowerManager)GetSystemService(PowerService);      
            wl = pw.NewWakeLock(WakeLockFlags.Full | WakeLockFlags.AcquireCausesWakeup, "Tagged");
            wl.Acquire();         
        }
                /*

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
          */
    }
    [BroadcastReceiver(Enabled = true)]
    public class OneShotAlarm : BroadcastReceiver
    {
        // reference to the main activity

       // activity.Wake
        /*
        public OneShotAlarm(Alarmcontroller mawmaw) {
           // activity = mawmaw;
          
        }
        
        public  OneShotAlarm()        
        {
            // Default constructor needed for Xamarin Forms bug?
            throw new Exception("This constructor should not actually ever be used");
        }
        */



        //             ActivityManager am = (ActivityManager)context.getSystemService(Context.ACTIVITY_SERVICE);
        //    ComponentName cn = am.getRunningTasks(1).get(0).topActivity;

        public override void OnReceive(Context context, Intent intent)
        {
            
       
            context.(intent);

            //Intent intent = new Intent(this, typeof(Alarmcontroller));
          //  StartActivity(intent);
            //var q = context;
            //MainActivity p = (MainActivity)(Activity) context;
            //Alarmcontroller activity = (Alarmcontroller )q;

            //  activity.WakeMeUpInside();
            //Alarmcontroller activity = (Alarmcontroller)context;
            //testcode
            //   context.SendBroadcast(new Intent("FUCKFUCK"));
            // PowerManager pw = (PowerManager)context.GetSystemService(Context.PowerService);
            //  PowerManager.WakeLock wl = pw.NewWakeLock(WakeLockFlags.Full, "Tagged");

            //pw.GoToSleep(10000);
            //wl.Release
            //  wl.Acquire();
            // saving as var for debugging
            // var c = context;
            //  var i = intent;
            //var activity = (Activity)Forms.Context;
            // context.StartActivity(intent);
            //if (true)
            //{
            //    //context is Alarmcontroller
            //    activity = (Alarmcontroller)context;
            //    activity.WakeMeUpInside();

            //}

            // Try to make a new AlarmController

            //Intent intent = new Intent(this, typeof(Alarmcontroller));
            //   activity = new Alarmcontroller();
            //activity.OnCreate();
            //  activity.WakeMeUpInside();

            //intent.
            //Toast.MakeText(this, Resource.String.ip_port_text, ToastLength.Short).Show();
            Toast.MakeText(context, "Onreceive activated oneshot", ToastLength.Short).Show();
            //activity.WakeMeUpInside();
        }
    }

}
