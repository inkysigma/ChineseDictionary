using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ChineseDictionary.temp;

namespace ChineseDictionary.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    partial class Initial
    {
        public override string Id
        {
            get { return "20150920052625_Initial"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540");

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Character", b =>
                {
                    b.Property<string>("Logograph");

                    b.Property<DateTime?>("JournalEntryDateTime");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Priority");

                    b.Property<string>("Pronunciation");

                    b.Property<DateTime>("ReviewTime");

                    b.Key("Logograph");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.DefinitionEntry", b =>
                {
                    b.Property<int>("Number");

                    b.Property<string>("CharacterLogograph");

                    b.Property<string>("Definition");

                    b.Property<string>("IdiomWord");

                    b.Property<string>("PartOfSpeech");

                    b.Property<string>("PhraseWord");

                    b.Key("Number");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.GrammarNote", b =>
                {
                    b.Property<int>("Number");

                    b.Property<string>("Note");

                    b.Key("Number");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Idiom", b =>
                {
                    b.Property<string>("Word");

                    b.Property<DateTime?>("JournalEntryDateTime");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Pronunciation");

                    b.Property<string>("Story");

                    b.Key("Word");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.JournalEntry", b =>
                {
                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Entry");

                    b.Key("DateTime");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Phrase", b =>
                {
                    b.Property<string>("Word");

                    b.Property<DateTime?>("JournalEntryDateTime");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Pronunciation");

                    b.Key("Word");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Usage", b =>
                {
                    b.Property<int>("Number");

                    b.Property<string>("CharacterLogograph");

                    b.Property<string>("IdiomWord");

                    b.Property<string>("PhraseWord");

                    b.Property<string>("Sentence");

                    b.Key("Number");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Character", b =>
                {
                    b.Reference("ChineseDictionary.Resources.Models.JournalEntry")
                        .InverseCollection()
                        .ForeignKey("JournalEntryDateTime");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.DefinitionEntry", b =>
                {
                    b.Reference("ChineseDictionary.Resources.Models.Character")
                        .InverseCollection()
                        .ForeignKey("CharacterLogograph");

                    b.Reference("ChineseDictionary.Resources.Models.Idiom")
                        .InverseCollection()
                        .ForeignKey("IdiomWord");

                    b.Reference("ChineseDictionary.Resources.Models.Phrase")
                        .InverseCollection()
                        .ForeignKey("PhraseWord");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Idiom", b =>
                {
                    b.Reference("ChineseDictionary.Resources.Models.JournalEntry")
                        .InverseCollection()
                        .ForeignKey("JournalEntryDateTime");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Phrase", b =>
                {
                    b.Reference("ChineseDictionary.Resources.Models.JournalEntry")
                        .InverseCollection()
                        .ForeignKey("JournalEntryDateTime");
                });

            modelBuilder.Entity("ChineseDictionary.Resources.Models.Usage", b =>
                {
                    b.Reference("ChineseDictionary.Resources.Models.Character")
                        .InverseCollection()
                        .ForeignKey("CharacterLogograph");

                    b.Reference("ChineseDictionary.Resources.Models.Idiom")
                        .InverseCollection()
                        .ForeignKey("IdiomWord");

                    b.Reference("ChineseDictionary.Resources.Models.Phrase")
                        .InverseCollection()
                        .ForeignKey("PhraseWord");
                });
        }
    }
}
