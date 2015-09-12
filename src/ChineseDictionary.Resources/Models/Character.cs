using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChineseDictionary.Resources.Models
{
    public class Character
    {
        [Key]
        public string Logograph { get; set; }

        public string Pronounciation { get; set; }

        public IList<string> Definition { get; set; }

        public IList<string> Usages { get; set; }
        
        public virtual IList<Phrase>  Phrases { get; set; }

        public virtual IList<Idiom> Idioms { get; set; }

        public bool Validate()
        {
            return string.IsNullOrEmpty(Pronounciation) && !string.IsNullOrEmpty(Logograph) && Definition.Any();
        }
    }
}
