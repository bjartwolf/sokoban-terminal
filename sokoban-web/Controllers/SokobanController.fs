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

    [<HttpGet>]
    member _.Get() =
                { Moves = server.Features.Get<IServerAddressesFeature>().Addresses |> Seq.map (fun a -> new Uri(a))  |> Seq.toList;
                  Game = "####"
                }
