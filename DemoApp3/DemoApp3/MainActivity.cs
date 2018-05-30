using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DemoApp3
{
    [Activity(Label = "DemoApp3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            TextView text = FindViewById<TextView>(Resource.Id.MyText);

            button.Text = "start";
            text.Text = "0";

            System.Timers.Timer timer = new System.Timers.Timer() { Interval = 1000, Enabled = false };

            timer.Elapsed += (obj, args) => 
            { RunOnUiThread(
                () => 
                {
                    int i = int.Parse(text.Text) + 1;
                    text.Text = i.ToString();
                });
            };

            button.Click += (obj, args) => 
            {
                timer.Enabled = !timer.Enabled;
                if(timer.Enabled)
                {
                    button.Text = "stop";
                    text.Text = "0";
                }
                else
                {
                    button.Text = "start";
                }
            };
        }
    }
}

