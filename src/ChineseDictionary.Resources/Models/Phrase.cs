using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChineseDictionary.Resources.Models
{
    public class Phrase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string Pronunciation { get; set; }

        public string PartOfSpeech { get; set; }

        [Key]
        public string Word { get; set; }

        public virtual IList<Character> Characters { get; set; } 

        public IList<string> Definition { get; set; }
        
        public IList<string> Usages { get; set; }

        public bool Validate()
        {
            return string.IsNullOrEmpty(Pronunciation) && string.IsNullOrEmpty(Word) && Definition.Any();
        }
    }
}
