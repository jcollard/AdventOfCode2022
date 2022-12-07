public record ElfFile(string Name, int Size)
{
    public static ElfFile Parse(string input)
    {
        // Initial Test
        // return new ElfFile(string.Empty, 0);
        string[] tokens = input.Split();
        return new ElfFile(tokens[1], int.Parse(tokens[0]));
    }

    public static bool IsParseable(string input)
    {
        // Initial Test
        // return false;
        string[] tokens = input.Split();
        return int.TryParse(tokens[0], out int _);
    }

    public static bool RunTests()
    {
        bool result = TestIsParseable();
        result &= TestParse();
        return result;
    }

    private static bool TestParse()
    {
        bool pass = true;
        string testInput = "14848514 b.txt";
        ElfFile result = ElfFile.Parse(testInput);
        ElfFile expected = new ElfFile("b.txt", 14848514);
        Console.WriteLine($"ElfFile.ParseElfFile({testInput}) => {result}");
        pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        if (pass)
        {
            Console.WriteLine($"ElfFile.TestParseElfFile: Pass!");
        }
        return pass;
    }

    private static bool TestIsParseable()
    {
        bool pass = true;
        {
            string testInput = "14848514 b.txt";
            bool result = ElfFile.IsParseable(testInput);
            bool expected = true;
            Console.WriteLine($"ElfFile.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        {
            string testInput = "$ cd /";
            bool result = ElfFile.IsParseable(testInput);
            bool expected = false;
            Console.WriteLine($"ElfFile.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        {
            string testInput = "dir a";
            bool result = ElfFile.IsParseable(testInput);
            bool expected = false;
            Console.WriteLine($"ElfFile.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        if (pass)
        {
            Console.WriteLine($"ElfFile.TestIsParseable: Pass!");
        }

        return pass;
    }
}