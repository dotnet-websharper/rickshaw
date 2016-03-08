namespace FixedDuration

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open Websharper.Rickshaw

[<JavaScript>]
module Client =    
    let Main =

        let Place = divAttr [attr.id "diagram"] [Doc.Empty]

        let Ser = Rickshaw.Series.FixedDuration([|FixDurArr("one")|], JS.Undefined, FixDurObj(250, 100, Date().GetTime()/1000))

        let Graph = Rickshaw.Graph(GraphData(Place.Dom,Ser,Width=900,Height=500,Renderer="line"))

        Graph.Render()

        let mutable I = 0.

        JS.SetInterval( (function () ->
                let randInt = Math.Random()*100.
                I <- (I + 1.)
                let data = New [
                    "one" => Math.Random() * 40. + 120.
                    "two" => (Math.Sin( I / 40.) + 4.) * (randInt + 400.)
                    "three" => randInt + 300.
                ]
                Ser.AddData(data)
                Graph.Update() 
            )) <| 250 |> ignore

        Doc.RunById "main" Place
