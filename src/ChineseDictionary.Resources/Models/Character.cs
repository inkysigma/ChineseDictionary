using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace ChineseDictionary.Resources.Models
{
    public partial class Character
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Number { get; set; }

        public int Priority { get; set; }

        [Key]
        public string Logograph { get; set; }

        public string Pronunciation { get; set; }

        public ReviewDateTime ReviewTime { get; set; }

        public virtual ICollection<DefinitionEntry> Definitions { get; set; }

        public virtual ICollection<Usage> Usages { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Phrase> Phrases { get; set; }

        [JsonIgnore]
        public virtual ICollection<Idiom> Idioms { get; set; }

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
    }
}
