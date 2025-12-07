using AoC_2025.Abstractions;
using AoC_2025.Enums;
using AoC_2025.ExtensionMethods;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day7 : PuzzleTemplate
    {
        private static char _startSymbol = 'S';
        private static char _beamSymbol = '|';
        private static char _splitterSymbol = '^';

        private static char _notUsedSplitterSymbol = '*';

        public Day7(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day7)
        {

        }

        protected override void RunPartOne(string data)
        {
            var matrix = data.ToCharMatrix();

            matrix = ReplaceNotUsedSplitters(matrix);

            var usedSplitters = CountUsedSplitters(matrix);

            _comunicationService.WritePuzzlePartOneText(usedSplitters.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var matrix = data.ToCharMatrix();

            var startCol = 0;

            for(int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[0,j] == _startSymbol)
                {
                    startCol = j;
                    break;
                }
            }

            long?[,] completionMatrix = new long?[matrix.GetLength(0), matrix.GetLength(1)];

            var total = GetPossibleDirections(matrix, completionMatrix, 0, startCol);

            _comunicationService.WritePuzzlePartOneText(total.ToString(), true);
        }

        private int CountUsedSplitters(char[,] matrix)
        {
            var total = 0;
            for (var i = 1; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == _splitterSymbol)
                    {
                        total++;
                    }
                }
            }

            return total;
        }

        private char[,] ReplaceNotUsedSplitters(char[,] matrix)
        {
            for (var i = 1; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == _splitterSymbol && !IsUsedSplitter(matrix, i, j))
                    {
                        matrix[i, j] = _notUsedSplitterSymbol;
                    }
                }
            }

            return matrix;
        }

        private bool IsUsedSplitter(char[,] matrix, int sLine, int sCol)
        {
            for (var i = sLine - 1; i >= 0; i--)
            {
                if (matrix[i, sCol] == _splitterSymbol || matrix[i, sCol] == _notUsedSplitterSymbol)
                {
                    return false;
                }
                else if(matrix[i, sCol - 1] == _notUsedSplitterSymbol || matrix[i, sCol + 1] == _notUsedSplitterSymbol)
                {
                    //return false;
                }
                else if (matrix[i, sCol] == _splitterSymbol || matrix[i, sCol] == _startSymbol
                    || matrix[i, sCol - 1] == _splitterSymbol || matrix[i, sCol - 1] == _startSymbol
                    || matrix[i, sCol + 1] == _splitterSymbol || matrix[i, sCol + 1] == _startSymbol)
                {
                    return true;
                }
            }

            return false;
        }

        private long GetPossibleDirections(char[,] matrix, long?[,] completionMatrix, int sLine, int sCol)
        {
            long total = 0;

            bool leftCompleted = false;
            bool rightCompleted = false;

            for (var i = sLine + 1; i < matrix.GetLength(0); i++)
            {
                if(!leftCompleted && matrix[i, sCol-1] == _splitterSymbol)
                {
                    if (completionMatrix[i, sCol-1] != null)
                    {
                        total += completionMatrix[i, sCol - 1]!.Value;
                    }
                    else
                    {
                        total += GetPossibleDirections(matrix, completionMatrix, i, sCol - 1);
                    }

                    leftCompleted = true;
                }

                if (!rightCompleted && matrix[i, sCol + 1] == _splitterSymbol)
                {
                    if (completionMatrix[i, sCol + 1] != null)
                    {
                        total += completionMatrix[i, sCol + 1]!.Value;
                    }
                    else
                    {
                        total += GetPossibleDirections(matrix, completionMatrix, i, sCol + 1);
                    }
                    
                    rightCompleted = true;
                }

                if(leftCompleted && rightCompleted)
                {
                    break;
                }
            }

            if(!leftCompleted)
            {
                total++;
            }

            if (!rightCompleted)
            {
                total++;
            }

            completionMatrix[sLine, sCol] = total;

            return total;
        }
    }
}