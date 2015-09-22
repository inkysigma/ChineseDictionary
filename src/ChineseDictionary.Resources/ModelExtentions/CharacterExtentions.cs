using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources.ModelExtentions
{
    public static class CharacterExtentions
    {
        public static bool Validate(this Character character)
        {
            return !string.IsNullOrEmpty(character.Pronunciation) && !string.IsNullOrEmpty(character.Logograph) &&
                   character.Definitions.Any() && character.Logograph.Length == 1;
        }
    }
}

namespace ChineseDictionary.Resources.Models
{
    public partial class Character
    {
        protected bool Equals(Models.Character other)
        {
            return Number == other.Number && Priority == other.Priority && string.Equals(Logograph, other.Logograph) &&
                   string.Equals(Pronunciation, other.Pronunciation) && Equals(ReviewTime, other.ReviewTime) &&
                   Equals(Definitions, other.Definitions) && Equals(Usages, other.Usages) &&
                   Equals(Phrases, other.Phrases) && Equals(Idioms, other.Idioms);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Models.Character) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Number;
                hashCode = (hashCode*397) ^ Priority;
                hashCode = (hashCode*397) ^ (Logograph != null ? Logograph.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Pronunciation != null ? Pronunciation.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ReviewTime != null ? ReviewTime.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Definitions != null ? Definitions.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Usages != null ? Usages.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Phrases != null ? Phrases.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Idioms != null ? Idioms.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}