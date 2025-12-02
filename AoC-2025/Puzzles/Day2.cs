using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.ExtensionMethods;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day2 : PuzzleTemplate
    {
        public Day2(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day2)
        {

        }

        protected override void RunPartOne(string data)
        {
            var ranges = SplitIntoRanges(data);

            long totalIDs = 0;

            for (int i = 0; i < ranges.GetLength(0); i++)
            {
                if (ranges[i,0].Length % 2 != 0 && ranges[i, 0].Length == ranges[i, 1].Length)
                {
                    continue;
                }

                var leftElement = long.Parse(ranges[i, 0]);
                var rightElement = long.Parse(ranges[i, 1]);

                for (long currentElement = leftElement; currentElement <= rightElement; currentElement++)
                {
                    var match = true;

                    var currentElementString = currentElement.ToString();
                    var nl = currentElementString.Length;

                    if(nl % 2 != 0)
                    {
                        continue;
                    }

                    for (int d = 0; d < nl / 2; d++)
                    {
                        if(currentElementString[d] != currentElementString[nl/2 + d])
                        {
                            match = false; 
                            break;
                        }
                    }

                    if (match)
                    {
                        totalIDs += currentElement;
                    }
                }
            }

            _comunicationService.WritePuzzlePartOneText(totalIDs.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var ranges = SplitIntoRanges(data);

            long totalIDs = 0;

            for (int i = 0; i < ranges.GetLength(0); i++)
            {
                var leftElement = long.Parse(ranges[i, 0]);
                var rightElement = long.Parse(ranges[i, 1]);

                for (long currentElement = leftElement; currentElement <= rightElement; currentElement++)
                {
                    var currentElementString = currentElement.ToString();

                    if (ContainsPattern(currentElement, currentElementString))
                    {
                        totalIDs += currentElement;
                    }
                }
            }

            _comunicationService.WritePuzzlePartOneText(totalIDs.ToString(), true);
        }

        private bool ContainsPattern(long number, string numberAsString)
        {
            var nl = numberAsString.Length;

            var patternLength = nl / 2;

            for(int i = patternLength; i > 0; i = i - 1)
            {
                var subStrings = numberAsString.SplitInChunks(i).ToArray();

                if (subStrings.Length * i < numberAsString.Length)
                {
                    continue;
                }

                bool match = true;

                for(int j = 0; j < subStrings.Length - 1; j++)
                {
                    if (subStrings[j] != subStrings[j + 1])
                    {
                        match = false; 
                        break;
                    }
                }

                if (match)
                {
                    return true;
                }
            }

            return false;
        }

        private string[,] SplitIntoRanges(string data)
        {
            var rawRanges = data.Split(FileConstants.CommaSeparator);
            string[,] ranges = new string[rawRanges.Length, 2];

            for (int i = 0; i < rawRanges.Length; i++)
            {
                var elements = rawRanges[i].Split(FileConstants.DashSeparator);
                ranges[i, 0] = elements[0];
                ranges[i, 1] = elements[1];
            }

            return ranges;
        }
    }
}