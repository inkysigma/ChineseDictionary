using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface ICharacterManager
    {
        Task<bool> AddCharacterAsync(Character character);
        Task<Character> FindCharacterAsync(string character);
        Task<IEnumerable<Character>> FindCharactersByDefinitionAsync(string character, string definition);
        Task<bool> UpdatePronunciationAsync(string character, string pronouncition);
        Task<bool> UpdateDefinitionAsync(string character, DefinitionEntry definition);
        Task<bool> UpdateUsageAsync(string character, Usage usage);
        Task<bool> UpdateCharacterAsync(Character character);
        Task<bool> RemoveDefinitionAsync(string character, string definition);
        Task<bool> RemoveUsageAsync(string character, string usage);
        Task<bool> RemoveCharacterAsync(string character);
        Task<Character> GetCharacterAsync(int number);
        Task<Character> GetCharacterByListAsync(int number);
        Task<IEnumerable<Character>> GetCharactersAsync();
        Task<IEnumerable<Character>> GetLatestCharactersAsync(int number);
        Task<IEnumerable<Character>> GetCharacterRangeAsync(int beginning, int range);
        Task<int> CountAsync();
    }
}