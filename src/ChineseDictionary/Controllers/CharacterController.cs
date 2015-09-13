using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace ChineseDictionary.Controllers
{
    public class CharacterController : Controller
    {
        // GET: api/values
        [HttpPost]
        public IEnumerable<string> GetRandom()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
