using Recorridos.Modelo;
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
    public partial class RegistrarRecorrido : TabbedPage
    {
        ModeloBD bd = new ModeloBD();
        int zonaID;
        int pisoID;
        int conceptoID;
        int parteID;
        int zonaConceptoParteID;
        int parteSeleccionada;
        String estado = "";

        List<ParteDTO> listaPartes = new List<ParteDTO>();

        Peticiones peticion = new Peticiones();
        public RegistrarRecorrido()
        {
            InitializeComponent();

            var recorridos = bd.Recorrido;

            if (recorridos.Count() > 0)
            {
                btnSincronizar.IsEnabled = true;
                btnSincronizar.Text = "Hay registros por sincronizar";
            }
            else
            {
                btnSincronizar.IsEnabled = false;
                btnSincronizar.Text = "No hay registros para sincronizar";
            }

            var zonas = from consulta in bd.Zona
                        select new ZonaDTO
                        {
                            ID = consulta.ID,
                            Nombre = consulta.Nombre
                        };

            ddlTorreAmenidad.ItemsSource = zonas.ToList();
            ddlTorreAmenidad.ItemDisplayBinding = new Binding("Nombre");

            var pisos = from consulta in bd.Piso
                        select new PisoDTO
                        {
                            ID = consulta.ID,
                            Nombre = consulta.Nombre
                        };

            ddlPiso.ItemsSource = pisos.ToList();
            ddlPiso.ItemDisplayBinding = new Binding("Nombre");
        }

        private void btnActualizarBD_Clicked(object sender, EventArgs e)
        {
            Modelo.InicioSesion login = new Modelo.InicioSesion
            {
                CorreoElectronico = Preferences.Get("CorreoElectronico", "0"),
                Contraseña = Preferences.Get("Contraseña", "0")
            };

            peticion.pedirJWT(login);

            List<Zona> zonas = peticion.consultarZonas();

            foreach (Zona zona in zonas)
            {
                var consulta = bd.Zona.Where(c => c.ID == zona.ID).FirstOrDefault();

                if (consulta == null)
                {
                    bd.Zona.Add(zona);
                    bd.SaveChanges();
                }
            }

            List<Concepto> conceptos = peticion.consultarConceptos();

            foreach (Concepto concepto in conceptos)
            {
                var consulta = bd.Concepto.Where(c => c.ID == concepto.ID).FirstOrDefault();

                if (consulta == null)
                {
                    bd.Concepto.Add(concepto);
                    bd.SaveChanges();
                }
            }

            List<Parte> partes = peticion.consultarPartes();

            foreach (Parte parte in partes)
            {
                var consulta = bd.Parte.Where(c => c.ID == parte.ID).FirstOrDefault();

                if (consulta == null)
                {
                    bd.Parte.Add(parte);
                    bd.SaveChanges();
                }
            }

            List<Piso> pisos = peticion.consultarPisos();

            foreach (Piso piso in pisos)
            {
                var consulta = bd.Piso.Where(c => c.ID == piso.ID).FirstOrDefault();

                if (consulta == null)
                {
                    bd.Piso.Add(piso);
                    bd.SaveChanges();
                }
            }

            List<ZonaConceptoParte> zonasConceptosPartes = peticion.consultarZonaConceptoParte();

            foreach (ZonaConceptoParte zonaConceptoParte in zonasConceptosPartes)
            {
                var consulta = bd.ZonaConceptoParte.Where(c => c.ID == zonaConceptoParte.ID).FirstOrDefault();

                if (consulta == null)
                {
                    bd.ZonaConceptoParte.Add(zonaConceptoParte);
                    bd.SaveChanges();
                }
            }

            DisplayAlert("Aviso", "La base de datos se actulizó correctamente", "Aceptar");
        }

        private void ddlTorreAmenidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                ZonaDTO zona = (ZonaDTO)picker.SelectedItem;
                zonaID = zona.ID;

                ddlPiso.SelectedIndex = -1;
                ddlConcepto.SelectedIndex = -1;
                ddlParte.SelectedIndex = -1;
                ddlEstado.SelectedIndex = -1;
                txtObservaciones.Text = "";
            }
        }

        private void ddlPiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                PisoDTO piso = (PisoDTO)picker.SelectedItem;
                pisoID = piso.ID;

                var conceptos = from consulta in bd.Concepto
                                select new ConceptoDTO
                                {
                                    ID = consulta.ID,
                                    Nombre = consulta.Nombre
                                };

                ddlConcepto.ItemsSource = conceptos.ToList();
                ddlConcepto.ItemDisplayBinding = new Binding("Nombre");

                ddlConcepto.SelectedIndex = -1;
                ddlParte.SelectedIndex = -1;
                ddlEstado.SelectedIndex = -1;
                txtObservaciones.Text = "";
            }
        }

        private void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                if (ddlParte.ItemsSource != null)
                    ddlParte.ItemsSource.Clear();

                ConceptoDTO concepto = (ConceptoDTO)picker.SelectedItem;
                conceptoID = concepto.ID;

                var partes = from consulta in bd.ZonaConceptoParte
                             where consulta.ZonaID == zonaID && consulta.ConceptoID == conceptoID
                             select new ParteDTO
                             {
                                 ID = consulta.ParteID,
                                 Nombre = consulta.Parte.Nombre,
                                 ZonaConceptoParteID = consulta.ID
                             };

                foreach (ParteDTO parte in partes)
                {
                    var elemento = listaPartes.Where(c => c.Nombre == parte.Nombre);

                    if (elemento.Count() == 0)
                    {
                        listaPartes.Add(parte);
                    }
                }

                ddlParte.ItemsSource = listaPartes;
                ddlParte.ItemDisplayBinding = new Binding("Nombre");

                ddlParte.SelectedIndex = -1;
                ddlEstado.SelectedIndex = -1;
                txtObservaciones.Text = "";
            }
        }

        private void ddlParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ParteDTO> listaPartes = new List<ParteDTO>();
            Picker picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                ParteDTO parte = (ParteDTO)picker.SelectedItem;
                parteID = parte.ID;
                parteSeleccionada = picker.SelectedIndex;

                zonaConceptoParteID = parte.ZonaConceptoParteID;
                ddlEstado.SelectedIndex = -1;
                txtObservaciones.Text = "";
            }
        }
        private void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ParteDTO> listaPartes = new List<ParteDTO>();
            Picker picker = (Picker)sender;

            if (picker.SelectedIndex != -1)
            {
                int tamaño = picker.SelectedItem.ToString().Length;
                estado = picker.SelectedItem.ToString().Substring(1, tamaño - 2); ;
            }
        }
        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (ddlTorreAmenidad.SelectedIndex != -1 && ddlPiso.SelectedIndex != -1 && ddlConcepto.SelectedIndex != -1 && ddlParte.SelectedIndex != -1 && ddlEstado.SelectedIndex != -1)
            {
                Epoch epoch = new Epoch();
                double fecha = epoch.convertirEpoch(DateTime.Now);

                Recorrido recorrido = new Recorrido
                {
                    Estado = estado,
                    Fecha = fecha,
                    PisoID = pisoID,
                    UsuarioID = int.Parse(Preferences.Get("usuarioID", "0")),
                    ZonaConceptoParteID = zonaConceptoParteID,
                    observaciones = txtObservaciones.Text
                };

                try
                {
                    bd.Recorrido.Add(recorrido);
                    bd.SaveChanges();

                    listaPartes.RemoveAt(parteSeleccionada);
                    //ddlParte.ItemsSource.Clear();
                    ddlParte.ItemsSource = listaPartes;
                    ddlParte.ItemDisplayBinding = new Binding("Nombre");
                    ddlParte.SelectedIndex = -1;
                    ddlEstado.SelectedIndex = -1;
                    txtObservaciones.Text = "";

                    btnSincronizar.IsEnabled = true;
                    btnSincronizar.Text = "Hay registros por sincronizar";

                    DisplayAlert("Aviso", "Registro guardado correctamente", "Aceptar");
                }
                catch (Exception ex)
                {
                    String excepcion = "Error: " + ex.Message;
                    DisplayAlert("Error", "No se pudo guardar el registro", "Aceptar");
                }
            }
            else
            {
                DisplayAlert("Error", "Verifique que haya seleccionado todos las opciones", "Aceptar");
            }
        }

        private void btnSincronizar_Clicked(object sender, EventArgs e)
        {
            List<Recorrido> guardarRecorridos = new List<Recorrido>();

            var recorridos = bd.Recorrido;

            foreach (Recorrido recorrido in recorridos)
            {
                Recorrido registro = new Recorrido
                {
                    Estado = recorrido.Estado,
                    Fecha = recorrido.Fecha,
                    observaciones = recorrido.observaciones,
                    PisoID = recorrido.PisoID,
                    UsuarioID = recorrido.UsuarioID,
                    ZonaConceptoParteID = recorrido.ZonaConceptoParteID
                };
                guardarRecorridos.Add(registro);
            }

            bool respuesta = peticion.guardarRecorrido(guardarRecorridos);

            if (respuesta == true)
            {
                foreach (Recorrido recorrido in recorridos)
                {
                    bd.Recorrido.Remove(recorrido);
                }
                bd.SaveChanges();

                btnSincronizar.IsEnabled = false;
                btnSincronizar.Text = "No hay registros para sincronizar";
                DisplayAlert("Aviso", "Registros sincronizados correctamente", "Aceptar");
            }
            else
            {
                DisplayAlert("Aviso", "Registro guardado correctamente", "Aceptar");
            }
        }
    }
}