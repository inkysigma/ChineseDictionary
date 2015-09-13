﻿using System;
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
        private ICharacterManager _characterManager;
        private IPhraseManager _phraseManager;
        private IIdiomManager _idiomManager;

        public DictionaryController(ICharacterManager characterManager, IPhraseManager phraseManager, IIdiomManager idiomManager)
        {
            _characterManager = characterManager;
            _phraseManager = phraseManager;
            _idiomManager = idiomManager;
        }

        public async Task<IEnumerable<Description>> GetRandom(int id = 20)
        {
            var random = new Random();
            int numChars = await _characterManager.CountAsync();
            int chars = random.Next(numChars);
            var list = (from i in await _characterManager.GetCharacterRangeAsync(chars - random.Next(0, id), chars)
                        select Description.Create((Character) i)).ToList();
            int numIdioms = await _idiomManager.CountAsync();
            int idioms = random.Next(numIdioms);
            foreach (var i in _idiomManager.Get)
            return list;
        }
    }
}
