namespace Websharper.Rickshaw

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator
open WebSharper.JavaScript.Dom
open WebSharper.JQuery

module Definition =
    

    //Rickshaw series

    let resourceList = 
        [
            Resource "jQuery.ui" "https://code.jquery.com/ui/1.11.3/jquery-ui.min.js"
        ] : CodeModel.Resource list

    let mutable classList = [] : CodeModel.NamespaceEntity list

    let addToClassList c =
        classList <- upcast c :: classList

    let ( |>! ) x f =
        f x
        x

    let EnumStrings name words = Pattern.EnumStrings name words |>! addToClassList


    let O = T<unit>
    let String = T<string>
    let Int = T<int>
    let Float = T<float>
    let Obj = T<obj>
    let Bool = T<bool>
    let Error = T<exn>
    let ( !| ) x = Type.ArrayOf x

    let Int2T = Type.Tuple [Int; Int]
    let Int2x2T = Type.Tuple [Int2T; Int2T]
    let Float2T = Type.Tuple [Float; Float]
    let Float3T = Type.Tuple [Float; Float; Float]
    let Float2x2T = Type.Tuple [Float2T; Float2T]
    let Comparator = Obj * Obj ^-> Int
    let Asd = !|Obj * Obj * Obj //?

    let Date = T<JavaScript.Date>
    let DateTime = T<System.DateTime>

    let Element = T<Element>
    let NodeList = T<NodeList>
    let Event = T<Event>

    //do this with JSON too, onComplete

    let Coord = 
        Pattern.Config "Coord"
            {
                Required =
                    [
                        "x", Int
                        "y", Float + Int
                    ]
                Optional = []
            }
        |>! addToClassList

    let Series =
        Pattern.Config "Series"
            {
                Required =
                    [
                        "data", !|Coord
                    ]
                Optional = 
                    [
                        "name", String
                        "color", String
                        "scale", Obj
                    ]
            }
        |>! addToClassList

    let SeriesColor =
        Pattern.Config "SeriesColor"
            {
                Required =
                    [
                        "name", String
                        "color", String
                    ]
                Optional = 
                    [
                        "scale", Obj
                    ]
            }
        |>! addToClassList

    let FileConfig = 
        Pattern.Config "FileConfig" 
            {
                Required = 
                    [
                        "element", Element
                        "dataURL", String
                    ] 
                Optional = 
                    [
                        "width", Int
                        "height", Int
                        "renderer", String
                        "onData", Series ^-> Series
                        "interpolation", String
                        "graph", Obj
                        "stroke", Bool
                        "preserve", Bool
                        "padding", Obj
                        "min", String
                        "unstack", Bool
                        "series", !|SeriesColor
                        "xScale", Obj
                        "yScale", Obj
                        "strokewidth", Float
                    ]
            }
        |>! addToClassList

    let GraphData = 
        Pattern.Config "GraphData"
            {
                Required = 
                    [
                        "element", Element
                        "series", !|Series + Obj
                    ]
                Optional =
                    [
                        "width", Int
                        "height", Int
                        "interpolation", String
                        "onData", Series ^-> Series
                        "renderer", String
                        "stroke", Bool
                        "preserve", Bool
                        "padding", Obj
                        "min", String
                        "unstack", Bool
                        "xScale", Obj
                        "yScale", Obj
                        "strokeWidth", Float
                    ]
            }
        |>! addToClassList

    let Scheme = 
        Pattern.Config "Scheme"
            {
                Required = 
                    [
                        "scheme", String
                    ]
                Optional = 
                    [
                        "interpolatedStopCount", Int
                    ]
            }
        |>! addToClassList
//
    let Slide =
        Pattern.Config "Slider"
            {
                Required =
                    [
                        "element", Element
                        "graph", Obj
                    ]
                Optional = []
            } |>! addToClassList
//
    let Hover =
        Pattern.Config "Hover"
            {
                Required =
                    [
                        "graph", Obj
                    ]
                Optional = 
                    [
                        "formatter", Obj * Int * Int ^-> String
                        "xFormatter", Int ^-> String
                    ]
            }
        |>! addToClassList
