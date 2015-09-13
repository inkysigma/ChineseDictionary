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

        public string Pronunciation { get; set; }

        public IDictionary<string, string> Definitions { get; set; }

        public IList<string> Usages { get; set; }
        
        public virtual IList<Phrase>  Phrases { get; set; }

        public virtual IList<Idiom> Idioms { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Pronunciation) && !string.IsNullOrEmpty(Logograph) && Definitions.Any();
        }

        public static bool operator ==(Character init, string character)
        {
            return init != null && init.Logograph == character;
        }

        public static bool operator !=(Character init, string character)
        {
            return !(init == character);
        }

        protected bool Equals(Character other)
        {
            return Number == other.Number && string.Equals(Logograph, other.Logograph) &&
                   string.Equals(Pronunciation, other.Pronunciation) && Equals(Definitions, other.Definitions) &&
                   Equals(Usages, other.Usages) && Equals(Phrases, other.Phrases) && Equals(Idioms, other.Idioms);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Character)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Number;
                hashCode = (hashCode * 397) ^ (Logograph != null ? Logograph.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Pronunciation != null ? Pronunciation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Definitions != null ? Definitions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Usages != null ? Usages.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Phrases != null ? Phrases.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Idioms != null ? Idioms.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
