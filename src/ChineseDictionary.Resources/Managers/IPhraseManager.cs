using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface IPhraseManager
    {
        Task<Phrase> FindPhraseAsync(string phrase);
        Task<bool> AddPhraseAsync(Phrase phrase);
        Task<bool> UpdatePronunciationAsync(string phrase, string pronunciation);
        Task<bool> UpdateDefinitionAsync(string phrase, string definition);
        Task<bool> UpdateUsageAsync(string phrase, string usage);
        Task<bool> RemoveDefinitionAsync(string phrase, string definition);
        Task<bool> RemoveUsageAsync(string phrase, string usage);
        Task<bool> RemoveIdiomAsync(string phrase);
        Task<IEnumerable<Phrase>> GetCharactersAsync();
        Task<IEnumerable<Phrase>> GetCharacterRangeAsync(int beginning, int range);
    }
}