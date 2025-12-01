using AoC_2025.Enums;

namespace AoC_2025.Abstractions
{
    public interface IComunicationService
    {
        public void WriteText(string text, bool isBold = false);
        public void WriteDescription(string title, string name, string description, string aboutAoC);

        public void WritePuzzlePartOneText(string data, bool isBold);
        public void WritePuzzlePartTwoText(string data, bool isBold);

        public void WriteNextLine();
        public void Clean();

        public void WriteError(string text);

        public PuzzleIdentifier ReadPuzzleIdentifier();
        public bool ReadBoolean(string text);
    }
}
