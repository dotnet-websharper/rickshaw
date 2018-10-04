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
namespace Simple

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
        
        let series1 = Series( [|Coord(0,40); Coord(1,49); Coord(2,38); Coord(3,30); Coord(4,32)|], Color = "steelblue")
                                                                                                   
        let series2 = Series( [|Coord(0,19); Coord(1,22); Coord(2,32); Coord(3,20); Coord(4,21)|], Color = "lightblue")

        div [
            attr.id "diagram"
            on.afterRender (fun diagram ->
                let graphdata = GraphData(diagram, [|series1; series2|], Renderer="line") 

                let test = Rickshaw.Graph(graphdata)

                test.Render()
            )
        ] []

        |> Doc.RunById "main"
         

       
       
