﻿using System;
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
        public async Task<QueryResult> AddCharacter(Character obj)
        {
            if (obj == null)
                return QueryResult.EmptyField("Nothing was filled out");
            if (!obj.Validate())
                return QueryResult.InvalidField(nameof(obj));
            if (obj.Phrases == null)
                obj.Phrases = new List<Phrase>();
            if (obj.Idioms == null)
                obj.Idioms = new List<Idiom>();
            if (!obj.Usages.Any())
                return QueryResult.InvalidField(nameof(obj));
            if (!await _characterManager.AddCharacterAsync(obj))
                return QueryResult.QueryFailed("The character already exists");
            return QueryResult.Succeded;
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
            if (total < 1)
                return null;
            return await _characterManager.GetCharacter(rng.Next(0, total));
        }
    }
}
