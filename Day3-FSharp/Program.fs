open System
open System.Linq
open System.Collections.Generic;

let pred : obj -> bool = not << isNull
let sacks _ : seq<string> = Seq.initInfinite (fun _ -> Console.ReadLine()) |> Seq.takeWhile pred
let testSacks = [
    "vJrwpWtwJgWrhcsFMMfFFhFp"; 
    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"; 
    "PmmdzqPrVvPwwTWBwg";
    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn";
    "ttgJtRGJQctTZtZT";
    "CrZsJsPPZsGzwwsLwLmpwMDw"
    ]
let split (sack : string) = (sack.[..(sack.Length/2 - 1)], sack.[(sack.Length/2)..])
let asEnumerable (s: string) = s.AsEnumerable()
let intersect (c0: IEnumerable<'a>) (c1: IEnumerable<'a>) = c0.Intersect(c1)
let intersect' (all: seq<IEnumerable<'a>>) = Seq.reduce intersect all
let withPair (f: 'a -> 'b -> 'c) (a, b) = f a b 
let unpair (f: 'a * 'b -> 'c) (a: 'a) (b: 'b) = f (a, b)
let chSub a b = (int a) - (int b)
let item (s: string) = s[0]
let priority (ch: char) = if Char.IsUpper ch then (chSub ch 'A') + 27 else (chSub ch 'a') + 1
let first (a: IEnumerable<'a>) = a.First()
let part1 sacks =  Seq.map (split >> withPair intersect >> first >> priority) sacks|> Seq.sum
let part2 sacks = (Seq.chunkBySize 3 sacks) |> Seq.map (Seq.map asEnumerable >> intersect' >> first >> priority) |> Seq.sum;;
let sacks' = (sacks null) |> List.ofSeq

printfn "%A" (part1 sacks')
printfn "%A" (part2 sacks')