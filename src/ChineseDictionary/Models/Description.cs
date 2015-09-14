using System.Collections.Generic;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Models
{
    public class Description
    {
        public string Type { get; set; }
        public string Word { get; set; }
        public ICollection<DefinitionEntry> Definitions { get; set; }
        public IEnumerable<string> Usages { get; set; } 

        public Description(string word, ICollection<DefinitionEntry> definitions, IEnumerable<string> usages)
        {
            Word = word;
            Definitions = definitions;
            Usages = usages;
        }

        public static Description Create(Character character)
        {
            var description = new Description(character.Logograph,character.Definitions,
                character.Usages)
            {
                Type = "Character"
            };
            return description;
        }

        public static Description Create(Phrase phrase)
        {
            return new Description(phrase.Word, phrase.Definitions, phrase.Usages)
            {
                Type = "Phrase"
            };
        }

        public static Description Create(Idiom idiom)
        {
            return new Description(idiom.Word, idiom.Definitions, idiom.Usages)
            {
                Type = "Idiom"
            };
        }
    }
}
