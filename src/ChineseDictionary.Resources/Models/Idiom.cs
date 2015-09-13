using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseDictionary.Resources.Models
{
    public class Idiom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string Pronunciation { get; set; }

        [Key]
        public string Word { get; set; }

        public string Story { get; set; }

        public IList<string> Definition { get; set; }

        public IList<string> Usages { get; set; }

        public virtual IList<Character> Characters { get; set; }

        public bool Validate()
        {
            return string.IsNullOrEmpty(Pronunciation) && string.IsNullOrEmpty(Word) && Definition.Any();
        }
    }
}
