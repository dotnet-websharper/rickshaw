namespace FixedDuration

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Rickshaw

[<JavaScript>]
module Client =    
    let Main =

        let diagram = divAttr [attr.id "diagram"] []

        let series = Rickshaw.Series.FixedDuration([|FixDurArr("one")|], JS.Undefined, FixDurObj(250, 100, Date().GetTime()/1000))

        let graph = Rickshaw.Graph(GraphData(diagram.Dom,Series,Width=900,Height=500,Renderer="line"))

        graph.Render()

        let mutable i = 0.

        JS.SetInterval((fun () ->
                let randint = Math.Random()*100.
                i <- (i + 1.)
                let data = New [
                    "one" => Math.Random() * 40. + 120.
                    "two" => (Math.Sin( i / 40.) + 4.) * (randint + 400.)
                    "three" => randint + 300.
                ]
                series.AddData(data)
                graph.Update() 
            )) 250 |> ignore

        Doc.RunById "main" diagram
