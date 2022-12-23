namespace Assignment2.BloomFilter
{
    using Assignment2.HashFactory;
    
    /// <summary>
    /// Implementation of the <see href="https://jeffe.cs.illinois.edu/teaching/algorithms/notes/06-bloom.pdf">bloom filter data structure</see>.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    public class BloomFilter<T>
    {
        /// <summary>
        /// The array containing the bits of the data structure.
        /// </summary>
        public bool[] BitArray { get; private set; }

        /// <summary>
        /// The number of hash functions.
        /// </summary>
        public int NumberOfHashFunctions { get; private set; }
        
        /// <summary>
        /// The number of elements N.
        /// </summary>
        public int NumberElements { get; private set; }

        /// <summary>
        /// Gives the expected 
        /// </summary>
        public double ExpectedRate
        {
            get
            {
                var exponent = -(this.NumberElements * this.NumberOfHashFunctions) / (double)this.BitArray.Length;
                var p = Math.Pow(Math.E, exponent);
                return Math.Pow((1 - p), this.NumberOfHashFunctions);
            }
        }

        /// <summary>
        /// The hash functions.
        /// </summary>
        private IEnumerable<Func<T, int>> hashFunctions;

        /// <summary>
        /// Creates an instance of the "Bloom Filter" data structure.
        /// </summary>
        /// <param name="m">The size of the bit array.</param>
        /// <param name="k">The number of hash functions that will be used.</param>
        public BloomFilter(int m, int k)
        {
            if (m < 1 || k < 1)
            {
                throw new ArgumentException("Values of k and m should be stricly positive");
            }

            this.NumberElements++;

            this.BitArray = new bool[m];
            this.hashFunctions = new Func<T, int>[k];

            this.NumberOfHashFunctions = k;
            this.hashFunctions = MurmurHashFactory.GenerateFunctions<T>(m, k);

            this.Initialize();
        }

        /// <summary>
        /// Checks whether an element is member in the set or not. There is a probability of false positive.
        /// </summary>
        /// <param name="element">The element whose membership is to check.</param>
        /// <returns>True if the element is present, false otherwise.</returns>
        public bool CheckMembershipOf(T element)
        {
            if (element is null)
            {
                throw new ArgumentNullException("The element to check may not be null");
            }

            foreach (var hashFunction in this.hashFunctions)
            {
                var hashed = hashFunction(element);

                if (!this.BitArray[hashed])
                {
                    return false;
                }
            }

            // Could be a false positive !
            return true;
        }

        /// <summary>
        /// Adds an element to the data structure.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(T element)
        {
            for (int i = 0; i < this.NumberOfHashFunctions; i++)
            {
                var hashed = this.hashFunctions.ElementAt(i)(element);
                this.BitArray[hashed] = true;
            }

            this.NumberElements++;
        }

        /// <summary>
        /// Adds several elements to the data structure.
        /// </summary>
        /// <param name="elements">The set of elements to add.</param>
        public void AddRange(IEnumerable<T> elements)
        {
            foreach (var elem in elements)
            {
                this.Add(elem);
            }
        }

        /// <summary>
        /// Initializes the structure of bits.
        /// </summary>
        private void Initialize()
        {
            for (int i = 0; i < this.BitArray.Length; i++)
            {
                this.BitArray[i] = false;
            }
        }
    }
}
