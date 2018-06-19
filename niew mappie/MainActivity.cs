using Android.App;
using Android.Widget;
using Android.OS;
using System.Timers;
using System;

namespace timer_onderdeel
{
    [Activity(Label = "timer_onderdeel", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button btnCancel;
        private Button Set;
        private TextView txtCountdown;
        private int count = 0;
        private int countdown;
        Timer timer;
        private EditText tijd;
        private string time;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            Set = FindViewById<Button>(Resource.Id.set);
            txtCountdown = FindViewById<TextView>(Resource.Id.txtCountdown);
            btnCancel.Click += BtnCancel_Click;
            Set.Click += Set_Click;
            tijd = FindViewById<EditText>(Resource.Id.tijd);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Set.Enabled = true;
            txtCountdown.Text = "stop";
            timer.Stop();
        }

        private void Set_Click(object sender, EventArgs e)
        {
            time = tijd.Text;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed; // 1 seconds
            timer.Start();
            Set.Enabled = false;
            count = 0;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (count < Convert.ToInt32(time))
            {
                count++; // increase count variable
                RunOnUiThread(() =>
                {
                    countdown = (Convert.ToInt32(time) + 1) - count;
                    int seconds = countdown % 60;
                    int minutes = countdown / 60;
                    txtCountdown.Text = minutes + ":" + seconds;
                });
            }
            else
            {
                RunOnUiThread(() => {
                    count = Convert.ToInt32(time); // Reset count variable
                    Toast.MakeText(this, "Hello", ToastLength.Short).Show();
                    timer.Stop();
                    countdown = Convert.ToInt32(time) - count;
                    int seconds = countdown % 60;
                    int minutes = countdown / 60;
                    txtCountdown.Text = minutes + ":" + seconds;
                    Set.Enabled = true;
                });
            }
        }
    }
}

