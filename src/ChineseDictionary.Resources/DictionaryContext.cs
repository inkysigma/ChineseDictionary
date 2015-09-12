using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChineseDictionary.Resources.Models;
using System.Data;

namespace ChineseDictionary.Resources
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [DbConfigurationType(typeof(DbConfiguration))]
    public class DictionaryContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Idiom> Idioms { get; set; }

        public DictionaryContext(string connection) : base(connection)
        {
            
        }
    }
}
