namespace Websharper.Rickshaw

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator
open WebSharper.JavaScript.Dom
open WebSharper.JQuery

module Definition =

    //Rickshaw series


    let mutable classList = [] : CodeModel.NamespaceEntity list

    let addToClassList c =
        classList <- upcast c :: classList


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

    //do this with JSONP too, onComplete
    let AjaxConfig = 
        Pattern.Config "AjaxConfig" 
            {
                Required = 
                    [
                        "element", Element
                        "dataURL", String
                        "series", !|Obj
                    ] 
                Optional = 
                    [
                        "width", Int
                        "height", Int
                        "renderer", String
                        "onData", !|Obj ^-> !|Obj
                        "interpolation", String
                        "graph", Obj
                        "stroke", Bool
                        "preserve", Bool
                        "padding", Obj
                        "min", String
                        "unstack", Bool
                    ]
            }

    let Graph = 
        Pattern.Config "Graph"
            {
                Required = 
                    [
                        "element", Element
                        "series", !|Obj
                    ]
                Optional =
                    [
                        "width", Int
                        "height", Int
                        "interpolation", String
                        "renderer", String
                        "stroke", Bool
                        "preserve", Bool
                        "padding", Obj
                        "min", String
                        "unstack", Bool
                    ]
            }

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

    let Slider =
        Pattern.Config "Slider"
            {
                Required =
                    [
                        "element", Element
                        "graph", Obj
                    ]
                Optional = []
            }

    let Hover =
        Pattern.Config "Hover"
            {
                Required =
                    [
                        "graph", Obj
                    ]
                Optional = 
                    [
                        "formatter", !|Obj * Int * Int ^-> String
                        "xFormatter", Int ^-> String
                    ]
            }

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
    // Something still misses here
    let XAxis =
        Pattern.Config "XAxis"
            {
                Required =
                    [
                        "graph", Obj
                    ]
                Optional = 
                    [
                        "ticksTreatment", String
                        "timeFixture", Obj
                    ]
            }

    let YAxis =
        Pattern.Config "YAxis"
            {
                Required =
                    [
                        "graph", Obj 
                    ]
                Optional = 
                    [
                        "ticksTreatment", String
                        "ticksFormat", Obj
                    ]
            }
       
//    let TimeFixture = Pattern.Config "TimeFixture" {Required = ["timeFixture", Obj]; Optional = []}


    let Rickshaw = 
        Class "Rickshaw"
        |+> Static [
            "namespace" => String * !?Obj ^-> Obj
            "keys" => !|Obj ^-> !|Obj
            "extend" => !|Obj ^-> !|Obj
            "clone" => Obj ^-> String
        ]

//    let RickshawCompatClassList =
//        Class "Rickshaw.Compat.ClassList"
//        |+> Static [
            
    let RickshawGraph = 
        Class "Rickshaw.Graph"
        |+> Static [
            Constructor Graph
//            "initialize" => !|Element ^-> O
//            "_loadRenderers" => O ^-> O
//            "validateSeries" => !|Obj ^-> O
//            "dataDomain" => O ^-> Float2T
//            "discoverRange" => O ^-> O
            "render" => O ^-> O
            "update" => O ^-> O
//            "stackData" => O ^-> 
//            "_validateStackable" => O ^-> O
//            "_slice" => Int ^-> Bool
//            "onUpdate" => Obj ^-> O
//            "onConfigure" => Obj ^-> O
//            "registerRenderer" => Obj ^-> O
            "configure" => Obj ^-> O
//            "setRenderer" => *  ^-> O 
//            "setSize" => Obj ^-> O
        ]
//    
//
//    let RickshawFixturesColor = 
//        Class "Rickshaw.Fixtures.Color"
//        |+> Static [
//            "schemes"                =? !|String
//            "schemes.spectrum14"     =? !|String
//            "schemes.spectrum2000"   =? !|String
//            "schemes.spectrum2001"   =? !|String
//            "schemes.classic9"       =? !|String
//            "schemes.httpStatus"     =? !|Obj
//            "schemes.colorwheel"     =? !|String
//            "schemes.cool"           =? !|String
//            "schemes.munin"          =? !|String
//        ]
//
    let RickshawFixturesRandomData =
        Class "Rickshaw.Fixtures.RandomData"
        |+> Static [
            Constructor Int
            "addData" => !|String ^-> O
            "removeData" => !|String ^-> O
        ]
//
    let RickshawFixturesTime =
        Class "Rickshaw.Fixtures.Time"
        |+> Static [
            "months" =? !|String
            "units" =? !|Obj
            "unit" => String ^-> !|Obj
            "formatDate" => DateTime ^-> Date
            "formatTime" => DateTime ^-> String
            "ceil" => DateTime * Obj ^-> Float
        ]

    let RickshawFixturesTimeLocal =
        Class "Rickshaw.Fixtures.Time.Local"
        |=> Inherits RickshawFixturesTime
