namespace Bars

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
        
        let SeriesData = [| [| |]; [| |]; [| |] |] : (Coord []) []

        let RanData = Rickshaw.Fixtures.RandomData(150)
        
        for i=1 to 70 do
                RanData.AddData(SeriesData)

        let Place = Doc.Element "div" [attr.id "diagram"] [Doc.Empty]

        let D =
            [|
                Series(SeriesData.[0], Color = "#c05020")
                Series(SeriesData.[1], Color = "#30c020")
                Series(SeriesData.[2], Color = "#6060c0")
            |]

        let GData = GraphData(Place.Dom, D, Renderer="bar", Width=960, Height=500) 

        let Test = Rickshaw.Graph(GData)

        Test.Render()

        div [
            label [text "Hello"]
            Place
            label [text "Hello"]
        ]|> Doc.RunById "main"