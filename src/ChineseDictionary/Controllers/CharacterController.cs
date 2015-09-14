using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Managers;
using ChineseDictionary.Resources.Models;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChineseDictionary.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ICharacterManager _characterManager;
        public CharacterController(ICharacterManager characterManager)
        {
            _characterManager = characterManager;
        }
        
        [HttpPost]
        public async Task<string> AddCharacter(Character obj)
        {
            if (!obj.Validate())
                return "The data is not valid";
            if (obj.Phrases == null)
                obj.Phrases = new List<Phrase>();
            if (obj.Idioms == null)
                obj.Idioms = new List<Idiom>();
            if (!obj.Usages.Any())
                return "There aren't any examples";
            if (!await _characterManager.AddCharacterAsync(obj))
                return "Character already exists";
            return "Success";
        }
    }
}
