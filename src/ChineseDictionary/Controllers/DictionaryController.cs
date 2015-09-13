using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Models;
using ChineseDictionary.Resources.Managers;
using ChineseDictionary.Resources.Models;
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

        public async Task<IEnumerable<Description>> GetRandomRange(int id = 20)
        {
            var random = new Random();
            int numChars = await _characterManager.CountAsync();
            int chars = random.Next(numChars);
            var list = new List<Description>();
            foreach (var i in await _characterManager.GetCharacterRangeAsync(chars - random.Next(0, id), chars))
            {
                list.Add(Description.Create(i));
            }
            int numIdioms = await _idiomManager.CountAsync();
            int idioms = random.Next(numIdioms);
            foreach (var i in await _idiomManager.GetIdiomRangeAsync(idioms - random.Next(0, id) + list.Count, idioms))
            {
                list.Add(Description.Create(i));
            }

            int numPhrases = await _phraseManager.CountAsync();
            int phrases = random.Next(numPhrases);

            foreach (var i in await _phraseManager.GetPhraseRangeAsync(phrases - random.Next(0, id) + list.Count, phrases))
            {
                list.Add(Description.Create(i));
            }

            return list;
        }
    }
}
