namespace Bars

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
        
        let seriesdata = [| [| |]; [| |]; [| |] |] : (Coord []) []

        let randata = Rickshaw.Fixtures.RandomData(150)
        
        for i=1 to 70 do
                randata.AddData(seriesdata)

        let diagram = div [attr.id "diagram"] []

        let data =
            [|
                Series(seriesdata.[0], Color = "#c05020")
                Series(seriesdata.[1], Color = "#30c020")
                Series(seriesdata.[2], Color = "#6060c0")
            |]

        let graphdata = GraphData(diagram.Dom, data, Renderer="bar", Width=960, Height=500) 

        let graph = Rickshaw.Graph(graphdata)

        graph.Render()

        div [] [
            diagram
        ]|> Doc.RunById "main"
