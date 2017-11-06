module loadTestTests

open canopy.runner
open loadTest

let all () =
    context "tests for the load tester in integration"

    "can create a basic job" &&&& fun _ ->
        let job =
          {
            Warmup = true
            Baseline = true
            AcceptableRatioPercent = 200
            Iterations = 1
            Tasks =
              [
                {
                  Description = "print hello world"
                  Action = fun _ -> printfn "hello world"
                  Frequency = 60
                }
              ]
          }

        runLoadTest job

    "timelines look reasonable" &&! fun _ ->
        let job =
          {
            Warmup = true
            Baseline = true
            AcceptableRatioPercent = 200
            Iterations = 1
            Tasks =
              [
                {
                  Description = "print hello world"
                  Action = fun _ -> printfn "hello world"
                  Frequency = 4
                }
              ]
          }

        createTimeline job
        |> List.iter (fun (timing, task) -> printfn "%A %A" timing task.Description)
