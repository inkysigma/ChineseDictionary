using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseDictionary.Resources.Models
{
    public class JournalEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
        public string Entry { get; set; }
        public virtual ICollection<Character> KeyCharacters { get; set; }
        public virtual ICollection<Idiom> KeyIdioms { get; set; }
        public virtual ICollection<Phrase> KeyPhrases { get; set; } 
    }
}
