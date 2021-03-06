// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace Extensions

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
        let seriesdata = [| |] : (Coord []) []

        for i=1 to 9 do
            seriesdata.JS.Push([| |] :Coord []) |> ignore
        
        let randata = Rickshaw.Fixtures.RandomData(150)
        
        for i=1 to 70 do
                randata.AddData(seriesdata)

        let palette = Rickshaw.Color.Palette(Scheme("classic9"))

        let diagram = Elt.div [attr.id "diagram"] []

        let series = 
            [|
                Series(seriesdata.[0], Color = palette.Color(), Name = "Moscow")
                Series(seriesdata.[1], Color = palette.Color(), Name = "Shanghai")
                Series(seriesdata.[2], Color = palette.Color(), Name = "Amsterdam")
                Series(seriesdata.[3], Color = palette.Color(), Name = "Paris")
                Series(seriesdata.[4], Color = palette.Color(), Name = "Tokyo")
                Series(seriesdata.[5], Color = palette.Color(), Name = "London")
                Series(seriesdata.[6], Color = palette.Color(), Name = "New York")
            |]

        let graph = Rickshaw.Graph(GraphData(diagram.Dom, series, Width=900, Height=500, Renderer="area", Stroke=true, Preserve=true))

        graph.Render()

        let preview = Elt.div [attr.id "preview"] []

        let range = Rickshaw.Graph.RangeSlider.Preview(Slider(preview.Dom,graph))

        let hoverdetail = Rickshaw.Graph.HoverDetail(Hover(graph, XFormatter=fun x -> Date( x*1000 ).ToDateString()))

        let timeline = Elt.div [attr.id "timeline"] []
        
        let annotator = Rickshaw.Graph.Annotate(Legend(graph, timeline.Dom))

        let leg = Elt.div [attr.id "legend"] []

        let graphlegend = Rickshaw.Graph.Legend(Legend(graph, leg.Dom))

        let toggle = Rickshaw.Graph.Behaviour.Series.Toggle(GLegend(graph, graphlegend))

        let order = Rickshaw.Graph.Behaviour.Series.Order(GLegend(graph, graphlegend))

        let highlight = Rickshaw.Graph.Behaviour.Series.Highlight(GLegend(graph, graphlegend))

        let smooth = Elt.div [attr.id "smoother"] []

        let smoother = Rickshaw.Graph.Smoother(Legend(graph, smooth.Dom))

        let xa = Rickshaw.Graph.Axis.Time(TimeAxis(graph, TicksTreatment="glow", TimeFixture=Rickshaw.Fixtures.Time.Local()))

        xa.Render()

        let ya = Rickshaw.Graph.Axis.Y(Axis(graph, TicksTreatment="glow", TicksFormat=Rickshaw.Fixtures.Number.FormatKMBT))

        ya.Render()

        
        let messages = 
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
            (fun () -> 
                randata.RemoveData(seriesdata)
                randata.AddData(seriesdata)
                graph.Update()
            )) 3000 |> ignore

        let addannotation =
            function force ->
                if Array.length messages > 0 && (force || Math.Random() >= 0.95) then
                    annotator.Add(seriesdata.[2].[Array.length seriesdata.[2]-1].X, messages.JS.Shift())
                    annotator.Update()

        addannotation(true)

         
        JS.SetTimeout((function () ->
            JS.SetInterval(function () -> addannotation(false)) 6000 |> ignore
            )) |> ignore
            

        let prevxa = Rickshaw.Graph.Axis.Time(TimeAxis(range.Previews.[0], TicksTreatment="glow", TimeFixture=Rickshaw.Fixtures.Time.Local()))
        
        prevxa.Render()
        
        let form = 
            Elt.div
                [
                    attr.id "side-panel"
                    attr.style "float: left"
                ]
                [
                    leg
                    smooth
                ]

        let chart =
            Elt.div
                [
                    attr.style "float: left; margin: 10px"
                ]
                [
                    diagram
                    timeline
                    preview
                ]


        let content =
            Elt.div
                [
                    attr.id "content"
                    attr.style "float: left"
                ]
                [] 

        content.Dom.AppendChild (chart.Dom) |> ignore
        content.Dom.AppendChild (form.Dom) |> ignore
        

        Doc.RunById "main" content
        

        



