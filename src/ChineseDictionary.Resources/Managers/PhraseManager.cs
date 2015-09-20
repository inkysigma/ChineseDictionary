using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public class PhraseManager : IPhraseManager
    {
        public ICharacterManager Manager { get; set; }
        public DictionaryContext Context { get; set; }

        public PhraseManager(DictionaryContext context, ICharacterManager manager)
        {
            Context = context;
            Manager = manager;
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

        public async Task<IEnumerable<Phrase>> FindPhrasesByCharacterAsync(string character)
        {
            if (string.IsNullOrEmpty(character))
                return new Phrase[0];
            return await Context.Phrases.Where(c => c.Characters.Any(x => x.Logograph == character)).ToArrayAsync();
        }

        public async Task<IEnumerable<Phrase>> FindPhrasesByDefinitionAsync(string phrase, string definition)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(definition))
                return null;
            return await Context.Phrases.Where(c => c.Definitions.Any(x => x.Definition == definition)).ToArrayAsync();
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
            var firstOrDefault = await FindPhraseAsync(phrase);
            if (firstOrDefault == null)
                return false;
            firstOrDefault.Pronunciation = pronunciation;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string phrase, DefinitionEntry definition)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(definition.Definition) || string.IsNullOrEmpty(definition.PartOfSpeech))
                return false;
            var c = await FindPhraseAsync(phrase);
            if (c == null)
                return false;
            c.Definitions.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string phrase, Usage usage)
        {
            if (string.IsNullOrEmpty(phrase) || string.IsNullOrEmpty(usage.Sentence))
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
            c.Definitions.Remove(c.Definitions.FirstOrDefault(x => x.Definition == definition));
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
            c.Usages.Remove(c.Usages.FirstOrDefault(x => x.Sentence == usage));
            await Save();
            return true;
        }

        public async Task<bool> RemovePhraseAsync(string phrase)
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

        public async Task<Phrase> GetPhrase(int number)
        {
            if (number < 0)
                return null;
            return await Context.Phrases.Where(c => c.Number == number).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Phrase>> GetPhrasesAsync()
        {
            return await Context.Phrases.Where(c => true).ToArrayAsync();
        }

        public async Task<IEnumerable<Phrase>> GetLatestPhrasesAsync(int number)
        {
            if (number < 0)
                return new Phrase[0];
            var total = await CountAsync();
            if (number > total)
                number = total;
            var result = Context.Phrases.OrderByDescending(c => c.Number)
                .Include(c => c.Definitions)
                .Include(c => c.Usages)
                .Take(number);
             if (!await result.AnyAsync())
                return new Phrase[0];
             return await result.ToArrayAsync();
        }

        public async Task<IEnumerable<Phrase>> GetPhraseRangeAsync(int beginning, int range)
        {
            if (beginning < 0 || range < 0 || beginning > range)
                return new Phrase[0];
            return await Context.Phrases.OrderBy(c => c.Number).Skip(beginning).Take(range).ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Phrases.CountAsync();
        }
    }
}
