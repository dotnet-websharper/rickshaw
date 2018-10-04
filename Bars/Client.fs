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

        let diagram = Elt.div [attr.id "diagram"] []

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