//
//      
    let RickshawFixturesNumber =
        Class "Rickshaw.Fixtures.Number"
        |+> Static [
            "formatKMBT" => Float ^-> String //?
            "formatBase1024KMGTP" => Float ^-> String //?
        ]
//
    let RickshawColorPalatte =
        Class "Rickshaw.Fixtures.Palette"
        |+> Static [
            Constructor Scheme
            "color" => Int ^-> String
            "interpolateColor" => O ^-> String
        ]

    let RickshawGraphAjax = 
        Class "Rickshaw.Graph.Ajax"
        |+> Static [
            Constructor AjaxConfig
        ]
        
    let RickshawGraphAxisY = 
        Class "Rickshaw.Graph.Axis.Y"
        |+> Static [
            Constructor YAxis
            "render" => O ^-> O
        ]
//
    let RickshawGraphLegend =
        Class "Rickshaw.Graph.Legend"
        |+> Static [
            Constructor Legend
        ]
//
    let RickshawGraphBehaviorSeriesToggle =
        Class "Rickshaw.Graph.Behavior.Series.Toggle"
        |+> Static [
            Constructor GLegend
        ]

    let RickshawGraphBehaviorSeriesOrder =
        Class "Rickshaw.Graph.Behavior.Series.Order"
        |+> Static [
            Constructor GLegend
        ]

    let RickshawGraphBehaviorSeriesHighlight =
        Class "Rickshaw.Graph.Behavior.Series.Highlight"
        |+> Static [
            Constructor GLegend
        ]
//
    let RickshawGraphSmoother =
        Class "Rickshaw.Graph.Smoother"
        |+> Static [
            Constructor Legend
        ]
//
    let RickshawGraphRangeSlider = 
        Class "Rickshaw.Graph.Range.Slider"
        |+> Static [
            Constructor Slider
        ]
//
    let RickshawGraphHoverDetail =
        Class "Rickshaw.Graph.Hover.Detail"
        |+> Static [
            Constructor Hover
        ]
//
    let RickshawGraphAnnotate =
        Class "Rickshaw.Graph.Annotate"
        |+> Static [
            Constructor Legend
        ]
// van render függvénye
    let RickshawGraphAxisTime =
        Class "Rickshaw.Graph.Axis.Time"
        |+> Static [
            Constructor XAxis
            "render" => O ^-> O
        ]
//
//    let AddAnnotation = Method "addAnnotation" (Bool ^-> O)
//
    let RickshawSeriesFixedDuration =
        Class "Rickshaw.Series.FixedDuration"
        |+> Static [
            Constructor Asd
            "addData" => Obj  ^-> O
        ]
//
//    //??? Rickshaw.Class.create()???
//
//    let RickshawGraphJSONPStatic = 
//        Class "Rickshaw.Graph.JSONP.Static"
//        |+> Static [
//            Constructor ElementNeed
//        ]
//
//
//    let RickshawGraphAxisYScaled =
//        Class "Rickshaw.Graph.Axis.Y.Scaled"
//        |+> Static [
//            Constructor Graph
//        ]
//
    let RickshawGraphRangeSliderPreview =
        Class "Rickshaw.Graph.RangeSlider.Preview"
        |+> Static [
            Constructor Legend
        ]

    let TransformData = Method "transformData" (!|Obj ^-> !|Obj)

    let RickshawSeriesZeroFill = Method "Rickshaw.Series.zeroFill" (!|Obj ^-> !|Obj)
 


    let I1 =
        Interface "I1"
        |+> [
                "test1" => T<string> ^-> T<string>
                "radius1" =@ T<float>
            ]

    let I2 =
        Generic - fun t1 t2 ->
            Interface "I2"
            |+> [
                    Generic - fun m1 -> "foo" => m1 * t1 ^-> t2
                ]

    let C1 =
        Class "C1"
        |+> Instance [
                "foo" =@ T<int>
            ]
        |+> Static [
                Constructor (T<unit> + T<int>)
                "mem"   => (T<unit> + T<int> ^-> T<unit>)
                "test2" => (TSelf -* T<int> ^-> T<unit>) * T<string> ^-> T<string>
                "radius2" =? T<float>
                |> WithSourceName "R2"
            ]

    let RickshawAssembly =
        Assembly [
            Namespace "Websharper.Rickshaw" classList
            Namespace "WebSharper.Rickshaw.Resources" [
                (Resource "Rickshaw" "rickshaw.min.js").AssemblyWide()
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.RickshawAssembly

[<assembly: Extension(typeof<Extension>)>]
do ()