//
    let Legend =
        Pattern.Config "Legend"
            {
                Required =
                    [
                        "graph", Obj
                        "element", Element
                    ]
                Optional = []
            }
        |>! addToClassList

    let GLegend =
        Pattern.Config "GLegend"
            {
                Required =
                    [
                        "graph", Obj
                        "legend", Obj
                    ]
                Optional = []
            }
        |>! addToClassList

    let XAxis =
        Pattern.Config "XAxis"
            {
                Required =
                    [
                        "graph", Obj
                    ]
                Optional = 
                    [
                        "orientation", String
                        "ticksTreatment", String
                        "timeFixture", Obj
                        "pixelsPerTick", Int
                    ]
            }
        |>! addToClassList

    let YAxis =
        Pattern.Config "YAxis"
            {
                Required =
                    [
                        "graph", Obj 
                    ]
                Optional = 
                    [
                        "pixelsPerTick", Int
                        "ticksTreatment", String
                        "ticksFormat", Obj
                        "orientation", String
                    ]
            }
        |>! addToClassList

    let FixDurObj = 
        Pattern.Config "FixDurObj"
            {
            Required = 
                [
                    "timeInterval", Int
                    "maxDataPoints", Int
                    "timeBase", Int
                ]
            Optional = []
            }
         |>! addToClassList

    let FixDurArr = 
        Pattern.Config "FixDurArr"
            {
            Required = 
                [
                    "name", String
                ]
            Optional = []
            }
        |>! addToClassList


    let Rickshaw = 
        Class "Rickshaw"
        |+> Static [
            "namespace" => String * !?Obj ^-> Obj
            "keys" => !|Obj ^-> !|Obj
            "extend" => !|Obj ^-> !|Obj
            "clone" => Obj ^-> String
        ]
        |=> Nested [
            Class "Rickshaw.Graph"
            |+> Static [
                Constructor GraphData
            ]
            |+> Instance [
                "render" => O ^-> O
                "update" => O ^-> O
                "element" =@ Element
                "configure" => GraphData ^-> O
                "series" =@ !|Series + Obj
                "setRenderer" => String  ^-> O 
                "setSize" => Obj ^-> O
            ]
            |=> Nested [
                Class "Rickshaw.Graph.Ajax"
                |+> Static [
                    Constructor FileConfig
                ]
                Class "Rickshaw.Graph.JSONP"
                |+> Static [
                    Constructor FileConfig
                ]
                Class "Rickshaw.Graph.RangeSlider"
                |+> Static [
                    Constructor Slide
                ]
                |=> Nested [
                    Class "Rickshaw.Graph.RangeSlider.Preview"
                    |+> Static [
                        Constructor Slide
                    ]
                    |+> Instance [
                        "previews" =? !|Obj
                    ]
                ]
                Class "Rickshaw.Graph.HoverDetail"
                |+> Static [
                    Constructor Hover
                ]
                Class "Rickshaw.Graph.Annotate"
                |+> Static [
                    Constructor Legend
                ]
                |+> Instance [
                    "update" => O ^-> O
                    "add" => (Int * String) ^-> O
                ]
                Class "Rickshaw.Graph.Legend"
                |+> Static [
                    Constructor Legend
                ]
                Class "Rickshaw.Graph.Smoother"
                |+> Static [
                    Constructor Legend
                ]
                Class "Rickshaw.Graph.Behaviour"
                |=> Nested [
                    Class "Rickshaw.Graph.Behaviour.Series"
                    |=> Nested [
                        Class "Rickshaw.Graph.Behavior.Series.Toggle"
                        |+> Static [
                            Constructor GLegend
                        ]
                        Class "Rickshaw.Graph.Behavior.Series.Order"
                        |+> Static [
                            Constructor GLegend
                        ] |> Requires resourceList 
                        Class "Rickshaw.Graph.Behavior.Series.Highlight"
                        |+> Static [
                            Constructor GLegend
                        ]
                    ]
                ]
                Class "Rickshaw.Graph.Axis"
                |=> Nested [
                    Class "Rickshaw.Graph.Axis.Time"
                    |+> Static [
                        Constructor XAxis
                    ]
                    |+> Instance [
                         "render" => O ^-> O
                    ]
                    Class "Rickshaw.Graph.Axis.Y"
                    |+> Static [
                        Constructor YAxis
                    ]
                    |+> Instance [
                         "render" => O ^-> O
                    ]
                ]
            ]
            Class "Rickshaw.Fixtures" 
                |=> Nested [
                Class "Rickshaw.Fixtures.RandomData"
                |+> Static [
                    Constructor Int
                ]
                |+> Instance [
                    "addData" => !|(!|Coord) ^-> O
                    "removeData" => !|(!|Coord) ^-> O
                ]
                Class "Rickshaw.Fixtures.Time"
                |+> Static [
                    Constructor O
                ]
//                |+> Instance [
//                    "months" =? !|String
//                    "units" =? !|Obj
//                    "unit" => String ^-> !|Obj
//                    "formatDate" => DateTime ^-> Date
//                    "formatTime" => DateTime ^-> String
//                    "ceil" => DateTime * Obj ^-> Float
//                ]
                |=> Nested [
                    Class "Rickshaw.Fixtures.Time.Local"
                    |+> Static [
                        Constructor O
                    ]
                    
                ]
                Class "Rickshaw.Fixtures.Number"
                |+> Static [
                    "formatKMBT" => Float ^-> String
                    "formatBase1024KMGTP" => Float ^-> String
                ]
                Class "Rickshaw.Fixtures.Color"
                |+> Static [
                    "schemes"                =? !|String
                    "schemes.spectrum14"     =? !|String
                    "schemes.spectrum2000"   =? !|String
                    "schemes.spectrum2001"   =? !|String
                    "schemes.classic9"       =? !|String
                    "schemes.httpStatus"     =? !|Obj
                    "schemes.colorwheel"     =? !|String
                    "schemes.cool"           =? !|String
                    "schemes.munin"          =? !|String
                ]
            ]
            Class "Rickshaw.Color"
                |=> Nested [
                    Class "Rickshaw.Color.Palette"
                    |+> Static [
                        Constructor Scheme
                    ]
                    |+> Instance [
                        "color" => O ^-> String
                        "scheme" =? !|String
                        "interpolateColor" => O ^-> String
                    ]
                ]
            Class "Rickshaw.Series"
                |=> Nested [
                    Class "Rickshaw.Series.FixedDuration"
                    |+> Static [
                        Constructor (!|FixDurArr * String * FixDurObj)
                    ]
                    |+> Instance [
                        "addData" => Obj ^-> O
                    ]
                ]
        ] |>! addToClassList

    let RenderControls =
        Class "RenderControls"
        |+> Static [
            Constructor Legend
        ] |>! addToClassList
        





