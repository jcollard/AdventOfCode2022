// 1. Representing an ElfFile
// 2. Write Tests for an ElfFile
// 3. Write Implementation for Parsing an ElfFile

// 4. Represent ElfDirectory
// 5. Write Tests / Write implementation for ElfDirectory
//    a. Parse and IsParseable
//    b. AddFile and Size
//    c. TestExample and Size
// 6. Write Implementation for ElfDirectory

RunTests();

bool RunTests()
{
    bool pass = ElfFile.RunTests();
    pass &= ElfDirectory.RunTests();
    return pass;
}