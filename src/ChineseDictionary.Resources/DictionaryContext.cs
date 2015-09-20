using Microsoft.Data.Entity;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DictionaryContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Idiom> Idioms { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<GrammarNote> GrammarNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Character>().Ignore(c => c.ReviewTime);
            builder.Entity<Idiom>().Ignore(c => c.ReviewTime);
            builder.Entity<Phrase>().Ignore(c => c.ReviewTime);
        }
    }
}
