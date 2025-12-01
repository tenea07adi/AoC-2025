using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using Spectre.Console;

namespace AoC_2025.Services
{
    public class SpectreComunicationService : IComunicationService
    {
        #region Write
        public void WriteText(string text, bool isBold = false)
        {
            Style style = new Style(
                foreground: Color.Gray100,
                decoration: isBold ? Decoration.Bold : Decoration.None
                );

            AnsiConsole.Write(new Markup(text, style));
        }

        public void WriteDescription(string title, string name, string description, string aboutAoC)
        {
            Style descriptionStyle = new Style(
                foreground: Color.Gray100
                );

            Style AoCStyle = new Style(
                foreground: Color.Gray100,
                link: TextConstants.UrlAoC
                );

            AnsiConsole.Write(new FigletText(title).Centered().Color(Color.Red));
            AnsiConsole.Write(new FigletText(name).Centered().Color(Color.Green));
            WriteNextLine();
            AnsiConsole.Write(new Markup(description, descriptionStyle));
            WriteNextLine();
            AnsiConsole.Write(new Markup(aboutAoC, AoCStyle));
            WriteNextLine();
            WriteNextLine();

        }

        public void WritePuzzlePartOneText(string data, bool isBold)
        {
            Style style = new Style(
                foreground: Color.Silver,
                decoration: isBold ? Decoration.Bold : Decoration.SlowBlink
                );

            AnsiConsole.Write(new Markup(data, style));
        }

        public void WritePuzzlePartTwoText(string data, bool isBold)
        {
            Style style = new Style(
                foreground: Color.Gold1,
                decoration: isBold ? Decoration.Bold : Decoration.SlowBlink
                );

            AnsiConsole.Write(new Markup(data, style));
        }

        public void WriteError(string text)
        {
            Style style = new Style(
                foreground: Color.Red,
                decoration: Decoration.Bold | Decoration.SlowBlink
                );

            AnsiConsole.Write(new Markup(text, style));
        }

        public void WriteNextLine()
        {
            AnsiConsole.Write(FileConstants.EndOfLine);
        }

        public void Clean()
        {
            AnsiConsole.Clear();
        }
        #endregion

        #region Read
        public PuzzleIdentifier ReadPuzzleIdentifier()
        {
            var puzzles = Enum.GetValues<PuzzleIdentifier>();

            PuzzleIdentifier selectedPuzzle = AnsiConsole.Prompt(
                new SelectionPrompt<PuzzleIdentifier>()
                    .Title(TextConstants.SelectPuzzle)
                    .PageSize(10)
                    .MoreChoicesText(TextConstants.MoreOptionsSelectPuzzle)
                    .AddChoices(puzzles));

            return selectedPuzzle;
        }

        public bool ReadBoolean(string text)
        {
            bool response = AnsiConsole.Prompt(
                new TextPrompt<bool>(text)
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(true)
                );

            return response;
        }
        #endregion
    }
}
