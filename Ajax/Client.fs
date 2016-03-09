namespace Ajax

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Rickshaw

[<JavaScript>]
module Client =

    let id =
            fun d -> d

    let Main =
        
        let color = 
            [|
                new SeriesColor("New York", "#c05020")
                new SeriesColor("London", "#30c020")
                new SeriesColor("Tokyo", "#6060c0")
            |]

        let diagram = divAttr [attr.id "diagram"] []

        let graphdata = FileConfig(diagram.Dom, "data.json", Renderer="line", Width=400, Height=200, OnData = id, Series=color) 

        let graph = Rickshaw.Graph.Ajax(graphdata)

        div [
            diagram
        ]|> Doc.RunById "main"
