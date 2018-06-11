using Android.App;
using Android.Widget;
using Android.OS;

namespace b_opdracht
{
    [Activity(Label = "b_opdracht", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //variables
        //controls on GUI
        Button ToggleKoffie;
        Button ToggleLicht;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            ToggleKoffie = FindViewById<Button>(Resource.Id.ToggleKoffie);
            ToggleLicht = FindViewById<Button>(Resource.Id.ToggleLicht);
        }
    }
}

