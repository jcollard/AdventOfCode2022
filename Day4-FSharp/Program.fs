open System
open System.Linq
// For more information see https://aka.ms/fsharp-console-apps
let sample = [
    "2-4,6-8";
    "2-3,4-5";
    "5-7,7-9";
    "2-8,3-7";
    "6-6,4-6";
    "2-6,4-8";
]

let pred : obj -> bool = not << isNull
let lines: seq<string> = Seq.initInfinite (fun _ -> Console.ReadLine()) |> Seq.takeWhile pred
let lines' = lines |> List.ofSeq
let pair (arr: 'a[]) = (arr[0], arr[1])
let parseRange (s: string) = s.Split("-").Select(int).ToArray() |> pair
let parseRanges (s: string) = s.Split(",") |> Seq.map parseRange |> Array.ofSeq |> pair
let contains ((l0: int, u0: int), (l1: int, u1: int)) = (l0 <= l1 && u0 >= u1) || (l1 <= l0 && u1 >= l0)
let contains' (l: int, u: int) (x: int) = l <= x && u >= x
let intersects ((l0: int, u0: int), (l1: int, u1: int)) = (contains' (l0, u0) l1) || (contains' (l1, u1) l0)
let part1: seq<string> -> int = Seq.map parseRanges >> Seq.filter contains >> Seq.length
let part2: seq<string> -> int = Seq.map parseRanges >> Seq.filter intersects >> Seq.length

printfn "Part 1: %d" (part1 lines')
printfn "Part 2: %d" (part2 lines')
