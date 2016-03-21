namespace Ajax

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open Websharper.Rickshaw

[<JavaScript>]
module Client =

    let Fun =
            fun d -> d

    let Main =
        
        let Color = 
            [|
                new SeriesColor("New York", "#c05020")
                new SeriesColor("London", "#30c020")
                new SeriesColor("Tokyo", "#6060c0")
            |]

        let Place = Doc.Element "div" [attr.id "diagram"] [Doc.Empty]

        let GData = FileConfig(Place.Dom, "data.json", Renderer="line", Width=400, Height=200, OnData = Fun, Series=Color) 

        let Test = Rickshaw.Graph.Ajax(GData)

        div [
            Place
        ]|> Doc.RunById "main"
