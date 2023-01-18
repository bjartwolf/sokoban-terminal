module Tests

open sokoban.terminal.elmish
open Xunit

[<Fact>]
let ``Initialized board nr 3`` () =
    let (model, _) = init ()
    Assert.Equal(3, model.BoardNr)

[<Fact>]
let ``Move left from board 3 changes board state`` () =
    let (model, _) = init ()
    let (model', _) = update (Move Left)  model
    Assert.NotStrictEqual(model.Board, model'.Board)

[<Fact>]
let ``Move right from board 3 does not change state`` () =
    let (model, _) = init ()
    let (model', _) = update (Move Right)  model
    Assert.StrictEqual(model.Board, model'.Board)

[<Fact>]
let ``Move left from board 3 and undo does not change board state`` () =
    let (model, _) = init ()
    let (model', _) = update (Move Left)  model
    let (model'', _) = update (Move Undo)  model'
    Assert.StrictEqual(model.Board, model''.Board)

[<Fact>]
let ``Change level changes board`` () =
    let (model, _) = init ()
    let (model', _) = update (Level 0)  model
    Assert.NotStrictEqual(model.Board, model'.Board)


