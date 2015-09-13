using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Managers;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseDictionary.Controllers
{
    public class DictionaryController : Controller
    {
        private ICharacterManager _characterManager;
        private IPhraseManager _phraseManager;
        private IIdiomManager _idiomManager;

        public DictionaryController(ICharacterManager characterManager, IPhraseManager phraseManager, IIdiomManager idiomManager)
        {
            _characterManager = characterManager;
            _phraseManager = phraseManager;
            _idiomManager = idiomManager;
        }

        public async Task<IEnumerable<string>> GetRandom(int id = 20)
        {
            var random = new Random();
            var list = new List<string>();
            int numChars = await _characterManager.CountAsync();
            int chars = random.Next(numChars);
            foreach (var i in await _characterManager.GetCharacterRangeAsync(chars - id, chars))
            {
                list.Add
            }
        }
    }
}
