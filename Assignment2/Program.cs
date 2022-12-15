using Assignment2.BloomFilter;

const int testSetSize = 150;
const int numberOfTestsForFalsePositive = 500;

bool verbose = false;

HandleArguments();

void HandleArguments()
{
    var args = Environment.GetCommandLineArgs();
    
    if (args.Length < 4)
    {
        throw new InvalidOperationException("There must be 3 arguments to use this program");
    }

    int k, m, n;

    bool success = int.TryParse(args[1], out k);
    success = int.TryParse(args[2], out m);
    success = int.TryParse(args[3], out n);

    if (!success)
    {
        throw new ArgumentException("Provided arguments are not valid.");
    }

    if (args.Length > 4 && args[4] == "--verbose")
    {
        verbose = true;
    }

    Console.WriteLine($"Testing for k = {k}, m = {m}, n = {n}...");
    
    if (verbose)
    {
        Console.WriteLine($"There will be {numberOfTestsForFalsePositive} iterations with {testSetSize} elements tested per iteration");
    }

    Console.Write("\n");

    Testing(k, m, n);
}

void Testing(int k, int m, int n)
{
    int falsePositives = 0;

    for (int nbTest = 1; nbTest <= numberOfTestsForFalsePositive; nbTest++)
    {
        int currentTestFalsePositives = 0;

        var bloomFilter = new BloomFilter<int>(m, k);
        var elementsInStructure = GenerateRandomNumbers(n);
        var elementsOutStructure = GenerateRandomNumbers(n: testSetSize, exclusionList: elementsInStructure);
        bloomFilter.AddRange(elementsInStructure); // Creating structure with some elements in it.

        foreach (var element in elementsOutStructure)
        {
            if (bloomFilter.CheckMembershipOf(element))
            {
                currentTestFalsePositives++;
            }
        }

        if (verbose)
        {
            Console.WriteLine($"Test {nbTest} : There are {currentTestFalsePositives} false positives.");
        }

        falsePositives += currentTestFalsePositives;
    }

    // There are "numberOfTestsForFalsePositive" tests, where each consists of "testSetSize" test elements.
    // Probability is (falsePositives / (numberOfTestsForFalsePositive * testSetSize).
    float rate = (float)falsePositives / (numberOfTestsForFalsePositive * testSetSize);
    Console.WriteLine($"There are {falsePositives} false positives for  tests");
    Console.WriteLine($"The false positive rate is : {rate}");
}

static IEnumerable<int> GenerateRandomNumbers(int n, IEnumerable<int> exclusionList = null)
{
    var random = new Random();
    var elements = new List<int>(n);

    for (int i = 0; i < n; i++)
    {
        int element;

        do
        {
            element = random.Next();
        } while (exclusionList is not null && exclusionList.Contains(element));

        elements.Add(element);
    }

    return elements;
}

