using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChineseDictionary.Models;
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
        public async Task<QueryResult> AddCharacter(Character character)
        {
            if (character == null)
                return QueryResult.EmptyField(nameof(character));
            if (!character.Validate())
                return QueryResult.InvalidField(nameof(character));
            if (character.Phrases == null)
                character.Phrases = new List<Phrase>();
            if (character.Idioms == null)
                character.Idioms = new List<Idiom>();
            if (!character.Usages.Any())
                return QueryResult.InvalidField(nameof(character));
            if (!await _characterManager.AddCharacterAsync(character))
                return QueryResult.QueryFailed("The character already exists");
            return QueryResult.Succeded;
        }

        [HttpPost]
        public async Task<QueryResult> AddCharacterUsage(string character, Usage usage)
        {
            if (usage == null)
                return QueryResult.EmptyField(nameof(usage));
            if (string.IsNullOrEmpty(character))
                return QueryResult.EmptyField(nameof(character));
            if (usage.Sentence == null)
                return QueryResult.InvalidField(nameof(usage));
            return QueryResult.QueryFailed(await _characterManager.UpdateUsageAsync(character, usage));
        }

        [HttpPost]
        public async Task<QueryResult> AddCharacterDefinition(string character, DefinitionEntry entry)
        {
            if (string.IsNullOrEmpty(character))
                return QueryResult.EmptyField(nameof(character));
            if (entry == null)
                return QueryResult.EmptyField(nameof(entry));
            if (string.IsNullOrEmpty(entry.Definition) || string.IsNullOrEmpty(entry.PartOfSpeech))
                return QueryResult.InvalidField(nameof(entry));
            return QueryResult.QueryFailed(await _characterManager.UpdateDefinitionAsync(character, entry));
        }

        [HttpPost]
        public async Task<QueryResult> UpdateCharacter(string character, Character model)
        {
            if (string.IsNullOrEmpty(character))
                return QueryResult.EmptyField(nameof(character));
            if (model == null)
                return QueryResult.EmptyField(nameof(model));
            if (!model.Validate())
                return QueryResult.InvalidField(nameof(model));
            return QueryResult.QueryFailed(await _characterManager.UpdateCharacterAsync(model));
        }

        [HttpPost]
        public async Task<Character> GetCharacter(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            return await _characterManager.FindCharacterAsync(id);
        }


        [HttpPost]
        public async Task<QueryResult> RemoveCharacter(string character)
        {
            if (string.IsNullOrEmpty(character))
                return QueryResult.EmptyField(nameof(character));
            return QueryResult.QueryFailed(await _characterManager.RemoveCharacterAsync(character));
        }

        [HttpPost]
        public async Task<Character> GetRandom()
        {
            var rng = new Random();
            var total = await _characterManager.CountAsync();
            if (total <= 0)
                return null;
            return await _characterManager.GetCharacter(rng.Next(0, total));
        }
    }
}
