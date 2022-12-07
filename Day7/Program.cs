// 1. Representing an ElfFile
// 2. Write Tests for an ElfFile
// 3. Write Implementation for Parsing an ElfFile

// 4. Represent ElfDirectory
// 5. Write Tests / Write implementation for ElfDirectory
//    a. Parse and IsParseable
//    b. AddFile and Size
//    c. TestExample and Size

// 6. Parse input and build File System
//   - ProcessFileLine
//   - ProcessDirectoryLine
//   - ProcessChangeDirLine 
//   - BuildFileSystem

// 7. FindAllDirectories
// 8. Iterate over all Directories
//    - Sum directories that are <= 100_000 in size
RunTests();
SolvePart1("example.txt");

bool RunTests()
{
    bool pass = ElfFile.RunTests();
    pass &= ElfDirectory.RunTests();
    pass &= TestProcessDirectoryLine();
    pass &= TestProcessFileLine();
    pass &= TestProcessChangeDir();
    pass &= TestBuildFileSystem();
    pass &= TestFindAllDirectories();
    if (pass)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All Tests Pass!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("At least one test is failing!");
    }
    Console.ResetColor();
    return pass;
}

void SolvePart1(string filename)
{
    ElfDirectory root = BuildFileSystem(File.ReadAllLines(filename));
    List<ElfDirectory> dirs = FindAllDirectories(root);
    int sum = 0;
    foreach (ElfDirectory dir in dirs)
    {
        int size = dir.Size();
        if (size <= 100_000)
        {
            Console.WriteLine($"Found directory: {dir}");
            sum += size;
        }
    }
    Console.WriteLine($"The sum of the directories is {sum}");
}

List<ElfDirectory> FindAllDirectories(ElfDirectory toSearch)
{
    List<ElfDirectory> all = new ();
    all.Add(toSearch);
    foreach (ElfDirectory child in toSearch.Children())
    {
        all.AddRange(FindAllDirectories(child));
    }
    return all;
}

bool TestFindAllDirectories()
{
    bool pass = true;
    Console.WriteLine("TestFindAllDirectories:");
    ElfDirectory root = new ElfDirectory("/", null!);
    ElfDirectory a = new ElfDirectory("a", root);
    ElfDirectory b = new ElfDirectory("b", a);
    ElfDirectory c = new ElfDirectory("c", root);
    ElfDirectory d = new ElfDirectory("d", c);


    List<ElfDirectory> all = FindAllDirectories(root);
    pass &= Test.Assert(all.Count == 5, $"  There should be 4 directories but found {all.Count}");

    pass &= Test.Assert(all.Contains(a), $"  'a' was not found.");
    pass &= Test.Assert(all.Contains(b), $"  'b' was not  found.");
    pass &= Test.Assert(all.Contains(c), $"  'c' was not  found.");
    pass &= Test.Assert(all.Contains(d), $"  'd' was not  found.");

    if (pass)
    {
        Console.WriteLine("TestFindAllDirectories Passed!");
    }
    return pass;
}

ElfDirectory BuildFileSystem(string[] rows)
{
    ElfDirectory root = new ElfDirectory("/", null!);
    ElfDirectory currentDir = root;
    foreach (string input in rows)
    {
        if (ElfFile.IsParseable(input))
        {
            currentDir = ProcessFileLine(currentDir, input);
        }

        if (ElfDirectory.IsParseable(input))
        {
            currentDir = ProcessDirectoryLine(currentDir, input);
        }

        if (input.StartsWith("$ cd"))
        {
            currentDir = ProcessChangeDirectory(currentDir, input);
        }
    }
    return root;
}

bool TestBuildFileSystem()
{
    bool pass = true;
    Console.WriteLine("TestBuildFileSystem:");
    string[] rows = File.ReadAllLines("example.txt");
    ElfDirectory root = BuildFileSystem(rows);

    ElfDirectory? a = root.FindChild("a");
    pass &= Test.Assert(a != null, $"  Expected root to have 'a' as child but it did not.");

    ElfDirectory? e = a?.FindChild("e");
    pass &= Test.Assert(e != null, $"  Expected 'a' to have 'e' as child but it did not.");

    List<ElfFile>? files = e?.Files();
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("i", 584)), $"  Expected 'e' to have file 'i' but it did not.");

    files = a?.Files();
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("f", 29116)), $"  Expected 'a' to have file 'f' but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("g", 2557)), $"  Expected 'a' to have file 'g' but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("h.lst", 62596)), $"  Expected 'a' to have file 'h.lst' but it did not.");

    files = root.Files();
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("b.txt", 14848514)), $"  Expected root to have b.txt but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("c.dat", 8504156)), $"  Expected root to have c.dat but it did not.");

    ElfDirectory? d = root.FindChild("d");
    pass &= Test.Assert(d != null, $"  Expected root to have 'd' as child but it did not.");

    files = d?.Files();
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("j", 4060174)), $"  Expected 'd' to have file 'j' but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("d.log", 8033020)), $"  Expected 'd' to have file 'd.log' but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("d.ext", 5626152)), $"  Expected 'd' to have file 'd.ext' but it did not.");
    pass &= Test.Assert(files != null && files.Contains(new ElfFile("k", 7214296)), $"  Expected 'd' to have file 'k' but it did not.");

    if (pass)
    {
        Console.WriteLine("TestBuildFileSystem Passed!");
    }
    return pass;
}

