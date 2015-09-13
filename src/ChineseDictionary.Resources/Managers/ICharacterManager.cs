using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface ICharacterManager
    {
        DictionaryContext Context { get; set; }
        Task<bool> AddCharacterAsync(Character character);
        Task<Character> FindCharacterAsync(string character);
        Task<IEnumerable<Character>> FindCharactersByDefinitionAsync(string character, string definition);
        Task<bool> UpdatePronunciationAsync(string character, string pronouncition);
        Task<bool> UpdatePartOfSpeechAsync(string character, string partOfSpeech);
        Task<bool> UpdateDefinitionAsync(string character, string definition);
        Task<bool> UpdateUsageAsync(string character, string usage);
        Task<bool> RemoveDefinitionAsync(string character, string definition);
        Task<bool> RemoveUsageAsync(string character, string usage);
        Task<bool> RemoveCharacterAsync(string character);
        Task<IEnumerable<Character>> GetCharactersAsync();
        Task<IEnumerable<Character>> GetCharacterRangeAsync(int beginning, int range);
        Task<int> CountAsync();
    }
}