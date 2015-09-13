using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public class PhraseManager : IPhraseManager
    {
        public CharacterManager Manager { get; set; }
        public DictionaryContext Context { get; set; }

        public PhraseManager(CharacterManager manager)
        {
            Manager = manager;
            Context = manager.Context;
        }

        private async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<Phrase> FindPhraseAsync(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                return null;
            return await Context.Phrases.Where(c => c.Word.StartsWith(phrase)).FirstOrDefaultAsync();
        }

        public async Task<bool> AddPhraseAsync(Phrase phrase)
        {
            if (!phrase.Validate() && !await Context.Phrases.ContainsAsync(phrase))
                return false;
            Context.Phrases.Add(phrase);
            foreach (char i in phrase.Word)
            {
                Character c = await Manager.FindCharacterAsync(i.ToString());
                phrase.Characters.Add(c);
                c.Phrases.Add(phrase);
            }
            await Save();
            return true;
        }

        public async Task<bool> UpdatePronunciationAsync(string phrase, string pronunciation)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(pronunciation))
                return false;
            var firstOrDefault =
                await Context.Phrases.Where(c => c.Pronunciation == pronunciation).FirstOrDefaultAsync();
            if (firstOrDefault == null)
                return false;
            firstOrDefault.Pronunciation = pronunciation;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string phrase, string definition)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            c.Definition.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string phrase, string usage)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            c.Usages.Add(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveDefinitionAsync(string phrase, string definition)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            c.Definition.Remove(definition);
            await Save();
            return true;
        }

        public async Task<bool> RemoveUsageAsync(string phrase, string usage)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            c.Usages.Remove(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveIdiomAsync(string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            Context.Phrases.Remove(c);
            await Save();
            return true;
        }

        public async Task<IEnumerable<Phrase>> GetCharactersAsync()
        {
            return await Context.Phrases.Where(c => true).ToArrayAsync();
        }

        public async Task<IEnumerable<Phrase>> GetCharacterRangeAsync(int beginning, int range)
        {
            return await Context.Phrases.OrderBy(c => c.Number).Skip(beginning).Take(range).ToArrayAsync();
        }
    }
}
