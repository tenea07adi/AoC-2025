namespace AoC_2025.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static IEnumerable<string> SplitInChunks(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
