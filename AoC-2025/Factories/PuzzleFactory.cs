using AoC_2025.Abstractions;
using AoC_2025.Enums;
using AoC_2025.Puzzles;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Factories
{
    public class PuzzleFactory : IPuzzleFactory
    {
        private readonly IComunicationService _comunicationService;
        private readonly IDataReaderService _dataReader;

        public PuzzleFactory(IComunicationService comunicationService, IDataReaderService dataReader) 
        {
            _comunicationService = comunicationService;
            _dataReader = dataReader;
        }

        public PuzzleTemplate? Create(PuzzleIdentifier identifier)
        {
            PuzzleTemplate? puzzle = null;

            switch (identifier)
            {
                case PuzzleIdentifier.Day1:
                    puzzle = new Day1(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day2:
                    puzzle = new Day2(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day3:
                    puzzle = new Day3(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day4:
                    puzzle = new Day4(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day5:
                    puzzle = new Day5(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day6:
                    puzzle = new Day6(_comunicationService, _dataReader);
                    break;
                case PuzzleIdentifier.Day7:
                    puzzle = new Day7(_comunicationService, _dataReader);
                    break;
                default:
                    puzzle = null;
                    break;
            }

            return puzzle;
        }
    }
}
