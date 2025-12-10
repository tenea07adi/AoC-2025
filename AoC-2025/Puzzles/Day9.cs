using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day9 : PuzzleTemplate
    {
        private const char _redSymbol = '#';
        private const char _greenSymbol = 'X';

        public Day9(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day9)
        {

        }

        protected override void RunPartOne(string data)
        {
            var coordonates = ExtractCoordonates(data);

            long largestArea = 0;

            for (int i = 0; i < coordonates.Length; i++)
            {
                for (int j = i + 1; j < coordonates.Length; j++)
                {
                    var area = CalculateArea(coordonates[i], coordonates[j]);

                    if(area > largestArea)
                    {
                        largestArea = area;
                    }
                }
            }

            _comunicationService.WritePuzzlePartOneText(largestArea.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var coordonates = ExtractCoordonates(data);

            long largestArea = 0;

            var colorsMatrix = CreateColorsMatrix(coordonates);

            for (int i = 0; i < coordonates.Length; i++)
            {
                for (int j = i + 1; j < coordonates.Length; j++)
                {
                    var area = CalculateArea(coordonates[i], coordonates[j]);

                    if (area <= largestArea)
                    {
                        continue;
                    }

                    if(!FullGreen(colorsMatrix, coordonates, coordonates[i], coordonates[j]))
                    {
                        continue;
                    }

                    largestArea = area;
                }
            }

            _comunicationService.WritePuzzlePartOneText(largestArea.ToString(), true);
        }

        #region part two
        private bool FullGreen(char[,] matrix, long[][] reds, long[] pointA, long[] pointB)
        {
            
            long minL = -1;
            long maxL = -1;
            long minC = -1;
            long maxC = -1;

            if (pointA[0] < pointB[0])
            {
                minC = pointA[0];
                maxC = pointB[0];
            }
            else
            {
                minC = pointB[0];
                maxC = pointA[0];
            }

            if (pointA[1] < pointB[1])
            {
                minL = pointA[1];
                maxL = pointB[1];
            }
            else
            {
                minL = pointB[1];
                maxL = pointA[1];
            }
            

            for(long i = minL; i < maxL; i++)
            {
                for (long j = minC; j < maxC; j++)
                {
                    if(matrix[i, j] != _greenSymbol && matrix[i,j] != _redSymbol)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private char[,] CreateColorsMatrix(long[][] reds)
        {
            // boundaries for next iterations
            long minRedLine = -1;
            long minRedCol = -1;

            long maxRedLine = -1;
            long maxRedCol = -1;

            for (int i = 0; i < reds.Length; i++)
            {
                if (reds[i][1] < minRedLine)
                {
                    minRedLine = reds[i][1];
                }

                if (reds[i][0] < minRedCol)
                {
                    minRedCol = reds[i][0];
                }

                if (reds[i][1] > maxRedLine)
                {
                    maxRedLine = reds[i][1];
                }

                if (reds[i][0] > maxRedCol)
                {
                    maxRedCol = reds[i][0];
                }
            }

            var matrix = new char[maxRedLine+1,maxRedCol+1];

            matrix = DrowGreenConnectionFromRed(matrix, reds);

            for (long i = minRedLine + 1; i < maxRedLine; i++)
            {
                for (long j = minRedCol + 1; j < maxRedCol; j++)
                {
                    matrix = DrowIfIsInShape(matrix, i, j);
                }
            }

            return matrix;
        }

        private char[,] DrowIfIsInShape(char[,] colorsMatrix, long line, long col)
        {
            long leftFountElm = -1;
            long rightFountElm = -1;
            long upFountElm = -1;
            long downFountElm = -1;

            for (long i = line + 1; i < colorsMatrix.GetLength(0); i++)
            {
                if (colorsMatrix[i,col] == _redSymbol || colorsMatrix[i,col] == _greenSymbol)
                {
                    downFountElm = i;
                    break;
                }
            }

            if(downFountElm == -1)
            {
                return colorsMatrix;
            }

            for (long i = line - 1; i >= 0; i--)
            {
                if (colorsMatrix[i, col] == _redSymbol || colorsMatrix[i, col] == _greenSymbol)
                {
                    upFountElm = i;
                    break;
                }
            }

            if (upFountElm == -1)
            {
                return colorsMatrix;
            }

            for (long i = col + 1; i < colorsMatrix.GetLength(1); i++)
            {
                if (colorsMatrix[line, i] == _redSymbol || colorsMatrix[line, i] == _greenSymbol)
                {
                    rightFountElm = i;
                    break;
                }
            }

            if (rightFountElm == -1)
            {
                return colorsMatrix;
            }

            for (long i = col - 1; i >= 0; i--)
            {
                if (colorsMatrix[line, i] == _redSymbol || colorsMatrix[line, i] == _greenSymbol)
                {
                    leftFountElm = i;
                    break;
                }
            }

            if (leftFountElm == -1)
            {
                return colorsMatrix;
            }

            DrowGreen(colorsMatrix, downFountElm, upFountElm, false, col);
            DrowGreen(colorsMatrix, leftFountElm, rightFountElm, true, line);

            return colorsMatrix;
        }

        private char[,] DrowGreenConnectionFromRed(char[,] matrix, long[][] reds)
        {
            for (int i = 0; i < reds.Length; i++)
            {
                matrix[reds[i][1], reds[i][0]] = _redSymbol;
                for (int j = i+1; j < reds.Length; j++)
                {
                    if (reds[i][0] == reds[j][0])
                    {
                        DrowGreen(matrix, reds[i][1], reds[j][1], false, reds[i][0]);
                    }

                    if (reds[i][1] == reds[j][1])
                    {
                        DrowGreen(matrix, reds[i][0], reds[j][0], true, reds[i][1]);
                    }
                }
            }
            
            return matrix;
        }

        private char[,] DrowGreen(char[,] matrix, long a, long b, bool isCol, long fixedElement)
        {
            long min = 0;
            long max = 0;

            if(a < b)
            {
                min = a;
                max = b;
            }
            else
            {
                min = b;
                max = a;
            }


            for (long i = min; i < max; i++)
            {
                if (isCol)
                {
                    if(matrix[fixedElement, i] == _redSymbol)
                    {
                        continue;
                    }
                    matrix[fixedElement, i] = _greenSymbol;
                }
                else
                {
                    if (matrix[i, fixedElement] == _redSymbol)
                    {
                        continue;
                    }

                    matrix[i, fixedElement] = _greenSymbol;
                }
            }

            return matrix;
        }
        #endregion

        #region part one
        private long[][] ExtractCoordonates(string data)
        {
            var lines = data.Split(FileConstants.EndOfLine);

            var coordonates = new long[lines.Length][];

            for(int i = 0; i < lines.Length; i++)
            {
                var lineStrings = lines[i].Split(FileConstants.CommaSeparator);
                var line = new long[lineStrings.Length];

                for (int j = 0; j < lineStrings.Length; j++)
                {
                    line[j] = long.Parse(lineStrings[j]);
                }

                coordonates[i] = line;
            }

            return coordonates;
        }

        private long CalculateArea(long[] pointA, long[] pointB)
        {
            long area = 0;

            var l1 = pointA[0] > pointB[0] ? pointA[0] - pointB[0] : pointB[0] - pointA[0];
            l1++;

            var l2 = pointA[1] > pointB[1] ? pointA[1] - pointB[1] : pointB[1] - pointA[1];
            l2++;

            area = l1 * l2;

            return area;
        }
        #endregion
    }
}