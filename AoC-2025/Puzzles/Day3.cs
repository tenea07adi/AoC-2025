using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day3 : PuzzleTemplate
    {
        public Day3(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day3)
        {

        }

        protected override void RunPartOne(string data)
        {
            var batteries = data.Split(FileConstants.EndOfLine);

            var totalJoltage = 0;

            for (int i = 0; i < batteries.Length; i++)
            {
                var currentBattery = batteries[i];

                var max = '0';
                var secondMax = '0';

                for (int j = 0; j < currentBattery.Length; j++)
                {
                    if (currentBattery[j] > secondMax)
                    {
                        if(secondMax > max)
                        {
                            max = secondMax;
                        }

                        secondMax = currentBattery[j];
                    }
                    else if (secondMax > max)
                    {
                        max = secondMax;
                        secondMax = currentBattery[j];
                    }
                }

                var joltage = int.Parse($"{max}{secondMax}");

                totalJoltage += joltage;
            }

            _comunicationService.WritePuzzlePartOneText(totalJoltage.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var batteries = data.Split(FileConstants.EndOfLine);

            long totalJoltage = 0;

            var joltageLength = 12;

            for (int i = 0; i < batteries.Length; i++)
            {
                var currentBattery = batteries[i];

                var newNumber = new char[joltageLength];

                for (int x = 0; x < joltageLength; x++)
                {
                    newNumber[x] = '0';
                }

                for (int j = 0; j < currentBattery.Length; j++) {
                    var positionToDecalate = GetDecalationPosition(newNumber, currentBattery[j]);

                    if (positionToDecalate != null)
                    {
                        newNumber = DecalateFromPosition(newNumber, currentBattery[j], positionToDecalate.Value);
                    }
                }

                totalJoltage += long.Parse(new string(newNumber));
            }

            _comunicationService.WritePuzzlePartOneText(totalJoltage.ToString(), true);
        }

        private int? GetDecalationPosition(char[] currentElements, char newElement)
        {
            for(int i = 1; i < currentElements.Length; i++)
            {
                if(currentElements[i-1] < currentElements[i])
                {
                    return i-1;
                }
            }

            if (newElement > currentElements[currentElements.Length - 1])
            {
                return currentElements.Length - 1;
            }

            return null;
        }

        private char[] DecalateFromPosition(char[] currentElements, char newElement, int position)
        {
            for (int i = position; i < currentElements.Length - 1; i++)
            {
                currentElements[i] = currentElements[i + 1];
            }

            currentElements[currentElements.Length - 1] = newElement;

            return currentElements;
        }
    }
}