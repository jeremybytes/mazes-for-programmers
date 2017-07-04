// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
open Functions

// Define your library scripting code here

open System.Linq

type Cell = North | East | Root

let rows = 10
let cols = 10
let rand = System.Random()

let row () = (Seq.singleton Root)
             |> Seq.append (Enumerable.Repeat(East, cols - 1))

let maingrid() = Enumerable.Repeat(row(), rows - 1)

let grid = (row() |> Seq.toList) :: 
            (maingrid ()
                   |> Seq.map (fun r -> r |> Seq.map (fun cell -> match cell with
                                                                  | Root -> North
                                                                  | East when rand.Next(2) = 1 -> North
                                                                  | _ -> East)
                                          |> Seq.toList) // row
                   |> Seq.toList) // grid

grid |> List.iter (fun r -> printf "+"
                            Seq.iter (fun cell -> match cell with
                                                  | North -> printf "   "
                                                  | _ -> printf "---"
                                                  printf "+") r
                            printfn ""
                            printf "|"
                            Seq.iter (fun cell -> match cell with
                                                  | East -> printf "    "
                                                  | _ -> printf "   |") r
                            printfn "")
grid |> List.head |> (fun r -> printf "+"
                               Seq.iter (fun cell -> printf "---+") r
                               printfn "" )                  
            
