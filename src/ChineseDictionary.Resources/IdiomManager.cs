using ChineseDictionary.Resources.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ChineseDictionary.Resources
{
    public class IdiomManager
    {
        public CharacterManager Manager { get; set; }
        public DictionaryContext Context { get; set; }

        public IdiomManager(CharacterManager manager)
        {
            Manager = manager;
            Context = manager.Context;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<bool> AddIdiomAsync(Idiom idiom)
        {
            if (!idiom.Validate() && !await Context.Idioms.ContainsAsync(idiom))
                return false;
            Context.Idioms.Add(idiom);
            foreach (char i in idiom.Word)
            {
                Character c = await Manager.FindCharacterAsync(i.ToString());
                idiom.Characters.Add(c);
            }
            await Save();
            return true;
        }

        public async Task<Idiom> FindIdiomAsync(string idiom)
        {
            if (string.IsNullOrEmpty(idiom))
                return null;
            return await Context.Idioms.Where(c => c.Word.StartsWith(idiom)).FirstOrDefaultAsync();
        }
    }
}