////
////    //??? Rickshaw.Class.create()???

////    let RickshawGraphAxisYScaled =
////        Class "Rickshaw.Graph.Axis.Y.Scaled"
////        |+> Static [
////            Constructor Graph
////        ]
////

    let RickshawAssembly =
        Assembly [
            Namespace "Websharper.Rickshaw" classList
            Namespace "Websharper.Rickshaw.Resources" [
                (Resource "Rickshaw" "https://cdnjs.cloudflare.com/ajax/libs/rickshaw/1.5.1/rickshaw.js").AssemblyWide()
                (Resource "RickshawCss" "https://cdnjs.cloudflare.com/ajax/libs/rickshaw/1.5.1/rickshaw.css").AssemblyWide()
                (Resource "D3" "https://cdnjs.cloudflare.com/ajax/libs/d3/3.5.16/d3.min.js").AssemblyWide()
                (Resource "jQuery.ui" "https://code.jquery.com/ui/1.11.3/jquery-ui.min.js").AssemblyWide()
                (Resource "jQuery-ui-css" "http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css").AssemblyWide()
            ]
            
        ]

[<Sealed>]
type RickshawExtension() =
    interface IExtension with
        member ext.Assembly = Definition.RickshawAssembly

[<assembly: Extension(typeof<RickshawExtension>)>]
do ()
