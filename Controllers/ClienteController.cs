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
                return View();
               // return View(Singleton.Instance.ArbolVehiculos.ObtenerLista());
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
                //Singleton.Instance.ArbolVehiculos.Add(NewAuto);
                //para el arbol binario hay que agragar un metodo add 
                Singleton.Instance.flag = 0;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
                            //Singleton.Instance.ArbolVehiculos.Add(nuevoVehiculo);
                            //agregar metodo cuando este arbol binario
                        }
                    }
                }
                timer.Stop();
                TimeSpan time = timer.Elapsed;
                TempData["TCarga"] = "Tiempo de carga: " + Convert.ToString(time);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["Message"] = "Algo sucedio mal";
                return RedirectToAction(nameof(Index));

            }


            //Imprimir "time" en pantalla


        }
    }
}
