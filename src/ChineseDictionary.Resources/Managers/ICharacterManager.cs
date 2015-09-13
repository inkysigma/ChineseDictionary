using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface ICharacterManager
    {
        Task<bool> AddCharacterAsync(Character character);
        Task<Character> FindCharacterAsync(string character);
        Task<bool> UpdatePronounciationAsync(string character, string pronouncition);
        Task<bool> UpdateDefinitionAsync(string character, string definition);
        Task<bool> UpdateUsageAsync(string character, string usage);
        Task<bool> RemoveDefinitionAsync(string character, string definition);
        Task<bool> RemoveUsageAsync(string character, string usage);
        Task<bool> RemoveCharacterAsync(string character);
        IEnumerable<Character> GetCharactersAsync();
        IEnumerable<Character> GetCharacterRangeAsync(int beginning, int range);
    }
}