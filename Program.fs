open Saturn
open Giraffe
open Giraffe.GiraffeViewEngine

let nowView () = 
    let utcNow = System.DateTime.UtcNow
    System.Console.WriteLine("--- nowView called: " + utcNow.ToString())

    div [] [
        h1 [] [rawText <| utcNow.ToString()]
    ]

let browser = pipeline {
    use_warbler
}

let router = router {
    pipe_through browser
    get "/broken" (htmlView <| nowView ())
    get "/works" (warbler (fun _ -> htmlView <| nowView ()))
}

[<EntryPoint>]
let main argv =
    let app = application {
        use_router router
    }

    run app
    0 // return an integer exit code