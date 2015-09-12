using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
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
            bool buildPronounciation = string.IsNullOrEmpty(idiom.Pronounciation);
            foreach (char i in idiom.Word)
            {
                Character c = await Manager.FindCharacterAsync(i.ToString());
                idiom.Characters.Add(c);
                c.Idioms.Add(idiom);
                if (buildPronounciation)
                    idiom.Pronounciation += c.Pronounciation;
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

        public async Task<bool> UpdatePronounciationAsync(string character, string pronouncition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(pronouncition))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            c.Pronounciation = pronouncition;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string character, string definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            c.Definition.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string character, string usage)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            c.Usages.Add(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveDefinitionAsync(string character, string definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            c.Definition.Remove(definition);
            await Save();
            return true;
        }

        public async Task<bool> RemoveUsageAsync(string character, string usage)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            c.Usages.Remove(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveCharacterAsync(string character)
        {
            if (string.IsNullOrEmpty(character))
                return false;
            var c = await FindIdiomAsync(character);
            if (c == null)
                return false;
            Context.Characters.Remove(c);
            await Save();
            return true;
        }

        public IEnumerable<Idiom> GetCharactersAsync()
        {
            return Context.Idioms.Where(c => true);
        }

        public IEnumerable<Idiom> GetCharacterRangeAsync(int beginning, int range)
        {
            return Context.Idioms.OrderBy(c => c.Number).Skip(beginning).Take(range);
        }
    }
}
