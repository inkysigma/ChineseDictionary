using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.Managers
{
    public class IdiomManager : IIdiomManager
    {
        public CharacterManager Manager { get; set; }
        public DictionaryContext Context { get; set; }

        public IdiomManager(CharacterManager manager)
        {
            Manager = manager;
            Context = manager.Context;
        }

        private async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<bool> AddIdiomAsync(Idiom idiom)
        {
            if (!idiom.Validate() && !await Context.Idioms.ContainsAsync(idiom))
                return false;
            Context.Idioms.Add(idiom);
            bool buildPronounciation = string.IsNullOrEmpty(idiom.Pronunciation);
            foreach (char i in idiom.Word)
            {
                Character c = await Manager.FindCharacterAsync(i.ToString());
                idiom.Characters.Add(c);
                c.Idioms.Add(idiom);
                if (buildPronounciation)
                    idiom.Pronunciation += c.Pronunciation;
            }
            await Save();
            return true;
        }

        public async Task<Idiom> FindIdiomAsync(string idiom)
        {
            if (string.IsNullOrEmpty(idiom))
                return null;
            return await Context.Idioms.Where(c => c.Word.StartsWith(idiom)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Idiom>> FindIdiomsByDefinitionAsync(string definition)
        {
            if (string.IsNullOrEmpty(definition))
                return null;
            return await Context.Idioms.Where(c => c.Definition.Contains(definition)).ToArrayAsync();
        }

        public async Task<bool> UpdatePronunciationAsync(string idiom, string pronouncition)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(pronouncition))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Pronunciation = pronouncition;
            await Save();
            return true;
        }

        public async Task<bool> UpdatePartOfSpeechAsync(string idiom, string partOfSpeech)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(partOfSpeech))
                return false;
            var firstOrDefault = await FindIdiomAsync(idiom);
            if (firstOrDefault == null)
                return false;
            firstOrDefault.PartOfSpeech = partOfSpeech;
            await Save();
            return true;
        }

        public async Task<bool> UpdateDefinitionAsync(string idiom, string definition)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Definition.Add(definition);
            await Save();
            return true;
        }

        public async Task<bool> UpdateStoryAsync(string idiom, string story)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(story))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Story = story;
            await Save();
            return true;
        }

        public async Task<bool> UpdateUsageAsync(string idiom, string usage)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Usages.Add(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveDefinitionAsync(string idiom, string definition)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(definition))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Definition.Remove(definition);
            await Save();
            return true;
        }

        public async Task<bool> RemoveUsageAsync(string idiom, string usage)
        {
            if (string.IsNullOrEmpty(idiom) || string.IsNullOrEmpty(usage))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            c.Usages.Remove(usage);
            await Save();
            return true;
        }

        public async Task<bool> RemoveIdiomAsync(string idiom)
        {
            if (string.IsNullOrEmpty(idiom))
                return false;
            var c = await FindIdiomAsync(idiom);
            if (c == null)
                return false;
            Context.Idioms.Remove(c);
            await Save();
            return true;
        }

        public async Task<IEnumerable<Idiom>> GetIdiomsAsync()
        {
            return await Context.Idioms.Where(c => true).ToArrayAsync();
        }

        public async Task<IEnumerable<Idiom>> GetIdiomRangeAsync(int beginning, int range)
        {
            return await Context.Idioms.OrderBy(c => c.Number).Skip(beginning).Take(range).ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Context.Idioms.CountAsync();
        }
    }
}
