using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recorridos
{
    public partial class App : Application
    {
        Peticiones peticion = new Peticiones();

        public App()
        {
            InitializeComponent();

            String CorreoElectronico = Preferences.Get("CorreoElectronico", "0");
            String Contraseña = Preferences.Get("Contraseña", "0");

            if (CorreoElectronico != "0" && Contraseña != "0")
            {
                //Preferences.Set("CorreoElectronico", "0");
                //Preferences.Set("Contraseña", "0");
                MainPage = new NavigationPage(new RegistrarRecorrido());
            }
            else
            {
                MainPage = new NavigationPage(new InicioSesion());
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
