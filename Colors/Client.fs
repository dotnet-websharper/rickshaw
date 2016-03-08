namespace Colors

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
        
        let Schemes = 
            [
                "spectrum14"
                "colorwheel"
                "cool"
                "spectrum2000"
                "spectrum2001"
                "classic9"
                "munin"
            ]

        let Ldiv =
            div []

        let Fun = fun d ->
            let Palette = Rickshaw.Color.Palette(Scheme(d))
            
            let SeriesData = [| |] : (Coord []) []

            Array.map (function _ ->
                SeriesData.JS.Push([| |]: Coord []) |> ignore
            ) Palette.Scheme |> ignore

            let RanData = Rickshaw.Fixtures.RandomData(150)
        
            for i=1 to 70 do
                RanData.AddData(SeriesData)
            
                
            let Elem = divAttr [] [Doc.Empty]
            let Caption = spanAttr [] [ text d ]
            let Section = 
                sectionAttr 
                    [
                        attr.width "300px"
                        attr.height "250px"
                        attr.style "float: left; margin: 16px"
                    ] 
                    [
                        Elem
                        Caption
                    ]

            Ldiv.Dom.AppendChild(Section.Dom) |> ignore

            let Serie = [| |] : Series []


            Array.map (fun x ->
                    Serie.JS.Push(Series(x,Color = Palette.Color())) |> ignore
            ) SeriesData |> ignore

            let Graph = Rickshaw.Graph(GraphData(Elem.Dom, Serie, Width=300, Height=200))

            Graph.Render()

            Console.Log(d)


        List.map Fun Schemes |> ignore

        Doc.RunById "main" Ldiv