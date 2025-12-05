using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day5 : PuzzleTemplate
    {
        public Day5(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day5)
        {

        }

        protected override void RunPartOne(string data)
        {
            var dataParts = data.Split(FileConstants.EndOfLine + FileConstants.EndOfLine);

            var freshIntervalsRaw = dataParts[0].Split(FileConstants.EndOfLine);

            var freshIntervals = new long[freshIntervalsRaw.Length,2];

            for (int i = 0; i < freshIntervalsRaw.Length; i++)
            {
                var ranges = freshIntervalsRaw[i].Split(FileConstants.DashSeparator);
                freshIntervals[i, 0] = long.Parse(ranges[0]);
                freshIntervals[i, 1] = long.Parse(ranges[1]);
            }

            var ingredientIdsAsString = dataParts[1].Split(FileConstants.EndOfLine);

            var freshIngredientsCount = 0;

            for (int i = 0; i < ingredientIdsAsString.Length; i++)
            {
                var ing = long.Parse(ingredientIdsAsString[i]);

                if(IsFreshElement(freshIntervals, ing))
                {
                    freshIngredientsCount++;
                }
            }

            _comunicationService.WritePuzzlePartOneText(freshIngredientsCount.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var dataParts = data.Split(FileConstants.EndOfLine + FileConstants.EndOfLine);

            var freshIntervalsRaw = dataParts[0].Split(FileConstants.EndOfLine);

            var freshIntervals = new long[freshIntervalsRaw.Length, 2];

            for (int i = 0; i < freshIntervalsRaw.Length; i++)
            {
                var ranges = freshIntervalsRaw[i].Split(FileConstants.DashSeparator);
                freshIntervals[i, 0] = long.Parse(ranges[0]);
                freshIntervals[i, 1] = long.Parse(ranges[1]);
            }

            var freshIngredientsCount = TotalFreshElementsInIntervals(freshIntervals);

            _comunicationService.WritePuzzlePartOneText(freshIngredientsCount.ToString(), true);
        }

        private bool IsFreshElement(long[,] freshIntervals, long ingredientId)
        {
            for (int i = 0; i < freshIntervals.GetLength(0); i++)
            {
                if (ingredientId >= freshIntervals[i,0] && ingredientId <= freshIntervals[i, 1])
                {
                    return true;
                }
            }

            return false;
        }
        
        private long[,] EliminateIntervalsDuplication(long[,] freshIntervals)
        {
            for (int i = 0; i < freshIntervals.GetLength(0); i++)
            {
                for (int j = i+1; j < freshIntervals.GetLength(0); j++)
                {
                    freshIntervals = EliminateIntervalsDuplicationForSegment(freshIntervals, i, j);
                    freshIntervals = EliminateIntervalsDuplicationForSegment(freshIntervals, j, i);
                }
            }

            return freshIntervals;
        }

        private long[,] EliminateIntervalsDuplicationForSegment(long[,] freshIntervals, int firstIntervalPos, int secontIntervalPos)
        {
            if (freshIntervals[firstIntervalPos, 0] <= freshIntervals[secontIntervalPos, 0] && freshIntervals[firstIntervalPos, 1] >= freshIntervals[secontIntervalPos, 0])
            {
                if (freshIntervals[firstIntervalPos, 1] >= freshIntervals[secontIntervalPos, 1])
                {
                    freshIntervals[secontIntervalPos, 0] = 0;
                    freshIntervals[secontIntervalPos, 1] = 0;
                }
                else
                {
                    freshIntervals[firstIntervalPos, 1] = freshIntervals[secontIntervalPos, 0] - 1;

                    if (freshIntervals[firstIntervalPos, 0] > freshIntervals[secontIntervalPos, 1])
                    {
                        freshIntervals[firstIntervalPos, 0] = 0;
                        freshIntervals[firstIntervalPos, 1] = 0;
                    }
                }
            }

            return freshIntervals;
        }

        private long TotalFreshElementsInIntervals(long[,] freshIntervals)
        {
            freshIntervals = EliminateIntervalsDuplication(freshIntervals);

            long count = 0;

            for (int i = 0; i < freshIntervals.GetLength(0); i++)
            {
                if(freshIntervals[i, 0] == 0 && freshIntervals[i, 1] == 0)
                {
                    continue;
                }

                count += freshIntervals[i, 1] - freshIntervals[i, 0] + 1;
            }

            return count;
        }
    }
}
