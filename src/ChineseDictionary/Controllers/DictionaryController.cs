using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Models;
using ChineseDictionary.Resources.Managers;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChineseDictionary.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly ICharacterManager _characterManager;
        private readonly IPhraseManager _phraseManager;
        private readonly IIdiomManager _idiomManager;

        public DictionaryController(ICharacterManager characterManager, IPhraseManager phraseManager, IIdiomManager idiomManager)
        {
            _characterManager = characterManager;
            _phraseManager = phraseManager;
            _idiomManager = idiomManager;
        }

        public async Task<IEnumerable<Description>> GetLatestRange(int id = 20)
        {
            var random = new Random();
            var list = (from i in await _characterManager.GetLatestCharactersAsync(random.Next(0, id)) select Description.Create(i)).ToList();
            list.AddRange((from i in await _idiomManager.GetLatestIdiomsAsync(random.Next(0, id) - list.Count) select Description.Create(i)));
            list.AddRange((from i in await _phraseManager.GetLatestPhrasesAsync(random.Next(0, id) - list.Count) select Description.Create(i)));
            return list;
        }

        public async Task<Description> GetRandom()
        {
            var random = new Random();
            var type = random.Next(0, 3);
            if (type == 1)
                return Description.Create(await _idiomManager.GetIdiom(random.Next(0, await _idiomManager.CountAsync())));
            if (type == 2)
                return
                    Description.Create(await _phraseManager.GetPhrase(random.Next(0, await _phraseManager.CountAsync())));
            return Description.Create(await _characterManager.GetCharacterAsync(random.Next(0, await _characterManager.CountAsync())));
        }

        public async Task<IEnumerable<Description>> SearchMultiple(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            if (id.Length == 1)
                return new Description[1] {Description.Create(await _characterManager.FindCharacterAsync(id))};
            var descriptions = (from i in await _idiomManager.FindIdiomsByCharacterAsync(id) select Description.Create(i)).ToList();
            descriptions.AddRange(from i in await _phraseManager.FindPhrasesByCharacterAsync(id) select Description.Create(i));
            return descriptions;
        }
    }
}
