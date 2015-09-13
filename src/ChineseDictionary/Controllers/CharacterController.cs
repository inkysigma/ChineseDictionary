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
        public async Task<string> AddCharacter(string data)
        {
            if (string.IsNullOrEmpty(data))
                return "Nothing is filled in";
            var obj = JsonConvert.DeserializeObject<Character>(data);
            if (!obj.Validate())
                return "The data is not valid";
            if (!obj.Usages.Any())
                return "There aren't any examples";
            await _characterManager.AddCharacterAsync(obj);
            return "Success";
        }
    }
}
