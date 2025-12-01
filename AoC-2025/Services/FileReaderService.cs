using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;

namespace AoC_2025.Services
{
    public class FileReaderService : IDataReaderService
    {
        public string ReadData(PuzzleIdentifier identifier)
        {
            string path = string.Format(FileConstants.DataFilePath, identifier.ToString());
            return File.ReadAllText(Path.Combine(Environment.CurrentDirectory, path));
        }

        public string[][] ReadDataAsMatrix(PuzzleIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        public string[][] ReadDataAsMatrix(PuzzleIdentifier identifier, string colExploader)
        {
            throw new NotImplementedException();
        }

        public string[] ReadDataByLines(PuzzleIdentifier identifier)
        {
            var data = ReadData(identifier);

            return data.Split(FileConstants.EndOfLine);
        }
    }
}
