ElfDirectory root = new ElfDirectory("/");
Dictionary<string, ElfDirectory> registry = new();
registry["/"] = root;
Queue<string> input = new(File.ReadAllLines("input.txt").ToArray());
input.Dequeue();
Stack<ElfDirectory> paths = new Stack<ElfDirectory>();
paths.Push(root);

while (input.Count > 0)
{
    string command = input.Dequeue();
    // Console.WriteLine(command);
    Command c = CommandUtils.ParseCommand(command);
    c.ProcessCommand(paths, registry);
}



void Part1()
{
    int sum = registry.Values.Where(d => d.Size <= 100_000).Sum(d => d.Size);
    Console.WriteLine(sum);
}

void Part2()
{
    int totalSpace = 70_000_000;
    int currentSpace = totalSpace - root.Size;
    int requiredSpace = 30_000_000 - currentSpace;
    List<ElfDirectory> dirs = registry.Values.ToList();
    dirs.Sort((d0, d1) => d0.Size - d1.Size);
    ElfDirectory toDelete = dirs.First(d => d.Size >= requiredSpace);
    // Console.WriteLine(string.Join("\n", dirs));
    // Console.WriteLine($"Space required: {requiredSpace}");
    Console.WriteLine(toDelete);
}


// foreach (KeyValuePair<string, ElfDirectory> el in registry)
// {
//     Console.WriteLine(el.Value);
// }