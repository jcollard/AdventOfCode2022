public static class Test
{

    public static bool Assert(bool assertion, string errorMessage)
    {
        if (!assertion)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(errorMessage);
            Console.ResetColor();
        }
        return assertion;
    }

}