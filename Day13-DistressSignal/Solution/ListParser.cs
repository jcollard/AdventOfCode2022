public static class ListParser
{
    public static Queue<char> StringToQueue(string toParse)
    {
        Queue<char> queue = new Queue<char>();
        foreach (char ch in toParse)
        {
            queue.Enqueue(ch);
        }
        return queue;
    }

    public static List<object> Parse(string data)
    {
        return ParseList(StringToQueue(data));
    }

    public static int ParseInt(Queue<char> data)
    {
        string token = string.Empty;
        while (char.IsDigit(data.Peek()))
        {
            token += data.Dequeue();
        }
        return int.Parse(token);
    }

    public static List<object> ParseList(Queue<char> data)
    {
        List<object> elements = new ();
        data.Dequeue(); // Remove the '[' from the queue
        while (data.Peek() != ']')
        {
            object el = ParseElement(data);
            elements.Add(el);
            if (data.Peek() == ',')
            {
                data.Dequeue();
            }
        }
        data.Dequeue(); // Remove the ']' from the queue
        return elements;
    }

    public static object ParseElement(Queue<char> data)
    {
        char next = data.Peek();
        if (char.IsDigit(next))
        {
            return ParseInt(data);
        }
        else if (next == '[')
        {
            return ParseList(data);
        }
        else
        {
            throw new Exception($"Expected an int or list but found: {string.Join("", data)}");
        }
    }


}