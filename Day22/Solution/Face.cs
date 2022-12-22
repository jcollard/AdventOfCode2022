using System.Text;

public record Face(int ID, Board Board)
{
    public (Face, Rotation) North { get; set; }
    public (Face, Rotation) East { get; set; }
    public (Face, Rotation) South { get; set; }
    public (Face, Rotation) West { get; set; }

    public (Face, Position, Facing) NextPosition(Position position, Facing facing)
    {
        Position desired = position.FromFacing(facing);
        if (Board.IsInBounds(desired))
        {
            if (Board.IsOpen(desired))
            {
                return (this, desired, facing);
            }
            else if (Board.IsWall(desired))
            {
                return (this, position, facing);
            }
            else
            {
                throw new Exception($"Unknown tile @ {desired} {Board.Data[desired.Row][desired.Col]}");
            }
        }
        else
        {
            (Face f, Rotation r) = FindFace(facing);
            // Wrap around
            desired = Wrap(desired, facing);
            // Rotate position on new board
            Position p = r.RotateCW(desired, Board.Data.Length);

            // If there is a wall there, don't move
            if (f.Board.IsWall(p))
            {
                return (this, position, facing);
            }

            Facing newF = r.RotateCW(facing);
            // Rotate face back
            //r.RotateCW(f.Board.Data);
            return (f, p, newF);
        }
    }

    public Position Wrap(Position p, Facing f)
    {
        return f switch {
            Facing.East => p with { Col = 0 },
            Facing.West => p with { Col = Board.Data.Length - 1 },
            Facing.North => p with { Row = Board.Data.Length - 1 },
            Facing.South => p with { Row = 0 },
            _ => throw new Exception($"Could not move {f}"),
        };
    }

    public (Face, Rotation) FindFace(Facing f)
    {
        return f switch {
            Facing.North => North,
            Facing.East => East,
            Facing.West => West,
            Facing.South => South,
            _ => throw new Exception($"Could not find face in direction {f}"),
        };
    }


}