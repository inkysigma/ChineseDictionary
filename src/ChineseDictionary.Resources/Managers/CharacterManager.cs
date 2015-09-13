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
            return await Context.Characters.Where(c => c.Definition.Contains(definition)).ToArrayAsync();
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

        public async Task<bool> UpdatePartOfSpeechAsync(string character, string partOfSpeech)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(partOfSpeech))
                return false;
            var firstOrDefault = await FindCharacterAsync(character);
            if (firstOrDefault == null)
                return false;
            firstOrDefault.PartOfSpeech = partOfSpeech;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string character, string definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindCharacterAsync(character);
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
            c.Definition.Remove(definition);
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
