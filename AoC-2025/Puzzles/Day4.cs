using AoC_2025.Abstractions;
using AoC_2025.Enums;
using AoC_2025.ExtensionMethods;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day4 : PuzzleTemplate
    {
        private const char _emptySpaceSymbol = '.';
        private const char _paperRollSymbol = '@';

        public Day4(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day4)
        {

        }

        protected override void RunPartOne(string data)
        {
            char[,] storage = data.ToCharMatrix();

            var accessibleRollsCount = 0;

            for (int i = 0; i < storage.GetLength(0); i++)
            {
                for (int j = 0; j < storage.GetLength(1); j++)
                {

                    if (storage[i,j] != _paperRollSymbol)
                    {
                        continue;
                    }

                    if (!BusySurroundings(storage, i, j))
                    {
                        accessibleRollsCount++;
                    }
                }
            }

            _comunicationService.WritePuzzlePartOneText(accessibleRollsCount.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            char[,] storage = data.ToCharMatrix();

            var accessibleRollsCount = 0;

            for (int i = 0; i < storage.GetLength(0); i++)
            {
                for (int j = 0; j < storage.GetLength(1); j++)
                {

                    if (storage[i,j] != _paperRollSymbol)
                    {
                        continue;
                    }

                    if (!BusySurroundings(storage, i, j))
                    {
                        accessibleRollsCount++;

                        storage[i,j] = _emptySpaceSymbol;
                        i = 0;
                        j = 0;
                    }
                }
            }

            _comunicationService.WritePuzzlePartOneText(accessibleRollsCount.ToString(), true);
        }

        private bool BusySurroundings(char[,] data, int line, int col)
        {
            var elementsCount = 0;

            var startL = line > 0 ? line - 1 : 0;
            var startC = col > 0 ? col - 1 : 0;
            var endL = line < data.GetLength(0) - 2 ? line + 1 : data.GetLength(0) - 1;
            var endC = col < data.GetLength(1) - 2 ? col + 1 : data.GetLength(1) - 1;

            for (int i = startL; i <= endL; i++)
            {
                for (int j = startC; j <= endC; j++)
                {
                    if(i == line && j == col)
                    {
                        continue;
                    }

                    if (data[i,j] == _paperRollSymbol)
                    {
                        elementsCount++;
                    }

                    if(elementsCount >= 4)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
