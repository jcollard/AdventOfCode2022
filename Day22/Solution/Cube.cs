public record Cube(Face F1, Face F2, Face F3, Face F4, Face F5, Face F6)
{

    public static Board Parse(int top, int left, char[][] input)
    {
        char[][] data = new char[50][];
        for (int r = 0; r < 50; r++)
        {
            data[r] = new char[50];
            for (int c = 0; c < 50; c++)
            {
                data[r][c] = input[r + top][c + left];
            }
        }
        return new Board(data);
    }

    public static Cube Parse(string[] rows)
    {
        char[][] input = rows.Select(r => r.PadRight(150, ' ').ToCharArray()).ToArray();
        Board b1 = Parse(0, 100, input);
        Board b3 = Parse(0, 50, input);
        Board b5 = Parse(50, 50, input);
        Board b4 = Parse(100, 50, input);
        Board b6 = Parse(100, 0, input);
        Board b2 = Parse(150, 0, input);

        Face f1 = new Face(1, b1);
        Face f2 = new Face(2, b2);
        Face f3 = new Face(3, b3);
        Face f4 = new Face(4, b4);
        Face f5 = new Face(5, b5);
        Face f6 = new Face(6, b6);

        f1.North = (f2, new Rotation(0));
        f1.East = (f4, new Rotation(2));
        f1.South = (f5, new Rotation(1));
        f1.West = (f3, new Rotation(0));

        f2.North = (f6, new Rotation(0));
        f2.East = (f4, new Rotation(3));
        f2.South = (f1, new Rotation(0));
        f2.West = (f3, new Rotation(3));

        f3.North = (f2, new Rotation(1));
        f3.East = (f1, new Rotation(0));
        f3.South = (f5, new Rotation(0));
        f3.West = (f6, new Rotation(2));

        f4.North = (f5, new Rotation(0));
        f4.East = (f1, new Rotation(2));
        f4.South = (f2, new Rotation(1));
        f4.West = (f6, new Rotation(0));

        f5.North = (f3, new Rotation(0));
        f5.East = (f1, new Rotation(3));
        f5.South = (f4, new Rotation(0));
        f5.West = (f6, new Rotation(3));

        f6.North = (f5, new Rotation(1));
        f6.East = (f4, new Rotation(0));
        f6.South = (f2, new Rotation(0));
        f6.West = (f3, new Rotation(2));

        return new Cube(f1, f2, f3, f4, f5, f6);
    }

}

public record Rotation(int Count)
{
    public Facing RotateCW(Facing f, int times)
    {
        while (times-- > 0)
        {
            f = f switch {
                Facing.North => Facing.East,
                Facing.East => Facing.South,
                Facing.South => Facing.West,
                Facing.West => Facing.North,
                _ => throw new Exception($"Could not rotate {f}")
            };
        }
        return f;
    }   
    public Facing RotateCounterCW(Facing f) => RotateCW(f, (Count * 3) % 4);
    public Facing RotateCW(Facing f) => RotateCW(f, Count);
    public Position RotateCW(Position p, int size) => RotateCW(p, size, Count);
    public Position RotateCW(Position p, int size, int times)
    {
        while (times-- > 0)
        {
            int newR = p.Col;
            int newC = size - 1 - p.Row;
            p = new Position(newR, newC);
        }
        return p;
    }

    public Position RotateCounterCW(Position p, int size) => RotateCW(p, size, (Count*3)%4);

    public void RotateCW(char[][] data) => RotateCW(data, Count);
    public void RotateCW(char[][] data, int times)
    {
        if (Count <= 0) return;
        int size = data.Length;
        char[][] newB = null!;
        char[][] temp = data;
        while (times-- > 0)
        {
            newB = new char[size][];
            for (int r = 0; r < size; r++)
            {
                newB[r] = new char[size];
                for (int c = 0; c < size; c++)
                {
                    newB[r][c] = temp[size - 1 - c][r];
                }
            }
            temp = newB;
        }
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c ++)
            {
                data[r][c] = temp[r][c];
            }
        }
    }

    public void RotateCounterCW(char[][] data) => RotateCW(data, (3 * Count) % 4);
}