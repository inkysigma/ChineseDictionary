using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Managers;
using ChineseDictionary.Resources.Models;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseDictionary.Controllers
{
    public class PhraseController : Controller
    {
        internal IPhraseManager Manager { get; set; }
        public PhraseController(IPhraseManager manager)
        {
            Manager = manager;
        }

        [HttpPost]
        public async Task<string> AddPhrase(Phrase phrase)
        {
            if (phrase == null)
                return "Nothing was filled out";
            if (!phrase.Validate())
                return "Data is invalid";
            if (phrase.Usages == null)
                return "No usages were submitted";
            if (phrase.Definitions == null)
                return "No definitions were submitted";
            if (await Manager.AddPhraseAsync(phrase))
                return "Success";
            return "Phrase already exists";
        }

        [HttpPost]
        public async Task<Phrase> GetRandomPhrase()
        {
            var rng = new Random();
            var total = await Manager.CountAsync();
            if (total < 1)
                return null;
            return await Manager.GetPhrase(rng.Next(0, total));
        }

        [HttpPost]
        public async Task<Phrase> GetPhrase(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            return await Manager.FindPhraseAsync(id);
        }

        [HttpPost]
        public async Task<IEnumerable<Phrase>> GetPhrasesByCharacter(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 1)
                return null;
            return await Manager.FindPhrasesByCharacterAsync(id);
        }
    }
}
