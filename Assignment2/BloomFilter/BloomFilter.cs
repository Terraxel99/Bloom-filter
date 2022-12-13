namespace Assignment2.BloomFilter
{
    public class BloomFilter
    {
        /// <summary>
        /// The array containing the bits of the data structure.
        /// </summary>
        public bool[] BitArray { get; private set; }

        /// <summary>
        /// Creates an instance of the "Bloom Filter" data structure.
        /// </summary>
        /// <param name="m">The size of the bit array.</param>
        /// <param name="k">The number of hash functions that will be used.</param>
        public BloomFilter(int m, int k)
        {
            this.BitArray = new bool[m];
        }
    }
}
