using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChineseDictionary.Resources.Models
{
    public class Phrase
    {
        [Key]
        public int Number { get; set; }

        public string Pronounciation { get; set; }

        public string Word { get; set; }

        public virtual IList<Character> Characters { get; set; } 

        public IList<string> Definition { get; set; }
        
        public IList<string> Usages { get; set; }

        public bool Validate()
        {
            return string.IsNullOrEmpty(Pronounciation) && string.IsNullOrEmpty(Word) && Definition.Any();
        }
    }
}
