namespace Simple

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
        
        let Series1 = Series( [|Coord(0,40); Coord(1,49); Coord(2,38); Coord(3,30); Coord(4,32)|], Color = "steelblue")
                                                                                                   
        let Series2 = Series( [|Coord(0,19); Coord(1,22); Coord(2,32); Coord(3,20); Coord(4,21)|], Color = "lightblue")

        let Chart = Doc.Element "div" [attr.id "diagram"] [Doc.Empty]

        let GData = GraphData(Chart.Dom, [|Series1; Series2|], Renderer="line") 

        let Test = Rickshaw.Graph(GData)

        Test.Render()

        div [
            Chart
        ]|> Doc.RunById "main"
         

       
       