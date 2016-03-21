namespace Extensions

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
        let SeriesData = [| |] : (Coord []) []

        for i=1 to 9 do
            SeriesData.JS.Push([| |] :Coord []) |> ignore
        
        let RanData = Rickshaw.Fixtures.RandomData(150)
        
        for i=1 to 70 do
                RanData.AddData(SeriesData)

        let Palette = Rickshaw.Color.Palette(Scheme("classic9"))

        let Place = Doc.Element "div" [attr.id "diagram"] [Doc.Empty]

        let Series = 
            [|
                Series(SeriesData.[0], Color = Palette.Color(), Name = "Moscow")
                Series(SeriesData.[1], Color = Palette.Color(), Name = "Shanghai")
                Series(SeriesData.[2], Color = Palette.Color(), Name = "Amsterdam")
                Series(SeriesData.[3], Color = Palette.Color(), Name = "Paris")
                Series(SeriesData.[4], Color = Palette.Color(), Name = "Tokyo")
                Series(SeriesData.[5], Color = Palette.Color(), Name = "London")
                Series(SeriesData.[6], Color = Palette.Color(), Name = "New York")
            |]

        let Graph = Rickshaw.Graph(GraphData(Place.Dom, Series, Width=900, Height=500, Renderer="area", Stroke=true, Preserve=true))

        Graph.Render()

        let Prev = Doc.Element "div" [attr.id "preview"] [Doc.Empty]

        let Range = Rickshaw.Graph.RangeSlider.Preview(Slider(Prev.Dom,Graph))

        let XFormat = 
            fun x ->
                Date( x*1000 ).ToDateString()
       
        let HoverDetail = Rickshaw.Graph.HoverDetail(Hover(Graph, XFormatter=XFormat))

        let TimeLine = Doc.Element "div" [attr.id "timeline"] [Doc.Empty]
        
        let Annotator = Rickshaw.Graph.Annotate(Legend(Graph, TimeLine.Dom))

        let Leg = Doc.Element "div" [attr.id "legend"] []

        let GraphLegend = Rickshaw.Graph.Legend(Legend(Graph, Leg.Dom))

        let Toggle = Rickshaw.Graph.Behaviour.Series.Toggle(GLegend(Graph, GraphLegend))

        let Order = Rickshaw.Graph.Behaviour.Series.Order(GLegend(Graph, GraphLegend))

        let Highlight = Rickshaw.Graph.Behaviour.Series.Highlight(GLegend(Graph, GraphLegend))

        let Smooth = Doc.Element "div" [attr.id "smoother"] []

        let Smoother = Rickshaw.Graph.Smoother(Legend(Graph, Smooth.Dom))

        let XA = Rickshaw.Graph.Axis.Time(TimeAxis(Graph, TicksTreatment="glow", TimeFixture=Rickshaw.Fixtures.Time.Local()))

        XA.Render()

        let YA = Rickshaw.Graph.Axis.Y(Axis(Graph, TicksTreatment="glow", TicksFormat=Rickshaw.Fixtures.Number.FormatKMBT))

        YA.Render()

        
        let Messages = 
            [|
                "Changed home page welcome message"
                "Minified JS and CSS"
                "Changed button color from blue to green"
                "Refactored SQL query to use indexed columns"
                "Added additional logging for debugging"
                "Fixed typo"
                "Rewrite conditional logic for clarity"
                "Added documentation for new methods"
            |]

        JS.SetInterval(
            (function () -> 
                RanData.RemoveData(SeriesData)
                RanData.AddData(SeriesData)
                Graph.Update()
            )) <| 3000 |> ignore

        let AddAnnotation =
            function force ->
                if Array.length Messages > 0 && (force || Math.Random() >= 0.95) then
                    Annotator.Add(SeriesData.[2].[Array.length SeriesData.[2]-1].X, Messages.JS.Shift())
                    Annotator.Update()

        AddAnnotation(true)

         
        JS.SetTimeout((function () ->
            JS.SetInterval(function () -> AddAnnotation(false)) <| 6000 |> ignore
            )) |> ignore
            

        let PrevXA = Rickshaw.Graph.Axis.Time(TimeAxis(Range.Previews.[0], TicksTreatment="glow", TimeFixture=Rickshaw.Fixtures.Time.Local()))
        
        PrevXA.Render()
        
        let Form = 
            Doc.Element
                "div"
                [
                    attr.id "side-panel"
                    attr.style "float: left"
                ]
                [
                    Leg
                    Smooth
                ]

        let Chart =
            Doc.Element
                "div"
                [
                    attr.style "float: left; margin: 10px"
                ]
                [
                    Place
                    TimeLine
                    Prev
                ]


        let Content =
            Doc.Element
                "div"
                [
                    attr.id "content"
                    attr.style "float: left"
                ]
                [] 

        Content.Dom.AppendChild (Chart.Dom) |> ignore
        Content.Dom.AppendChild (Form.Dom) |> ignore
        

        Doc.RunById "main" Content
        

        



