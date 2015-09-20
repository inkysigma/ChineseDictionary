using System;
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
            if (!character.Validate() || Context.Characters.Any(c => c.Logograph == character.Logograph))
                return false;
            if (character.Priority <= 3)
                character.ReviewTime = DateTime.Now + TimeSpan.FromDays(4 - character.Priority);
            if (character.Priority > 3 && character.Priority >= 5)
                character.ReviewTime = DateTime.Now + TimeSpan.FromHours(6 - character.Priority);
            if (character.Priority > 5)
                character.ReviewTime = DateTime.Now + TimeSpan.FromMinutes(11 - character.Priority);
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
                return new Character[0];
            var result = 
                    Context.Characters.Where(c => c.Definitions.Any(x => x.Definition == definition))
                        .Include(c => c.Definitions)
                        .Include(c => c.Usages);
            if (!await result.AnyAsync())
                return new Character[0];
            return await result.ToArrayAsync();
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

        public async Task<bool> UpdateDefinitionAsync(string character, DefinitionEntry definition)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(definition.Definition) || string.IsNullOrEmpty(definition.PartOfSpeech))
                return false;
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            c.Definitions.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string character, Usage usage)
        {
            if (string.IsNullOrEmpty(character) || string.IsNullOrEmpty(usage.Sentence))
                return false;
            var c = await FindCharacterAsync(character);
            if (c == null)
                return false;
            c.Usages.Add(usage);
            await Save();
            return true;
        }

        public async Task<bool> UpdateCharacterAsync(Character character)
        {
            if (character == null || !character.Validate())
                return false;
            var original = await FindCharacterAsync(character.Logograph);
            if (original == null)
                return false;
            character.Number = original.Number;
            Context.Characters.Attach(character);
            Context.Entry(character).State = EntityState.Modified;
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
            c.Definitions.Remove(c.Definitions.FirstOrDefault(x => x.Definition == definition));
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
            c.Usages.Remove(c.Usages.FirstOrDefault(x => x.Sentence == usage));
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


        public async Task<Character> GetCharacterAsync(int number)
        {
            if (number < 0)
                return null;
            return await Context.Characters.Where(c => c.Number == number).FirstOrDefaultAsync();
        }

        public async Task<Character> GetCharacterByListAsync(int number)
        {
            if (number < 0)
                return null;
            return await Context.Characters.OrderBy(c => c.Number).Skip(number).Take(1).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            return await Context.Characters.Where(c => true).ToArrayAsync();
        }

        public async Task<IEnumerable<Character>> GetLatestCharactersAsync(int number)
        {
            if (number < 0)
                return new Character[0];
            int total = await CountAsync();
            if (number > total)
                number = total;
            return await Context.Characters.OrderByDescending(c => c.Number).Take(number).ToArrayAsync();
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
