using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Managers;
using ChineseDictionary.Resources.Models;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

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
        public async Task<bool> AddCharacter(string data)
        {
            var obj = JsonConvert.DeserializeObject<Character>(data);
            if (!obj.Validate())
                return false;
            if (!obj.Usages.Any())
                return false;
            await _characterManager.AddCharacterAsync(obj);
            return true;
        }
    }
}
