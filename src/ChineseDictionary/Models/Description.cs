using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Models
{
    public class Description
    {
        public string Word { get; set; }
        public string PartOfSpeech { get; set; }
        public IEnumerable<string> Definitions { get; set; }

        public Description(string word, string partOfSpeech, IEnumerable<string> definitions)
        {
            Word = word;
            PartOfSpeech = partOfSpeech;
            Definitions = definitions;
        }

        public static Description Create(Character character)
        {
            return new Description(character.Logograph, character.PartOfSpeech, character.Definition);
        }

        public static Description Create(Phrase phrase)
        {
            return new Description(phrase.Word, phrase.PartOfSpeech, phrase.Definition);
        }

        public static Description Create(Idiom idiom)
        {
            return new Description(idiom.Word, idiom.PartOfSpeech, idiom.Definition);
        }
    }
}