ElfDirectory ProcessChangeDirectory(ElfDirectory currentDir, string input)
{
    string childName = input.Split()[2];
    return childName switch
    {
        ".." => currentDir.Parent,
        "/" => currentDir, // Should only occur for root
        _ => currentDir.FindChild(childName)!,
    };
}

bool TestProcessChangeDir()
{
    bool pass = true;
    Console.WriteLine("TestProcessChangeDir");
    ElfDirectory root = new ElfDirectory("/", null!);
    ElfDirectory a = new ElfDirectory("a", root);
    ElfDirectory b = new ElfDirectory("b", a);
    ElfDirectory result = ProcessChangeDirectory(root, "$ cd /");
    pass &= Test.Assert(result == root, $"  Test Failed: 'cd /' should have no effect.");
    result = ProcessChangeDirectory(root, "$ cd a");
    pass &= Test.Assert(result == a, $"  Test Failed: 'cd a' should change to 'a' directory.");
    result = ProcessChangeDirectory(a, "$ cd b");
    pass &= Test.Assert(result == b, $"  Test Failed: 'cd b' should change to 'b' directory.");
    result = ProcessChangeDirectory(b, "$ cd ..");
    pass &= Test.Assert(result == a, $"  Test Failed: 'cd ..' should change to 'a' directory.");
    result = ProcessChangeDirectory(a, "$ cd ..");
    pass &= Test.Assert(result == root, $"  Test Failed: 'cd ..' should change to 'root' directory.");

    if (pass)
    {
        Console.WriteLine("TestProcessChangeDir Passed!");
    }
    return pass;
}

ElfDirectory ProcessFileLine(ElfDirectory currentDir, string input)
{
    ElfFile file = ElfFile.Parse(input);
    currentDir.AddFile(file);
    return currentDir;
}

bool TestProcessFileLine()
{
    bool pass = true;
    Console.WriteLine("TestProcessFileLine");
    ElfDirectory root = new ElfDirectory("/", null!);
    ElfDirectory result = ProcessFileLine(root, "500 foo.txt");
    pass &= Test.Assert(result == root, $"  Expected no change in directory.");
    pass &= Test.Assert(root.Files().Contains(new ElfFile("foo.txt", 500)), $"  Expected root to contain foo.txt but it did not.");

    ElfDirectory a = new ElfDirectory("a", root);
    result = ProcessFileLine(a, "200 bob.txt");
    pass &= Test.Assert(result == a, $"  Expected no change in directory.");
    pass &= Test.Assert(a.Files().Contains(new ElfFile("bob.txt", 200)), $"  Expected a to contain bob.txt but it did not.");

    if (pass)
    {
        Console.WriteLine("TestProcessFileLine Passed!");
    }
    return pass;
}

ElfDirectory ProcessDirectoryLine(ElfDirectory currentDir, string input)
{
    ElfDirectory.Parse(input, currentDir);
    return currentDir;
}

bool TestProcessDirectoryLine()
{
    bool pass = true;
    Console.WriteLine("TestProcessDirectoryLine");
    ElfDirectory root = new ElfDirectory("/", null!);
    ElfDirectory result = ProcessDirectoryLine(root, "dir a");
    pass &= Test.Assert(result == root, $"  Expected no change in directory.");

    ElfDirectory? a = root.FindChild("a");
    pass &= Test.Assert(a != null && a.Name == "a", $"  Expected 'a' to have Name 'a' but was {a?.Name}");
    pass &= Test.Assert(a != null && a.Parent == root, $"  Expected 'a' to have parent {root} but was {a?.Parent}");

    result = ProcessDirectoryLine(a, "dir b");
    pass &= Test.Assert(result == a, $"  Expected no change in directory.");

    ElfDirectory? b = a?.FindChild("b");
    pass &= Test.Assert(b != null && b.Name == "b", $"  Expected 'b' to have Name 'b' but was {b?.Name}");
    pass &= Test.Assert(b != null && b.Parent == a, $"  Expected 'b' to have parent {a} but was {b?.Parent}");
    if (pass)
    {
        Console.WriteLine("TestProcessDirectoryLine Passed!");
    }
    return pass;
}