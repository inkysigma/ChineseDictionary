using System.IO;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseDictionary.Controllers
{
    public class DefaultController : Controller
    {
        private IApplicationEnvironment _env;
        public DefaultController(IApplicationEnvironment env)
        {
            _env = env;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return File(new FileStream(Path.Combine(_env.ApplicationBasePath, "wwwroot/index.html"), FileMode.Open), "text/html");
        }
    }
}
