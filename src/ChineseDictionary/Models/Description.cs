using System.Collections.Generic;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Models
{
    public class Description
    {
        public string Type { get; set; }
        public string Word { get; set; }
        public string PartOfSpeech { get; set; }
        public IEnumerable<string> Definitions { get; set; }
        public IEnumerable<string> Usages { get; set; } 

        public Description(string word, string partOfSpeech, IEnumerable<string> definitions, IEnumerable<string> usages)
        {
            Word = word;
            PartOfSpeech = partOfSpeech;
            Definitions = definitions;
            Usages = usages;
        }

        public static Description Create(Character character)
        {
            var description = new Description(character.Logograph, character.PartOfSpeech, character.Definition,
                character.Usages)
            {
                Type = "Character"
            };
            return description;
        }

        public static Description Create(Phrase phrase)
        {
            return new Description(phrase.Word, phrase.PartOfSpeech, phrase.Definition, phrase.Usages)
            {
                Type = "Phrase"
            };
        }

        public static Description Create(Idiom idiom)
        {
            return new Description(idiom.Word, idiom.PartOfSpeech, idiom.Definition, idiom.Usages)
            {
                Type = "Idiom"
            };
        }
    }
}
