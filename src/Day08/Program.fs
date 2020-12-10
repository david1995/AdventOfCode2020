open System
open System.IO

type Instruction =
| Acc of increment : int
| Jmp of relativeTarget : int
| Nop of number : int





let parseInstruction (input:string) =
  let splitInstruction =
    input.Split ' '
    |> List.ofArray

  match splitInstruction with
  | "acc"::incStr::[] ->
    let (succ, inc) = Int32.TryParse(incStr)
    if succ
      then Some(Acc inc)
      else None

  | "jmp"::targetStr::[] ->
    let (succ, target) = Int32.TryParse(targetStr)
    if succ
      then Some(Jmp target)
      else None

  | "nop"::numStr::[] ->
    let (succ, num) = Int32.TryParse(numStr)
    if succ
      then Some(Nop num)
      else None
  | _ -> None




let rec runProgram (program : Instruction array) (currentIndex : int) (accumulator : int) (executedIndices: int list) =
  if currentIndex >= program.Length
    then (accumulator, true)
  elif executedIndices |> List.contains currentIndex
    then (accumulator, false)
  else
    let currentCommand = program.[currentIndex]
    match currentCommand with
    | Acc inc -> runProgram program (currentIndex + 1) (accumulator + inc) (currentIndex::executedIndices)
    | Jmp target -> runProgram program (currentIndex + target) accumulator (currentIndex::executedIndices)
    | Nop _ -> runProgram program (currentIndex + 1) accumulator (currentIndex::executedIndices)





let task1() =
  Console.WriteLine ("Task 1")
  let commandStrings = File.ReadAllLines "input.txt"
  let program =
    commandStrings
    |> Seq.map parseInstruction
    |> Seq.filter Option.isSome
    |> Seq.map Option.get
    |> Array.ofSeq

  let (result, finishedExpectedly) = runProgram program 0 0 []
  Console.WriteLine (sprintf "Result: %i %b" result finishedExpectedly)





let rec getSwappedNopJmpProgramResult (inputProgram : Instruction list) (currentIndex : int) =
  if currentIndex >= inputProgram.Length then (0, false)
  else
    let mutatedProgramInstruction =
      match inputProgram.[currentIndex] with
      | Nop num -> Jmp num
      | Jmp target -> Nop target
      | i -> i

    let mutatedProgram =
      if currentIndex <= 0
        then mutatedProgramInstruction::inputProgram.Tail
        else
          let before = (List.take (currentIndex) inputProgram)
          let tail = (List.skip (currentIndex + 1) inputProgram)
          before@mutatedProgramInstruction::tail
      |> Array.ofList

    let (result, finishedExpectedly) = runProgram mutatedProgram 0 0 []

    if finishedExpectedly
      then (result, finishedExpectedly)
      else getSwappedNopJmpProgramResult inputProgram (currentIndex + 1)





let task2() =
  Console.WriteLine ("Task 2")
  let commandStrings = File.ReadAllLines "input.txt"
  let program =
    commandStrings
    |> Seq.map parseInstruction
    |> Seq.filter Option.isSome
    |> Seq.map Option.get
    |> Array.ofSeq

  let (result, finishedExpectedly) = getSwappedNopJmpProgramResult (program |> List.ofArray) 0
  Console.WriteLine (sprintf "Result: %i %b" result finishedExpectedly)






[<EntryPoint>]
let main argv =
  task1()
  task2()
  Console.ReadLine() |> ignore
  0
