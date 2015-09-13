using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public class CharacterManager : ICharacterManager
    {
        public DictionaryContext Context { get; set; }

        public CharacterManager(DictionaryContext context)
        {
            Context = context;
        }

        private async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<bool> AddCharacterAsync(Character character)
        {
            if (!character.Validate() && !await Context.Characters.ContainsAsync(character))
                return false;
            Context.Characters.Add(character);
            await Save();
            return true;
        }

        public async Task<Character> FindCharacterAsync(string character)
        {
            if (string.IsNullOrEmpty(character))
                return null;
            return await Context.Characters.Where(c => c.Logograph == character).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Character>> FindCharactersByDefinitionAsync(string character, string definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition))
                return null;
            return await Context.Characters.Where(c => c.Definitions.Any(x => x.Value == definition)).ToArrayAsync();
        }

        public async Task<bool> UpdatePronunciationAsync(string character, string pronouncition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(pronouncition))
                return false;
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            c.Pronunciation = pronouncition;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string character, KeyValuePair<string, string> definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition.Value) || string.IsNullOrEmpty(definition.Key))
                return false;
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            c.Definitions.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string character, string usage)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindCharacterAsync(character);
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
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            c.Definitions.Remove(c.Definitions.FirstOrDefault(x => x.Value == definition));
            await Save();
            return true;
        }

        public async Task<bool> RemoveUsageAsync(string character, string usage)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindCharacterAsync(character);
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
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            Context.Characters.Remove(c);
            await Save();
            return true;
        }


        public async Task<Character> GetCharacter(int number)
        {
            if (number < 0)
                return null;
            return await Context.Characters.Where(c => c.Number == number).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            return await Context.Characters.Where(c => true).ToArrayAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterRangeAsync(int beginning, int range)
        {
            if (beginning < 0 || range < 0 || beginning > range)
                return new Character[0];
            return await Context.Characters.OrderBy(c => c.Number).Skip(beginning).Take(range).ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Characters.CountAsync();
        }
    }
}
