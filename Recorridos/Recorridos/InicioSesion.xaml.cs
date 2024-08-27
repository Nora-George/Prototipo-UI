using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recorridos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioSesion : ContentPage
    {
        Peticiones peticion = new Peticiones();
        public InicioSesion()
        {
            InitializeComponent();

            String CorreoElectronico = Preferences.Get("CorreoElectronico", "0");
            String Contraseña = Preferences.Get("Contraseña", "0");
        }

        private void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            Modelo.InicioSesion login = new Modelo.InicioSesion
            {
                CorreoElectronico = txtCorreoElectronico.Text,
                Contraseña = txtContraseña.Text
            };

            peticion.pedirJWT(login);

            String jwt = Preferences.Get("jwt", "0");

            if (Preferences.Get("jwt", "0") != "0")
            {
                Preferences.Set("CorreoElectronico", txtCorreoElectronico.Text);
                Preferences.Set("Contraseña", txtContraseña.Text);

                peticion.pedirUsuarioID();

                ((NavigationPage)this.Parent).PushAsync(new RegistrarRecorrido());
            }
            else
            {
                DisplayAlert("Error", "Correo electrónico o contraseña incorrectos", "Aceptar");
            }
        }
    }
}