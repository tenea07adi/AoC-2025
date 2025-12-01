using AoC_2025.Enums;

namespace AoC_2025.Abstractions
{
    public interface IDataReaderService
    {
        public string ReadData(PuzzleIdentifier identifier);
        public string ReadDataByLines(PuzzleIdentifier identifier);
        public string[][] ReadDataAsMatrix(PuzzleIdentifier identifier);
        public string[][] ReadDataAsMatrix(PuzzleIdentifier identifier, string colExploader);
    }
}
