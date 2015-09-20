using System.Data.Entity;
using System.Diagnostics;
using ChineseDictionary.Resources.Configuration;
using ChineseDictionary.Resources.Models;

namespace ChineseDictionary.Resources
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class DictionaryContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Idiom> Idioms { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<GrammarNote> GrammarNotes { get; set; }

        public DictionaryContext(string connection) : base(connection)
        {
            Database.Log = s => Debug.WriteLine(s);
        }
    }
}
