namespace Simple

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.Rickshaw

[<JavaScript>]
module Client =

    [<SPAEntryPoint>]
    let Main() =
        
        let series1 = Series( [|Coord(0,40); Coord(1,49); Coord(2,38); Coord(3,30); Coord(4,32)|], Color = "steelblue")
                                                                                                   
        let series2 = Series( [|Coord(0,19); Coord(1,22); Coord(2,32); Coord(3,20); Coord(4,21)|], Color = "lightblue")

        let diagram = div [attr.id "diagram"] []

        let graphdata = GraphData(diagram.Dom, [|series1; series2|], Renderer="line") 

        let test = Rickshaw.Graph(graphdata)

        test.Render()

        div [] [
            diagram
        ]|> Doc.RunById "main"
         

       
       
