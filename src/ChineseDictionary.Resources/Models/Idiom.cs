using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChineseDictionary.Resources.Models
{
    public class Idiom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }

        public string Pronunciation { get; set; }

        public ReviewDateTime ReviewTime { get; set; }

        [Key]
        public string Word { get; set; }

        public string Story { get; set; }

        public virtual ICollection<DefinitionEntry> Definitions { get; set; }

        public virtual ICollection<Usage> Usages { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
