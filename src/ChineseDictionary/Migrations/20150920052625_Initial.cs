using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ChineseDictionary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrammarNote",
                columns: table => new
                {
                    Number = table.Column<int>(isNullable: false),
                    Note = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrammarNote", x => x.Number);
                });
            migrationBuilder.CreateTable(
                name: "JournalEntry",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(isNullable: false),
                    Entry = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntry", x => x.DateTime);
                });
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Logograph = table.Column<string>(isNullable: false),
                    JournalEntryDateTime = table.Column<DateTime>(isNullable: true),
                    Number = table.Column<int>(isNullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Priority = table.Column<int>(isNullable: false),
                    Pronunciation = table.Column<string>(isNullable: true),
                    ReviewTime = table.Column<DateTime>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Logograph);
                    table.ForeignKey(
                        name: "FK_Character_JournalEntry_JournalEntryDateTime",
                        column: x => x.JournalEntryDateTime,
                        principalTable: "JournalEntry",
                        principalColumn: "DateTime");
                });
            migrationBuilder.CreateTable(
                name: "Idiom",
                columns: table => new
                {
                    Word = table.Column<string>(isNullable: false),
                    JournalEntryDateTime = table.Column<DateTime>(isNullable: true),
                    Number = table.Column<int>(isNullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Pronunciation = table.Column<string>(isNullable: true),
                    Story = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiom", x => x.Word);
                    table.ForeignKey(
                        name: "FK_Idiom_JournalEntry_JournalEntryDateTime",
                        column: x => x.JournalEntryDateTime,
                        principalTable: "JournalEntry",
                        principalColumn: "DateTime");
                });
            migrationBuilder.CreateTable(
                name: "Phrase",
                columns: table => new
                {
                    Word = table.Column<string>(isNullable: false),
                    JournalEntryDateTime = table.Column<DateTime>(isNullable: true),
                    Number = table.Column<int>(isNullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Pronunciation = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrase", x => x.Word);
                    table.ForeignKey(
                        name: "FK_Phrase_JournalEntry_JournalEntryDateTime",
                        column: x => x.JournalEntryDateTime,
                        principalTable: "JournalEntry",
                        principalColumn: "DateTime");
                });
            migrationBuilder.CreateTable(
                name: "DefinitionEntry",
                columns: table => new
                {
                    Number = table.Column<int>(isNullable: false),
                    CharacterLogograph = table.Column<string>(isNullable: true),
                    Definition = table.Column<string>(isNullable: true),
                    IdiomWord = table.Column<string>(isNullable: true),
                    PartOfSpeech = table.Column<string>(isNullable: true),
                    PhraseWord = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefinitionEntry", x => x.Number);
                    table.ForeignKey(
                        name: "FK_DefinitionEntry_Character_CharacterLogograph",
                        column: x => x.CharacterLogograph,
                        principalTable: "Character",
                        principalColumn: "Logograph");
                    table.ForeignKey(
                        name: "FK_DefinitionEntry_Idiom_IdiomWord",
                        column: x => x.IdiomWord,
                        principalTable: "Idiom",
                        principalColumn: "Word");
                    table.ForeignKey(
                        name: "FK_DefinitionEntry_Phrase_PhraseWord",
                        column: x => x.PhraseWord,
                        principalTable: "Phrase",
                        principalColumn: "Word");
                });
            migrationBuilder.CreateTable(
                name: "Usage",
                columns: table => new
                {
                    Number = table.Column<int>(isNullable: false),
                    CharacterLogograph = table.Column<string>(isNullable: true),
                    IdiomWord = table.Column<string>(isNullable: true),
                    PhraseWord = table.Column<string>(isNullable: true),
                    Sentence = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usage", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Usage_Character_CharacterLogograph",
                        column: x => x.CharacterLogograph,
                        principalTable: "Character",
                        principalColumn: "Logograph");
                    table.ForeignKey(
                        name: "FK_Usage_Idiom_IdiomWord",
                        column: x => x.IdiomWord,
                        principalTable: "Idiom",
                        principalColumn: "Word");
                    table.ForeignKey(
                        name: "FK_Usage_Phrase_PhraseWord",
                        column: x => x.PhraseWord,
                        principalTable: "Phrase",
                        principalColumn: "Word");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("DefinitionEntry");
            migrationBuilder.DropTable("GrammarNote");
            migrationBuilder.DropTable("Usage");
            migrationBuilder.DropTable("Character");
            migrationBuilder.DropTable("Idiom");
            migrationBuilder.DropTable("Phrase");
            migrationBuilder.DropTable("JournalEntry");
        }
    }
}
