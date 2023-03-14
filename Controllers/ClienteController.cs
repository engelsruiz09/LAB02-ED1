using LAB02_ED1_G.Models;
using LAB02_ED1_G.Models.Datos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Diagnostics;

namespace LAB02_ED1_G.Controllers
{
    public class ClienteController : Controller
    {
        private IWebHostEnvironment Environment;
        public ClienteController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            if (Singleton.Instance.flag == 1)
            {
                Singleton.Instance.flag = 0;
                return View(Singleton.Instance.Aux);
            }
            else
            {
               return View(Singleton.Instance.ArbolVehiculos.ObtenerLista());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var NewAuto = new Models.ExtensionVehiculo
                {
                    ID = collection["ID"],
                    Email = collection["email"],
                    Propietario = collection["Propietario"],
                    Color = collection["color"],
                    Marca = collection["Marca"],
                    NumSerie = collection["NumSerie"],                    
                };
                Singleton.Instance.ArbolVehiculos.Add(NewAuto);
                Singleton.Instance.flag = 0;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(string id)
        {
            var viewautos = Singleton.Instance.ArbolVehiculos.ObtenerLista().FirstOrDefault(a => a.ID == id);
            return View(viewautos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var viewautos = Singleton.Instance.ArbolVehiculos.ObtenerLista().FirstOrDefault(a => a.ID == id);
                string auxids = Singleton.Instance.ArbolVehiculos.ObtenerLista().FirstOrDefault(a => a.ID == id).ID;
                Singleton.Instance.ArbolVehiculos.Remove(viewautos);
                var nuevoauto = new Models.ExtensionVehiculo
                {
                    ID = auxids,
                    Email = collection["Email"],
                    Propietario = collection["Propietario"],
                    Color = collection["Color"],
                    Marca = collection["Marca"],
                    NumSerie = collection["NumSerie"]
                };
                Singleton.Instance.flag = 0;
                Singleton.Instance.ArbolVehiculos.Add(nuevoauto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }
        public ActionResult Delete(string id)
        {
            Singleton.Instance.flag = 0;
            var Viewautos = Singleton.Instance.ArbolVehiculos.ObtenerLista().FirstOrDefault(a => a.ID == id);
            return View(Viewautos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                Singleton.Instance.flag = 0;
                var Viewautos = Singleton.Instance.ArbolVehiculos.ObtenerLista().FirstOrDefault(a => a.ID == id);
                Singleton.Instance.ArbolVehiculos.Remove(Viewautos);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BuscarDPI(string BuscDPI)
        {
            try
            {
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolVehiculos.Obtener(a => a.ID == BuscDPI);
                int a = Singleton.Instance.ArbolVehiculos.GetComparaciones();
                TempData["TComp"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult BuscarNumSerie(string BuscSerie)
        {
            try
            {
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolVehiculos.Obtener(a => a.NumSerie == BuscSerie);
                int a = Singleton.Instance.ArbolVehiculos.GetComparaciones();
                TempData["TComp2"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult BuscarCorreo(string BuscCorreo)
        {
            try
            {
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolVehiculos.Obtener(a => a.Email == BuscCorreo);
                int a = Singleton.Instance.ArbolVehiculos.GetComparaciones();
                int profundidad = Singleton.Instance.ArbolVehiculos.ObtenerProfundidad();
                TempData["TComp3"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                TempData["Deep"] = "La profundidad del arbol es: " + Convert.ToString(profundidad);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult CargarArchivo(IFormFile File)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string ID = "", Email = "", Propietario = "", Color = "", Marca = "", NumSerie = "";

            try
            {

                if (File != null)
                {
                    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string FileName = Path.GetFileName(File.FileName);
                    string FilePath = Path.Combine(path, FileName);
                    using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    using (TextFieldParser csvFile = new TextFieldParser(FilePath))
                    {

                        csvFile.CommentTokens = new string[] { "#" };
                        csvFile.SetDelimiters(new string[] { "," });
                        csvFile.HasFieldsEnclosedInQuotes = true;

                        csvFile.ReadLine();

                        while (!csvFile.EndOfData)
                        {
                            string[] fields = csvFile.ReadFields();
                            ID = Convert.ToString(fields[0]);
                            Email = Convert.ToString(fields[1]);
                            Propietario = Convert.ToString(fields[2]);
                            Color = Convert.ToString(fields[3]);
                            Marca = Convert.ToString(fields[4]);
                            NumSerie = Convert.ToString(fields[5]);
                            ExtensionVehiculo nuevoVehiculo = new ExtensionVehiculo
                            {
                                ID = ID,
                                Email = Email,
                                Propietario = Propietario,
                                Color = Color,
                                Marca = Marca,
                                NumSerie = NumSerie,

                            };
                            Singleton.Instance.ArbolVehiculos.Add(nuevoVehiculo);
                            //agregar metodo cuando este arbol binario
                        }
                    }
                }
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TCarga"] = "Tiempo de carga: " + Convert.ToString(time);
                int profundidad = Singleton.Instance.ArbolVehiculos.ObtenerProfundidad();
                TempData["Deep"] = "La profundidad del arbol es: " + Convert.ToString(profundidad);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult OrdenarDPI()
        {
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolVehiculos.ObtenerLista();
                Singleton.Instance.Aux.Sort(new PropietarioID());
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TSortDpiABB"] = "Tiempo de ordenamiento: " + Convert.ToString(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult OrdenarSerie()
        {
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.ArbolVehiculos.ObtenerLista();
                Singleton.Instance.Aux.Sort(new VehiculoID());
                /*var cmp = Singleton.Instance.ArbolVehiculos.ObtenerLista();
                cmp.Sort();*/
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TSortSNABB"] = "Tiempo de ordenamiento: " + Convert.ToString(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult OrdenarEmail()
        {
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Singleton.Instance.flag = 1;
                var cmp = Singleton.Instance.ArbolVehiculos.ObtenerLista();
                cmp.Sort(new PropietarioEmail());
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TSortEmailABB"] = "Tiempo de ordenamiento: " + Convert.ToString(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index));

            }
        }
    }
}
