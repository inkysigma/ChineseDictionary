using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace ChineseDictionary.Resources.Models
{
    public class Character
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Number { get; set; }

        public int Priority { get; set; }

        [Key]
        public string Logograph { get; set; }

        public string Pronunciation { get; set; }

        public DateTime ReviewTime { get; set; }

        public virtual ICollection<DefinitionEntry> Definitions { get; set; }

        public virtual ICollection<Usage> Usages { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Phrase> Phrases { get; set; }

        [JsonIgnore]
        public virtual ICollection<Idiom> Idioms { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Pronunciation) && !string.IsNullOrEmpty(Logograph) && Definitions.Any() && Logograph.Length == 1;
        }

        public static bool operator ==(Character init, string character)
        {
            if (string.IsNullOrEmpty(character) || init?.Logograph == null)
                return false;
            return init.Logograph == character;
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

        public bool ShouldSerializeReviewTime()
        {
            return false;
        }
    }
}
