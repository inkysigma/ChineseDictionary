using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Models;
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
        public async Task<QueryResult> AddPhrase(Phrase phrase)
        {
            if (phrase == null)
                return QueryResult.EmptyField(nameof(phrase));
            if (!phrase.Validate())
                return QueryResult.InvalidField(nameof(phrase));
            if (phrase.Usages == null)
                return QueryResult.InvalidField(nameof(phrase));
            if (phrase.Definitions == null)
                return QueryResult.InvalidField(nameof(phrase));
            if (await Manager.AddPhraseAsync(phrase))
                return QueryResult.Succeded;
            return QueryResult.QueryFailed("The field already exists.");
        }

        [HttpPost]
        public async Task<QueryResult> AddPhraseDefinition(string phrase, DefinitionEntry entry)
        {
            if (string.IsNullOrEmpty(entry.Definition))
                return QueryResult.EmptyField(nameof(entry));
            if (string.IsNullOrEmpty(entry.PartOfSpeech))
                return QueryResult.EmptyField(nameof(entry));
            if (string.IsNullOrEmpty(phrase))
                return QueryResult.EmptyField(nameof(phrase));
            return QueryResult.QueryFailed(await Manager.UpdateDefinitionAsync(phrase, entry));
        }

        [HttpPost]
        public async Task<QueryResult> AddPhraseUsage(string phrase, Usage usage)
        {
            if (string.IsNullOrEmpty(phrase))
                return QueryResult.EmptyField(nameof(phrase));
            if (usage == null)
                return QueryResult.EmptyField(nameof(usage));
            if (string.IsNullOrEmpty(usage.Sentence))
                return QueryResult.InvalidField(nameof(usage));
            return QueryResult.QueryFailed(await Manager.UpdateUsageAsync(phrase, usage));
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

        [HttpPost]
        public async Task<QueryResult> RemovePhrase(string id)
        {
            if (string.IsNullOrEmpty(id))
                return QueryResult.EmptyField(nameof(id));
            return QueryResult.QueryFailed(await Manager.RemovePhraseAsync(id));
        }
    }
}
