using System.Collections.Generic;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public interface IIdiomManager
    {
        Task Save();
        Task<bool> AddIdiomAsync(Idiom idiom);
        Task<Idiom> FindIdiomAsync(string idiom);
        Task<IEnumerable<Idiom>> FindIdiomsByDefinitionAsync(string definition);
        Task<bool> UpdatePronounciationAsync(string idiom, string pronouncition);
        Task<bool> UpdateDefinitionAsync(string idiom, string definition);
        Task<bool> UpdateUsageAsync(string idiom, string usage);
        Task<bool> RemoveDefinitionAsync(string idiom, string definition);
        Task<bool> RemoveUsageAsync(string idiom, string usage);
        Task<bool> RemoveIdiomAsync(string idiom);
        IEnumerable<Idiom> GetCharactersAsync();
        IEnumerable<Idiom> GetCharacterRangeAsync(int beginning, int range);
    }
}