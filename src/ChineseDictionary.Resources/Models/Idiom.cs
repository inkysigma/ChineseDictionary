using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ChineseDictionary.Resources.Models
{
    public class Idiom
    {
        [Key]
        public int Number { get; set; }

        public string Pronounciation { get; set; }

        public string Word { get; set; }

        public string Story { get; set; }

        public IList<string> Definition { get; set; }

        public IList<string> Usages { get; set; }

        public virtual IList<Character> Characters { get; set; }

        public bool Validate()
        {
            return string.IsNullOrEmpty(Pronounciation) && string.IsNullOrEmpty(Word) && Definition.Any();
        }
    }
}
