namespace Colors

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
        
        let schemes = 
            [
                "spectrum14"
                "colorwheel"
                "cool"
                "spectrum2000"
                "spectrum2001"
                "classic9"
                "munin"
            ]

        let ldiv =
            Elt.div [] []

        let fund = fun d ->
            let palette = Rickshaw.Color.Palette(Scheme(d))
            
            let seriesdata = [| |] : (Coord []) []

            Array.map (function _ ->
                seriesdata.JS.Push([| |]: Coord []) |> ignore
            ) palette.Scheme |> ignore

            let randata = Rickshaw.Fixtures.RandomData(150)
        
            for i=1 to 70 do
                randata.AddData(seriesdata)
            
                
            let elem = Elt.div [] []
            let caption = span [] [ text d ]
            let section = 
                Elt.section
                    [
                        attr.width "300px"
                        attr.height "250px"
                        attr.style "float: left; margin: 16px"
                    ] 
                    [
                        elem
                        caption
                    ]

            ldiv.Dom.AppendChild(section.Dom) |> ignore

            let series = [| |] : Series []

            Array.map (fun x ->
                    series.JS.Push(Series(x,Color = palette.Color())) |> ignore
            ) seriesdata |> ignore

            let graph = Rickshaw.Graph(GraphData(elem.Dom, series, Width=300, Height=200))

            graph.Render()


        List.map fund schemes |> ignore

        Doc.RunById "main" ldiv
