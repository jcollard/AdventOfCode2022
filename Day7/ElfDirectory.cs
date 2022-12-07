
public record ElfDirectory
{
    public string Name { get; }
    public ElfDirectory Parent { get; }
    private List<ElfFile> _files = new ();
    private List<ElfDirectory> _children = new ();

    public void AddFile(ElfFile file)
    {
        this._files.Add(file);
    }

    public ElfDirectory(string name, ElfDirectory parent)
    {
        this.Name = name;
        this.Parent = parent;
        if (parent != null)
        {
            parent._children.Add(this);
        }
    }

    public int Size()
    {
        int size = 0;
        foreach (ElfFile file in this._files)
        {
            size += file.Size;
        }

        foreach (ElfDirectory child in this._children)
        {
            size += child.Size();
        }

        return size;
    }

    public static ElfDirectory Parse(string input, ElfDirectory parent)
    {
        // Pre Test
        // return new ElfDirectory(string.Empty, null);
        string[] tokens = input.Split();
        return new ElfDirectory(tokens[1], parent);
    }

    public static bool IsParseable(string input)
    {
        // Pre Test
        // return false;
        return input.StartsWith("dir");
    }

    public static bool RunTests()
    {
        bool pass = TestParse();
        pass &= TestIsParseable();
        pass &= TestAddFile();
        pass &= TestExample();
        return pass;
    }

    private static bool TestParse()
    {
        bool pass = true;
        ElfDirectory root = new ("/", null!);
        
        string testInput = "dir a";
        ElfDirectory a = ElfDirectory.Parse(testInput, root);
        ElfDirectory expected = new ElfDirectory("a", root);
        Console.WriteLine($"ElfDirectory.Parse({testInput}, {root}) => {a}");
        pass &= Test.Assert(a.Name == expected.Name && a.Parent == expected.Parent, $"  Result ({a}) did not match expected ({expected})!");
        pass &= Test.Assert(root._children.Contains(a), $"  Root should contain child ({a}) but did not!");

        testInput = "dir b";
        ElfDirectory b = ElfDirectory.Parse(testInput, a);
        expected = new ElfDirectory("b", a);
        Console.WriteLine($"ElfDirectory.Parse({testInput}, {root}) => {b}");
        pass &= Test.Assert(b.Name == expected.Name && b.Parent == expected.Parent, $"  Result ({b}) did not match expected ({expected})!");
        pass &= Test.Assert(a._children.Contains(b), $"  Directory 'a' should contain child ({b}) but did not!");
        
        if (pass)
        {
            Console.WriteLine($"ElfDirectory.TestParse: Pass!");
        }
        return pass;
    }

    private static bool TestIsParseable()
    {
        bool pass = true;
        {
            string testInput = "14848514 b.txt";
            bool result = ElfDirectory.IsParseable(testInput);
            bool expected = false;
            Console.WriteLine($"ElfDirectory.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        {
            string testInput = "$ cd /";
            bool result = ElfDirectory.IsParseable(testInput);
            bool expected = false;
            Console.WriteLine($"ElfDirectory.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        {
            string testInput = "dir a";
            bool result = ElfDirectory.IsParseable(testInput);
            bool expected = true;
            Console.WriteLine($"ElfDirectory.IsParseable({testInput}) => {result}");
            pass &= Test.Assert(result == expected, $"  Result ({result}) did not match expected ({expected})!");
        }

        if (pass)
        {
            Console.WriteLine($"ElfDirectory.TestIsParseable: Pass!");
        }

        return pass;
    }

    public static bool TestAddFile()
    {
        bool pass = true;
        ElfDirectory root = new ("/", null!);
        pass &= Test.Assert(root.Size() == 0, $"  Expected root to start at size 0 but was {root.Size()}");
        
        ElfFile foo = new ElfFile("foo.txt", 500);
        Console.WriteLine($"Testing root.AddFile({foo})");
        root.AddFile(foo);
        pass &= Test.Assert(root._files.Contains(foo), "  Expected root to contain foo but it did not.");
        pass &= Test.Assert(root.Size() == 500, $"  Expected root to have a size of 500 but was {root.Size()}");

        ElfFile bar = new ElfFile("bar.txt", 300);
        Console.WriteLine($"Testing root.AddFile({bar})");
        root.AddFile(bar);
        pass &= Test.Assert(root._files.Contains(foo), "  Expected root to contain foo but it did not.");
        pass &= Test.Assert(root._files.Contains(bar), "  Expected root to contain bar but it did not.");
        pass &= Test.Assert(root.Size() == 800, $"  Expected root to start at size of 800 but was {root.Size()}");
        
        if (pass)
        {
            Console.WriteLine($"ElfDirectory.TestAddFile: Pass!");
        }
        return pass;
    }

    public static bool TestExample()
    {
        bool pass = true;
        ElfDirectory root = new ("/", null!);
        
        ElfDirectory a = new ElfDirectory("a", root);
        
        ElfDirectory e = new ElfDirectory("e", a);
        e.AddFile(new ElfFile("i", 584));

        a.AddFile(new ElfFile("a", 29116));
        a.AddFile(new ElfFile("g", 2557));
        a.AddFile(new ElfFile("h.lst", 62596));

        root.AddFile(new ElfFile("b.txt", 14848514));
        root.AddFile(new ElfFile("c.dat", 8504156));

        ElfDirectory d = new ElfDirectory("d", root);
        d.AddFile(new ElfFile("j", 4060174));
        d.AddFile(new ElfFile("d.log", 8033020));
        d.AddFile(new ElfFile("d.ext", 5626152));
        d.AddFile(new ElfFile("k", 7214296));

        Console.WriteLine("Testing Example: ");
        Test.Assert(e.Size() == 584, $"  Expected e to have size 584 but had {e.Size()}" );
        Test.Assert(a.Size() == 94853, $"  Expected a to have size 94853 but had {a.Size()}" );
        Test.Assert(d.Size() == 24933642, $"  Expected d to have size 24933642 but had {d.Size()}" );
        Test.Assert(root.Size() == 48381165, $"  Expected root to have size 48381165 but had {root.Size()}");

        if (pass)
        {
            Console.WriteLine($"ElfDirectory.TestExample: Pass!");
        }
        return pass;
    }
}