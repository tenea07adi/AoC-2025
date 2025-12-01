using AoC_2025.Enums;
using AoC_2025.Puzzles.Base;

namespace AoC_2025.Abstractions
{
    public interface IPuzzleFactory
    {
        public PuzzleTemplate? Create(PuzzleIdentifier identifier);
    }
}
