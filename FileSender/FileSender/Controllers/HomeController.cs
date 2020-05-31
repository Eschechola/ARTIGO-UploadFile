using System;
using FileSender.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FileSender.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            ViewBag.UrlImage = "/images/default.png";
            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile Upload)
        {
            try
            {
                //verifica se algum arquivo foi enviado
                if (Upload is null || Upload?.Length == 0)
                    return Redirect("/");
                

                //pega o diretório de upload que está no arquivo de configuração
                var directory = _configuration["Directories:Uploads"];

                //salva o arquivo no diretório informado
                var filePath = new FileManager().SaveFile(Upload, directory);

                //atribui a URL da imagem para a viewBag removendo a "wwwroot"
                ViewBag.UrlImage = filePath.Remove(0, 7);

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
