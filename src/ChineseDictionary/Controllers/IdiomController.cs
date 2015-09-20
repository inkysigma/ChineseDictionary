using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Models;
using ChineseDictionary.Resources.Managers;
using ChineseDictionary.Resources.Models;
using Microsoft.AspNet.Mvc;

namespace ChineseDictionary.Controllers {
    public class IdiomController : Controller
    {
        internal IIdiomManager Manager { get; set; }

        public IdiomController(IIdiomManager manager)
        {
            Manager = manager;
        }

        [HttpPost]
        public async Task<QueryResult> AddIdiom(Idiom idiom)
        {
            if (idiom == null)
                return QueryResult.EmptyField(nameof(idiom));
            if (!idiom.Validate())
                return QueryResult.InvalidField(nameof(idiom));
            if (!idiom.Usages.Any())
                return QueryResult.InvalidField(nameof(idiom));
            return QueryResult.QueryFailed(await Manager.AddIdiomAsync(idiom));
        }
    }
}