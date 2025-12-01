using AoC_2025.Abstractions;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day1 : PuzzleTemplate
    {
        public Day1(IComunicationService comunicationService, IDataReaderService dataReaderService) 
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day1)
        {
            
        }

        protected override void RunPartOne(string data)
        {
            _comunicationService.WritePuzzlePartOneText(data, true);
        }

        protected override void RunPartTwo(string data)
        {
            _comunicationService.WritePuzzlePartTwoText(data, true);
        }
    }
}
