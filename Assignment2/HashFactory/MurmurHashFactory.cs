namespace Assignment2.HashFactory
{
    using System.Data.HashFunction.MurmurHash;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class MurmurHashFactory
    {
        /// <summary>
        /// Generates the given amount of hash functions.
        /// </summary>
        /// <typeparam name="T">The type of element to hash.</typeparam>
        /// <param name="amount">The amount of functions to generate.</param>
        /// <returns>A set of hash functions.</returns>
        public static IEnumerable<Func<T, int>> GenerateFunctions<T>(int amount)
        {
            var fns = new Func<T, int>[amount];

            for (int i = 0; i < amount; i++)
            {
                int seed = i; // Need to copy for anonymous function to ensure different seeds.
                fns[i] = (e) => MurmurHashFactory.Hash<T>(e, (uint) seed);       
            }

            return fns;
        }

        /// <summary>
        /// Hashes the given element with the given seed (Murmur3 hashing).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element to hash.</param>
        /// <param name="seed">The seed of the hash function.</param>
        /// <returns>The integer after mapping of the hash function.</returns>
        private static int Hash<T>(T element, uint seed)
        {
            var factory = MurmurHash3Factory.Instance;

            var murmur = factory.Create(new MurmurHash3Config { Seed = seed });
            var hash = murmur.ComputeHash(ObjectToByteArray(element)).Hash;

            // Ensure positive value with absolute value.
            return Math.Abs(BitConverter.ToInt32(hash));
        }

        /// <summary>
        /// Converts a given object to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of element to convert.</typeparam>
        /// <param name="obj">The element to convert.</param>
        /// <returns>The byte array after conversion.</returns>
        private static byte[] ObjectToByteArray<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
