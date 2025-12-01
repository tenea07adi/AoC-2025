using AoC_2025.Abstractions;
using AoC_2025.Constants;
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
            var dataLines = data.Split(FileConstants.EndOfLine);
            var dialPosition = Int32.Parse(dataLines[0]);

            var totalZeroPosition = 0;

            for (int i = 1; i < dataLines.Length; i++)
            {
                var direction = dataLines[i][0];
                var number = Int32.Parse(dataLines[i].Substring(1));

                if( direction == 'L')
                {
                    dialPosition -= number;
                }
                else
                {
                    dialPosition += number;
                }

                while (dialPosition >= 100 || dialPosition <= -100)
                {
                    dialPosition = dialPosition % 100;
                }

                if (dialPosition < 0)
                {
                    dialPosition = 100 - (dialPosition * -1);
                }

                if(dialPosition == 0)
                {
                    totalZeroPosition++;
                }
            }

            _comunicationService.WritePuzzlePartOneText(totalZeroPosition.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var dataLines = data.Split(FileConstants.EndOfLine);
            var dialPosition = Int32.Parse(dataLines[0]);

            var totalOverTimes = 0;

            for (int i = 1; i < dataLines.Length; i++)
            {
                var direction = dataLines[i][0];
                var number = Int32.Parse(dataLines[i].Substring(1));

                var initialDial= dialPosition;

                if (direction == 'L')
                {
                    dialPosition -= number;
                }
                else
                { 
                    dialPosition += number;
                }

                var overTimes = 0;

                if (dialPosition >= 100 || dialPosition <= -100)
                {
                    overTimes += dialPosition / 100;
                    dialPosition = dialPosition % 100;
                }

                totalOverTimes += overTimes >= 0 ? overTimes : (overTimes * -1);

                if (initialDial > 0 && (dialPosition < 0 || overTimes < 0))
                {
                    totalOverTimes++;
                }

                if (dialPosition < 0)
                {
                    dialPosition = 100 - (dialPosition * -1);
                }

                if (dialPosition == 0 && overTimes == 0 && initialDial != 0)
                {
                    totalOverTimes++;
                }
            }

            _comunicationService.WritePuzzlePartOneText((totalOverTimes).ToString(), true);
        }
    }
}
