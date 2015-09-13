using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChineseDictionary.Resources.Models
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        [Key]
        public string Logograph { get; set; }

        public string PartOfSpeech { get; set; }

        public string Pronunciation { get; set; }

        public IList<string> Definitions { get; set; }

        public IList<string> Usages { get; set; }
        
        public virtual IList<Phrase>  Phrases { get; set; }

        public virtual IList<Idiom> Idioms { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Pronunciation) && !string.IsNullOrEmpty(Logograph) && Definitions.Any() && !string.IsNullOrEmpty(PartOfSpeech);
        }

        public static bool operator ==(Character init, string character)
        {
            return init != null && init.Logograph == character;
        }

        public static bool operator !=(Character init, string character)
        {
            return !(init == character);
        }
    }
}
