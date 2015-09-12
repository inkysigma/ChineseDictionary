using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;
using System.Data.Entity;

namespace ChineseDictionary.Resources
{
    public class PhraseManager
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
    }
}
