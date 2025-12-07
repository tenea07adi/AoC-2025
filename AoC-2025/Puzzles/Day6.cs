using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day6 : PuzzleTemplate
    {

        private const char _sumSymbol = '+';
        private const char _multiplySymbol = '*';

        public Day6(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day6)
        {

        }

        protected override void RunPartOne(string data)
        {
            var lines = data.Split(FileConstants.EndOfLine);

            var symbols = lines[lines.Length - 1].Split(FileConstants.WordsSeparator, StringSplitOptions.RemoveEmptyEntries);

            var numbers = new long[lines.Length - 1, symbols.Length];

            for (int i = 0; i < lines.Length-1; i++)
            {
                var lineNumbers = lines[i].Split(FileConstants.WordsSeparator, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < lineNumbers.Length; j++) {
                    numbers[i, j] = long.Parse(lineNumbers[j]);
                }
            }

            long totals = 0;

            for (int j = 0; j < numbers.GetLength(1); j++)
            {
                long? colTotal = null;

                for (int i = 0; i < numbers.GetLength(0); i++)
                {
                    //navigate trough the cols + line to make calculation on cols
                    colTotal = Operate(symbols[j][0], colTotal, numbers[i, j]);
                }

                totals += colTotal!.Value;
            }

            _comunicationService.WritePuzzlePartOneText(totals.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var rotatedData = RotateString(data);

            var lines = rotatedData.Split(FileConstants.EndOfLine);

            long totals = 0;

            long?[] lineNumbers = new long?[100];
            var numCount = 0;
            char? symbol = null;

            for (int i = 0; i < lines.Length - 1; i++)
            {
                var stringnNumber = "";

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == FileConstants.WordsSeparator[0])
                    {
                        continue;
                    }
                    else if (lines[i][j] == _sumSymbol || lines[i][j] == _multiplySymbol)
                    {
                        symbol = lines[i][j];
                        break;
                    }
                    else
                    {
                        stringnNumber += lines[i][j];
                    }
                }

                if(stringnNumber != "")
                {
                    lineNumbers[numCount] = long.Parse(stringnNumber);
                    numCount++;
                }
                if (symbol != null)
                {
                    long? segmentTotals = null;

                    for (int j = 0; j < lineNumbers.Length - 1; j++)
                    {
                        if (lineNumbers[j] == null)
                        {
                            break;
                        }

                        segmentTotals = Operate(symbol.Value, segmentTotals, lineNumbers[j]!.Value);
                    }

                    totals += segmentTotals!.Value;

                    // reset vars
                    lineNumbers = new long?[100];
                    numCount = 0;
                    symbol = null;
                }
            }

            _comunicationService.WritePuzzlePartOneText(totals.ToString(), true);
        }

        private long Operate(char symbol, long? leftOperator, long rightOperator)
        {
            switch (symbol)
            {
                case _sumSymbol : 
                    return (leftOperator ?? 0) + rightOperator;
                case _multiplySymbol :
                    return (leftOperator ?? 1) * rightOperator;
            }

            throw new NotImplementedException();
        }

        private string RotateString(string data)
        {
            string[] numbersLines = data.Split(FileConstants.EndOfLine);

            var rotated= "";

            for (int i = numbersLines[0].Length - 1; i >= 0; i--)
            {

            }

            for (int j = numbersLines[0].Length - 1; j >= 0; j--)
            {
                for (int i = 0; i < numbersLines.Length; i++)
                {
                    rotated += numbersLines[i][j];
                }
                rotated += FileConstants.EndOfLine;
            }

            return rotated;
        }
    }
}