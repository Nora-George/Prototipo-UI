using HTTPupt;
using Recorridos.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Recorridos
{
    class Peticiones
    {
        PeticionHTTP peticion = new PeticionHTTP("http://arquidinter.sytes.net");
        public void pedirJWT(Modelo.InicioSesion login)
        {
            String json = JsonConvertidor.Objeto_Json(login);
            peticion.PedirComunicacion("Usuario/PedirToken", MetodoHTTP.POST, TipoContenido.JSON);
            peticion.enviarDatos(json);
            json = peticion.ObtenerJson();

            if (json != "null")
                Preferences.Set("jwt", json.Substring(1, length: json.Length - 2));
            else
                Preferences.Set("jwt", "0");
        }

        public void pedirUsuarioID()
        {
            Modelo.InicioSesion login = new Modelo.InicioSesion
            {
                CorreoElectronico = Preferences.Get("CorreoElectronico", "0"),
                Contraseña = Preferences.Get("Contraseña", "0")
            };

            pedirJWT(login);

            String jwt = Preferences.Get("jwt", "0");
            peticion.PedirComunicacion("Usuario/ConsultarPorUsuario", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<UsuarioDTO> usuario = JsonConvertidor.Json_ListaObjeto<UsuarioDTO>(json);

            Preferences.Set("usuarioID", usuario[0].ID.ToString());
        }

        public List<Zona> consultarZonas()
        {
            peticion.PedirComunicacion("Zona/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<Zona> zonas = JsonConvertidor.Json_ListaObjeto<Zona>(json);

            return zonas;
        }
        public List<Concepto> consultarConceptos()
        {
            peticion.PedirComunicacion("Concepto/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<Concepto> conceptos = JsonConvertidor.Json_ListaObjeto<Concepto>(json);

            return conceptos;
        }

        public List<Parte> consultarPartes()
        {
            peticion.PedirComunicacion("Parte/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<Parte> partes = JsonConvertidor.Json_ListaObjeto<Parte>(json);

            return partes;
        }

        public List<Piso> consultarPisos()
        {
            peticion.PedirComunicacion("Piso/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<Piso> pisos = JsonConvertidor.Json_ListaObjeto<Piso>(json);

            return pisos;
        }

        public List<ZonaConceptoParte> consultarZonaConceptoParte()
        {
            peticion.PedirComunicacion("ZonaConceptoParte/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<ZonaConceptoParte> zonaConceptoParte = JsonConvertidor.Json_ListaObjeto<ZonaConceptoParte>(json);

            return zonaConceptoParte;
        }

        public List<Recorrido> consultarRecorrido()
        {
            peticion.PedirComunicacion("Recorrido/Consultar", MetodoHTTP.GET, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            String json = peticion.ObtenerJson();

            List<Recorrido> recorridos = JsonConvertidor.Json_ListaObjeto<Recorrido>(json);

            return recorridos;
        }

        public bool guardarRecorrido(List<Recorrido> recorridos)
        {
            Modelo.InicioSesion login = new Modelo.InicioSesion
            {
                CorreoElectronico = Preferences.Get("CorreoElectronico", "0"),
                Contraseña = Preferences.Get("Contraseña", "0")
            };

            pedirJWT(login);

            String enviar = JsonConvertidor.Objeto_Json(recorridos);
            peticion.PedirComunicacion("Recorrido/InsertarLista", MetodoHTTP.POST, TipoContenido.JSON, Preferences.Get("jwt", "0"));
            peticion.enviarDatos(enviar);
            String json = peticion.ObtenerJson();

            Comprobacion comp = JsonConvertidor.Json_Objeto<Comprobacion>(json);
            return comp.Estado;
        }
    }
}
