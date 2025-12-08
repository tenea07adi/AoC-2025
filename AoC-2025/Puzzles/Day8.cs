using AoC_2025.Abstractions;
using AoC_2025.Constants;
using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Puzzles
{
    public class Day8 : PuzzleTemplate
    {
        private long latestA = -1;
        private long latestB = -1;

        public Day8(IComunicationService comunicationService, IDataReaderService dataReaderService)
            : base(comunicationService, dataReaderService, PuzzleIdentifier.Day8)
        {

        }

        protected override void RunPartOne(string data)
        {
            int maxConnections = 10;
            int maxMultiply = 3;

            var locations = ExtractCircuitsLocations(data);

            long?[] existingCircuits = new long?[locations.Length];

            var distancesMatrix = CreateDistancesMatrix(locations);

            existingCircuits = GetConnections(distancesMatrix, existingCircuits, 1, maxConnections);

            var total = MultiplyCircuitsElementsCount(existingCircuits, maxMultiply);

            _comunicationService.WritePuzzlePartOneText(total.ToString(), true);
        }

        protected override void RunPartTwo(string data)
        {
            var locations = ExtractCircuitsLocations(data);

            long?[] existingCircuits = new long?[locations.Length];

            var distancesMatrix = CreateDistancesMatrix(locations);

            var stillWork = true;

            while (stillWork)
            {
                var result = GetConnectionsLiniarForPart2(distancesMatrix, existingCircuits);
                stillWork = result.stillWorking;
                existingCircuits = result.circuits;
            }

            var total = locations[latestA][0] * locations[latestB][0];

            _comunicationService.WritePuzzlePartOneText(total.ToString(), true);
        }

        // This method works for part 2, but since it is recurvie, it run into a stack overflow issue with the full set of data
        // Send maxConnections = 2 to use it for part
        private long?[] GetConnections(long[,] distancesMatrix, long?[] existingCircuits, int connectionCount, int maxConnections)
        {
            if(maxConnections == -1 && latestA != -1)
            {
                return existingCircuits;
            }

            if(maxConnections != -1 && connectionCount > maxConnections)
            {
                return existingCircuits;
            }

            var closestDistanceLine = -1;
            var closestDistanceRow = -1;

            for (int i = 0; i < distancesMatrix.GetLength(0); i++)
            {
                for (int j = i+1; j < distancesMatrix.GetLength(1); j++)
                {
                    if (distancesMatrix[i,j] == -1)
                    {
                        continue;
                    }

                    if(closestDistanceLine == -1 || distancesMatrix[i, j] < distancesMatrix[closestDistanceLine, closestDistanceRow])
                    {
                        closestDistanceLine = i;
                        closestDistanceRow = j;
                    }
                }
            }

            if(closestDistanceLine != -1)
            {
                existingCircuits = RegisterCircuit(existingCircuits, closestDistanceLine, closestDistanceRow);

                distancesMatrix[closestDistanceLine, closestDistanceRow] = -1;
                distancesMatrix[closestDistanceRow, closestDistanceLine] = -1;

                connectionCount++;

                existingCircuits = GetConnections(distancesMatrix, existingCircuits, connectionCount, maxConnections);
            }

            return existingCircuits;
        }

        private (long?[] circuits, bool stillWorking)  GetConnectionsLiniarForPart2(long[,] distancesMatrix, long?[] existingCircuits)
        {
            if (latestA != -1)
            {
                return (existingCircuits, false);
            }

            var closestDistanceLine = -1;
            var closestDistanceRow = -1;

            for (int i = 0; i < distancesMatrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < distancesMatrix.GetLength(1); j++)
                {
                    if (distancesMatrix[i, j] == -1)
                    {
                        continue;
                    }

                    if (closestDistanceLine == -1 || distancesMatrix[i, j] < distancesMatrix[closestDistanceLine, closestDistanceRow])
                    {
                        closestDistanceLine = i;
                        closestDistanceRow = j;
                    }
                }
            }

            if (closestDistanceLine != -1)
            {
                existingCircuits = RegisterCircuit(existingCircuits, closestDistanceLine, closestDistanceRow);

                distancesMatrix[closestDistanceLine, closestDistanceRow] = -1;
                distancesMatrix[closestDistanceRow, closestDistanceLine] = -1;

                return (existingCircuits, true);
            }

            return (existingCircuits, false);
        }

        private long[,] CreateDistancesMatrix(long[][] locations)
        {
            long[,] matrix = new long[locations.Length, locations.Length];

            for (int i = 0; i < locations.Length; i++)
            {
                for (int j = 0; j < locations.Length; j++)
                {
                    if(i == j)
                    {
                        matrix[i, j] = -1;
                        continue;
                    }

                    matrix[i,j] = CalculateDistance(locations[i], locations[j]);
                }
            }

            return matrix;
        }

        private long?[] RegisterCircuit(long?[] circuit, int linkLocation, int newElementLocation)
        {
            if (circuit[linkLocation] == null)
            {
                circuit[linkLocation] = linkLocation;
            }

            if(circuit[newElementLocation] == null)
            {
                circuit[newElementLocation] = circuit[linkLocation];
            }
            else
            {
                ReplaceCircuit(circuit, circuit[newElementLocation]!.Value, circuit[linkLocation]!.Value);
            }

            CollectFirstFullCircuitData(circuit, linkLocation, newElementLocation);

            return circuit;
        }

        private long?[] ReplaceCircuit(long?[] circuit, long oldId, long newId)
        {
            for(int i = 0; i < circuit.Length; i++)
            {
                if (circuit[i] == oldId)
                {
                    circuit[i] = newId;
                }
            }

            return circuit;
        }

        private long MultiplyCircuitsElementsCount(long?[] circuit, int numberOfElementsToMultiply)
        {
            Dictionary<long, long> counts = new Dictionary<long, long>();

            for(int i = 0; i < circuit.Length; i++)
            {
                if(circuit[i] == null)
                {
                    continue;
                }

                if (counts.ContainsKey(circuit[i]!.Value))
                {
                    counts[circuit[i]!.Value] = counts[circuit[i]!.Value] + 1;
                }
                else
                {
                    counts[circuit[i]!.Value] = 1;
                }
            }

            long total = 1;

            long count = 0;

            var orderedCounts = counts.ToArray().OrderByDescending(c => c.Value);

            foreach (KeyValuePair<long, long> entry in orderedCounts)
            {
                if(count >= numberOfElementsToMultiply)
                {
                    break;
                }

                total = total * entry.Value;

                count++;
            }

            return total;
        }

        private void CollectFirstFullCircuitData(long?[] circuit, int linkLocation, int newElementLocation)
        {
            if(latestA != -1)
            {
                return;
            }

            bool singleCircuit = true;

            for(int i = 0; i < circuit.Length - 1; i++)
            {
                if (circuit[i] != circuit[i+1])
                {
                    singleCircuit = false;
                    break;
                }
            }

            if (singleCircuit)
            {
                latestA = linkLocation;
                latestB = newElementLocation;
            }
        }

        private long[][] ExtractCircuitsLocations(string data)
        {
            var lines = data.Split(FileConstants.EndOfLine).ToArray();

            long[][] locations = new long[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                var stringLocation = lines[i].Split(FileConstants.CommaSeparator);
                var loc = new long[stringLocation.Length];

                for(int j = 0; j < stringLocation.Length; j++)
                {
                    loc[j] = int.Parse(stringLocation[j]);
                }
                locations[i] = loc;
            }

            return locations;
        }
    
        private long CalculateDistance(long[] pointA, long[] pointB)
        {
            double distance3D = 0;

            for (int j = 0; j < pointA.Length; j++)
            {
                var axl = pointA[j] - pointB[j];
                axl = axl * axl;

                distance3D += axl;
            }

            distance3D = Math.Sqrt(distance3D);

            return (long)distance3D;
        }
    }
}