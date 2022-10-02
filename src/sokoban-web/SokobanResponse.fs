namespace sokoban_web

open System

type SokobanResponse =
    { Moves: Uri list;
      Game: string}
