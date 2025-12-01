using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Factories;
using AoC_2025.Services;

namespace AoC_2025
{
    internal class Program
    {
        private static IComunicationService? _comunicationService;
        private static IDataReaderService? _dataReader;

        private static IPuzzleFactory? _puzzleFactory;

        static void Main(string[] args)
        {
            Init();

            while (true)
            {
                DisplayTitle();
                Run();

                if (AskForRunAgain())
                {
                    _comunicationService!.Clean();
                }
                else
                {
                    break;
                }
            }
        }

        # region UserInteraction
        private static void DisplayTitle()
        {
            _comunicationService!.WriteDescription(TextConstants.Title, TextConstants.Author, TextConstants.Description, TextConstants.AboutAoC);
        }

        private static bool AskForRunAgain()
        {
            _comunicationService!.WriteNextLine();
            _comunicationService.WriteNextLine();

            return _comunicationService.ReadBoolean(TextConstants.ResetConsole);
        }

        #endregion

        #region Logic
        private static void Init()
        {
            _comunicationService = new SpectreComunicationService();
            _dataReader = new FileReaderService();

            _puzzleFactory = new PuzzleFactory(_comunicationService, _dataReader);
        }

        private static void Run()
        {
            var selectedPuzzle = _comunicationService!.ReadPuzzleIdentifier();

            var puzzle = _puzzleFactory!.Create(selectedPuzzle);

            if (puzzle == null)
            {
                _comunicationService.WriteError(TextConstants.PuzzleNotFound);
                return;
            }

            puzzle.Run();
        }
        #endregion
    }
}
