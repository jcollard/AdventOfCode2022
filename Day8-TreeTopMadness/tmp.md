# Beginner's Guide

Happy Thursday!

Beginner's Guide to Day 8 Video: [https://youtu.be/RJDgMcrJ8wE](https://youtu.be/RJDgMcrJ8wE)

I've created a guide for new programmers that talks through a straight forward
strategy for solving today's puzzle. Anyone who has a handle functions and
2-dimensional arrays should be able to complete it. The video allows a moment
for you to pause before revealing spoilers.

Although this solution is in C#, it provides a high level overview of the steps
necessary to solve the puzzle in any programming language:

    string[] rows = File.ReadAllLines(args[0]);
    int[,] heightMap = HeightMap.ParseHeightMap(rows);
    int count = 0;
    for (int r = 0; r < heightMap.GetLength(0); r++)
    {
        for (int c = 0; c < heightMap.GetLength(1); c++)
        {
            if (HeightMap.IsVisible(heightMap, r, c))
            {
                count++;
            }
        }
    }
    Console.WriteLine($"There are {count} visible trees.");

The full code can be found on
[Github](https://github.com/jcollard/AdventOfCode2022/blob/jcollard/solution/Day8-TreeTopMadness/Solution/HeightMap.cs)