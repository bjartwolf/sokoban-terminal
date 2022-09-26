namespace sokoban_web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open sokoban_web
open Microsoft.AspNetCore.Hosting.Server
open Microsoft.AspNetCore.Hosting.Server.Features

[<ApiController>]
[<Route("[controller]")>]
type SokobanController (logger : ILogger<SokobanController>, server: IServer) =
    inherit ControllerBase()

    [<HttpGet("{boardnr}/")>]
    member _.Get(boardnr:int) =
                let board = sokoban.game.init(0)
                let moves = ['u';'d';'l';'r';'b']
                let serverUrl = server.Features.Get<IServerAddressesFeature>().Addresses.First()
                let moveUrls = moves |> List.map (fun m -> new Uri(sprintf "%s/sokoban/%d/%s" serverUrl boardnr (m.ToString())))
                { Moves = moveUrls 
                  Game = board 
                }

    [<HttpGet("{boardnr}/{history}/")>]
    member _.Get(boardnr:int, history:string) =
                let board = sokoban.game.init(0)
                let moves = ['u';'d';'l';'r';'b']
                let serverUrl = server.Features.Get<IServerAddressesFeature>().Addresses.First()
                let moveUrls = moves |> List.map (fun m -> new Uri(sprintf "%s/sokoban/%d/%s" serverUrl boardnr (history + m.ToString())))
                { Moves = moveUrls 
                  Game = board 
                }
