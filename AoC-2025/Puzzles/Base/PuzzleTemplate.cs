using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;

namespace AoC_2025.Puzzles.Base
{
    public abstract class PuzzleTemplate
    {
        protected readonly IComunicationService _comunicationService;
        protected readonly IDataReaderService _dataReaderService;

        public PuzzleIdentifier Identifier { get; }

        public PuzzleTemplate(IComunicationService comunicationService, IDataReaderService dataReaderService, PuzzleIdentifier identifier)
        {
            _comunicationService = comunicationService;
            _dataReaderService = dataReaderService;

            Identifier = identifier;
        }

        public void Run()
        {
            var puzzleData = _dataReaderService.ReadData(Identifier);

            _comunicationService.WriteText(string.Format(TextConstants.SelectedDay, Identifier.ToString()), true);

            _comunicationService.WriteNextLine();
            _comunicationService.WriteNextLine();
            _comunicationService.WritePuzzlePartOneText(TextConstants.PartOneResult, false);
            _comunicationService.WriteNextLine();
            RunPartOne(puzzleData);

            _comunicationService.WriteNextLine();
            _comunicationService.WriteNextLine();
            _comunicationService.WritePuzzlePartTwoText(TextConstants.PartTwoResult, false);
            _comunicationService.WriteNextLine();
            RunPartTwo(puzzleData);
        }

        protected abstract void RunPartOne(string data);
        protected abstract void RunPartTwo(string data);
    }
}
