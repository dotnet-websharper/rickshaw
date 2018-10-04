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
namespace FixedDuration

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

        div [
            attr.id "diagram"

            on.afterRender (fun elt ->
                let series = Rickshaw.Series.FixedDuration([|FixDurArr("one")|], JS.Undefined, FixDurObj(250, 100, Date().GetTime()/1000))

                let graph = Rickshaw.Graph(GraphData(elt,series,Width=900,Height=500,Renderer="line"))

                graph.Render()

                let mutable i = 0.

                JS.SetInterval((fun () ->
                        let randint = Math.Random()*100.
                        i <- (i + 1.)
                        let data = New [
                            "one" => Math.Random() * 40. + 120.
                            "two" => (Math.Sin( i / 40.) + 4.) * (randint + 400.)
                            "three" => randint + 300.
                        ]
                        series.AddData(data)
                        graph.Update() 
                    )) 250 |> ignore
            )
        ] []
        |> Doc.RunById "main"
