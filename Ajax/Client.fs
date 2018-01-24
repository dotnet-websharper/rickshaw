namespace Ajax

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
        
        let color = 
            [|
                new SeriesColor("New York", "#c05020")
                new SeriesColor("London", "#30c020")
                new SeriesColor("Tokyo", "#6060c0")
            |]

        let diagram = div [attr.id "diagram"] []

        let graphdata =
            FileConfig(
                diagram.Dom,
                "data.json",
                Renderer="line",
                Width=400,
                Height=200,
                OnData = (fun x -> x),
                Series=color
            )

        let graph = Rickshaw.Graph.Ajax(graphdata)

        div [] [
            diagram
        ]|> Doc.RunById "main"
