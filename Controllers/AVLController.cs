using LAB02_ED1_G.Models;
using LAB02_ED1_G.Models.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Globalization;

namespace LAB02_ED1_G.Controllers
{
    public class AVLController : Controller
    {
        private IWebHostEnvironment Environment;

        public AVLController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index2()
        {
            if (Singleton.Instance.flag == 1)
            {
                Singleton.Instance.flag = 0;
                return View(Singleton.Instance.Aux);
            }
            else
            {
                return View(Singleton.Instance.AVL.ObtenerLista());
            }
        }

        public ActionResult Create2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create2(IFormCollection collection)
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
                    NumSerie = collection["NumSerie"]
                };
                Singleton.Instance.AVL.Add(NewAuto);
                Singleton.Instance.flag = 0;
                return RedirectToAction(nameof(Index2));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit2(string id)
        {
            var viewautos = Singleton.Instance.AVL.ObtenerLista().FirstOrDefault(a => a.ID == id);
            return View(viewautos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(string id, IFormCollection collection)
        {
            try
            {
                var viewautos = Singleton.Instance.AVL.ObtenerLista().FirstOrDefault(a =>a.ID == id);
                string auxids = Singleton.Instance.AVL.ObtenerLista().FirstOrDefault(a => a.ID == id).ID;
                Singleton.Instance.AVL.Remove(viewautos);
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
                Singleton.Instance.AVL.Add(nuevoauto);
                return RedirectToAction(nameof(Index2));
            }
            catch
            {

                return View();
            }
        }
        public ActionResult Delete2(string id)
        {
            Singleton.Instance.flag = 0;
            var Viewautos = Singleton.Instance.AVL.ObtenerLista().FirstOrDefault(a => a.ID == id);
            return View(Viewautos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete2(string id, IFormCollection collection)
        {
            try
            {
                Singleton.Instance.flag = 0;
                var Viewautos = Singleton.Instance.AVL.ObtenerLista().FirstOrDefault(a => a.ID == id);
                Singleton.Instance.AVL.Remove(Viewautos);
                return RedirectToAction(nameof(Index2));
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
                Singleton.Instance.Aux = Singleton.Instance.AVL.Obtener(a => a.ID == BuscDPI);
                int a = Singleton.Instance.AVL.GetComparaciones();
                TempData["TComp"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                return RedirectToAction(nameof(Index2));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index2));

            }
        }
        public ActionResult BuscarNumSerie(string BuscSerie)
        {
            try
            {
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.AVL.Obtener(a => a.NumSerie == BuscSerie);
                int a = Singleton.Instance.AVL.GetComparaciones();
                TempData["TComp2"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                return RedirectToAction(nameof(Index2));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index2));

            }
        }
        public ActionResult BuscarCorreo(string BuscCorreo)
        {
            try
            {
                Singleton.Instance.flag = 1;
                Singleton.Instance.Aux = Singleton.Instance.AVL.Obtener(a => a.Email == BuscCorreo);
                int a = Singleton.Instance.AVL.GetComparaciones();
                TempData["TComp3"] = "Se realizaron: " + Convert.ToString(a) + " comparaciones.";
                return RedirectToAction(nameof(Index2));
            }
            catch (Exception)
            {
                Singleton.Instance.flag = 0;
                ViewData["Message"] = "No Encontrado";
                return RedirectToAction(nameof(Index2));

            }
        }


        public ActionResult CargarArchivo2(IFormFile File)
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
                            Singleton.Instance.AVL.Add(nuevoVehiculo);
                        }
                    }
                }
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TCargaAVL"] = "Tiempo de carga: " + Convert.ToString(time);
                int b = Singleton.Instance.AVL.GetRotacion();
                TempData["TRot"] = "Existieron: " + Convert.ToString(b) + " rotaciones.";
                return RedirectToAction(nameof(Index2));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index2));

            }
        }
    }
}
