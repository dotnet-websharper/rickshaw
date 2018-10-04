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
namespace Ajax

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
        
        let color = 
            [|
                new SeriesColor("New York", "#c05020")
                new SeriesColor("London", "#30c020")
                new SeriesColor("Tokyo", "#6060c0")
            |]

        div [
            attr.id "diagram"
            on.afterRender (fun diagram ->
                let graphdata =
                    FileConfig(
                        diagram,
                        "data.json",
                        Renderer="line",
                        Width=400,
                        Height=200,
                        OnData = (fun x -> x),
                        Series=color
                    )
                let graph = Rickshaw.Graph.Ajax(graphdata)
                ()
            )
        ] []
        |> Doc.RunById "main"
