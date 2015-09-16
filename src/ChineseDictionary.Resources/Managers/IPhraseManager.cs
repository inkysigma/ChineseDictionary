using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface IPhraseManager
    {
        Task<Phrase> FindPhraseAsync(string phrase);
        Task<IEnumerable<Phrase>> FindPhrasesByCharacterAsync(string character);
        Task<IEnumerable<Phrase>> FindPhrasesByDefinitionAsync(string phrase, string definition);
        Task<bool> AddPhraseAsync(Phrase phrase);
        Task<bool> UpdatePronunciationAsync(string phrase, string pronunciation);
        Task<bool> UpdateDefinitionAsync(string phrase, DefinitionEntry definition);
        Task<bool> UpdateUsageAsync(string phrase, Usage usage);
        Task<bool> RemoveDefinitionAsync(string phrase, string definition);
        Task<bool> RemoveUsageAsync(string phrase, string usage);
        Task<bool> RemovePhraseAsync(string phrase);
        Task<Phrase> GetPhrase(int number);
        Task<IEnumerable<Phrase>> GetPhrasesAsync();
        Task<IEnumerable<Phrase>> GetLatestPhrasesAsync(int number);
        Task<IEnumerable<Phrase>> GetPhraseRangeAsync(int beginning, int range);
        Task<int> CountAsync();
    }
}