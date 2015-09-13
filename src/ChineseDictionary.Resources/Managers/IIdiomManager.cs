using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    /// <summary>
    /// Notes: The KeyValuePair represents definition:pos
    /// </summary>
    public interface IIdiomManager
    {
        Task<bool> AddIdiomAsync(Idiom idiom);
        Task<Idiom> FindIdiomAsync(string idiom);
        Task<IEnumerable<Idiom>> FindIdiomsByCharacterAsync(string character);
        Task<IEnumerable<Idiom>> FindIdiomsByDefinitionAsync(string definition);
        Task<bool> UpdatePronunciationAsync(string idiom, string pronouncition);
        Task<bool> UpdateStoryAsync(string idiom, string story);
        Task<bool> UpdateDefinitionAsync(string idiom, KeyValuePair<string, string> definition);
        Task<bool> UpdateUsageAsync(string idiom, string usage);
        Task<bool> RemoveDefinitionAsync(string idiom, string definition);
        Task<bool> RemoveUsageAsync(string idiom, string usage);
        Task<bool> RemoveIdiomAsync(string idiom);
        Task<Idiom> GetIdiom(int number);
        Task<IEnumerable<Idiom>> GetIdiomsAsync();
        Task<IEnumerable<Idiom>> GetIdiomRangeAsync(int beginning, int range);
        Task<int> CountAsync();
    }
}