# WebSharper.Rickshaw

## Introduction

(The following is the introduction from [code.shutterstock.com][rickshaw] with samples translated to F#, and some comments on typing.)

Rickshaw is a handy tool, which allow you to create static and dynamic charts to a Document Object Model (DOM), which are actually well customisable. Rickshaw is based on the [D3][d3] library, and uses the [jQuery-ui][jqueryui] library.

This extension works in modern browsers and Internet Explorer 9+.

## Basic charts

Here is an example how to create a simple chart:

```
let series1 = Series( [|Coord(0,40); Coord(1,49); Coord(2,38); Coord(3,30); Coord(4,32)|], Color = "steelblue"
let series2 = Series( [|Coord(0,19); Coord(1,22); Coord(2,32); Coord(3,20); Coord(4,21)|], Color = "lightblue")
let chart = divAttr [attr.id "diagram"] []
let gdata = GraphData(Chart.Dom, [|series1; series2|], Renderer="line") 
let test = Rickshaw.Graph(GData)
test.Render()
```
`Rickshaw.Graph` takes a GraphData as an argument which can be constructed as gdata above. Required parameter is the DOM element, where the graph should be rendered, and the array of datas, and it can take a lot of additional options, such as the rendering shape, as it's shown above.

series1 and series2, which holds the data, is constructed by `Series`, which takes a list of `Coord`s (which needs an integer as an x coordinate, and a float or and integer as an y coordinate) as a must-have argument, and additionally you can set the color, the name and the scale of this data. To render the graph, just call the created graph's render method.

Optional GraphData features

* Renderer - Needs a string, which identifies, what kind of chart you are want to create. ("line", "bar", "area", "scatter")
* Width - Set the width of the chart
* Height - Set the height of the chart
* Interpolation - Set the style of the interpolation ("linear", "cardinal", "step", "basis")
* OnData - Provide a function which transforms the data
* Stroke - If this is setted to false, then the data will be generated, but not showed
* Strokewidth - Sets the width of the stroke (float)
* Preserve - Set this true, if you want to preserve the states of the chart (useful, when the data coming continuously, and you have a range slider(see below))
* Padding - Set paddings for the chart
* Min - Sets the minimum value of the y axis
* Max - Set the maximum value of the y axis
* Unstack - Setting this to false, makes your datas stacked on each other, so this will not show relative values of the data
* XScale - Sets the scale of the X axis
* YScale - Sets the scale of the Y axis

After creating the chart, it's possible to change the settings. To do this, just call the `Configure` method of the chart, and pass an objects of the modified settings to it.
Don't forget to use the `Render` function again.

With the `OnUpdate` method, you can provide a callback, which will be used when the chart renders.

## Color schemes

There are predefined colorschemes in this extension:

* spectrum14
* spectrum2000
* spectrum2001
* classic9
* colorwheel
* cool
* munin

Here is an example to how to use them.
```
let palette = Rickshaw.Color.Palette(Scheme("colorwheel"))
```
Now palette will be a color palette object, which holds the colors of this scheme. To get a color from this scheme, use `palette.Colors()`, which returns with the color as a string. This will also remove that color from palette, so when you call it next time, you will not get the same color again.

## Adding a legend

Creating a legend is just as simple as creating a graph:

```
let leg = divAttr [attr.id "legend"] []
let graphlegend = Rickshaw.Graph.Legend(Legend(graph, leg.Dom))
let toggle = Rickshaw.Graph.Behaviour.Series.Toggle(GLegend(graph, graphlegend))
let order = Rickshaw.Graph.Behaviour.Series.Order(GLegend(graph, graphlegend))
let highlight = Rickshaw.Graph.Behaviour.Series.Highlight(GLegend(graph, graphlegend))
```

To create a legend, you need to use `Rickshaw.Graph.Legend`, and pass it a Legend constructed data, as an argument. Legend takes two parameters. First parameter is the chart object, which to the legend will be attached, the second is the DOM elemet, where it will be created.

When you have a legend, you can apply three option to that. `Toggle`, so when you have multiple chart elements, you can toggle which should be visilbe, and which not. `Order`, so you can rearrange the stack on the chart. `Highlight`, so when you hover your mouse over one of the lines of the legend, it highlights that part of the chart. 

Theese three features can be created within the `Rickshaw.Graph.Series.Behaviour`, `Rickshaw.Graph.Series.Order`, `Rickshaw.Graph.Series.Toggle` and they takes a GLegend as an argument, which can be constructed by the chart and the legend objects.

## Hover details, Smoothing, Timeline

Let's see an example of hover details

```
let xformat = 
    fun x ->
        Date( x*1000 ).ToDateString()
        
 let HoverDetail = Rickshaw.Graph.HoverDetail(Hover(graph, XFormatter=xformat))
```

When using `Rickshaw.Graph.HoverDetail`, just pass an Hover constructed object as an argument. It's required parameter is the chart object, and you can provide optional parameters, like XFormatter, and YFormatter which are int to string functions, or the formatter function, which takes an (object, int, int) to string function.

```
let annotator = Rickshaw.Graph.Annotate(Legend(graph, timeline.Dom))
annotator.Add(5, "Hello")
annotator.Update()
```

The example above creates a timeline for your chart. This takes the same argument as the legend needed, so pass it a `Legend` constructed argument. To add message to the timeline, call the Add method of your `Annotate` object, and pass it an integer and a string. To show your message on the timeline, you have to call the `Update` method.

```
let smooth = divAttr [attr.id "smoother"] []
let smoother = Rickshaw.Graph.Smoother(Legend(graph, smooth.Dom))
```

This example creates a slide for your chart, with you can set the frequency of data on the chart with a slider.

## Pulling data from file

The chart can get the data via JSON or JSONP files.

```
let gdata = FileConfig(place.Dom, "data.json", Renderer="line", Width=400, Height=200)
let test = Rickshaw.Graph.Ajax(gdata)
```

This example creates a chart, where the data comes from a file. To create a chart like this, you need to use the `Rickshaw.Graph.Ajax`, which needs a `FileConfig` type as a constructor argument. `FileConfig` requires the DOM element, where the chart will be located, and the source file. An example of the data.json file:

```
[
	{
		"color": "blue",
		"name": "New York",
		"data": [ { "x": 0, "y": 40 }, { "x": 1, "y": 49 }, { "x": 2, "y": 38 }, { "x": 3, "y": 30 }, { "x": 4, "y": 32 } ]
	}, {
	    "color": "red",
		"name": "London",
		"data": [ { "x": 0, "y": 19 }, { "x": 1, "y": 22 }, { "x": 2, "y": 29 }, { "x": 3, "y": 20 }, { "x": 4, "y": 14 } ]
	}, {
	    "color": "green",
		"name": "Tokyo",
		"data": [ { "x": 0, "y": 8 }, { "x": 1, "y": 12 }, { "x": 2, "y": 15 }, { "x": 3, "y": 11 }, { "x": 4, "y": 10 } ]
	}
]
```

To create a chart with JSONP, use `Rickshaw.Graph.JSONP` similar way.

## Formatting the X and Y axis

```
let xa = Rickshaw.Graph.Axis.X(YAxis(graph, TicksTreatment="glow", TicksFormat=Rickshaw.Fixtures.Number.FormatKMBT))
xa.Render()
```
```
let xat = Rickshaw.Graph.Axis.Time(XAxis(graph, TicksTreatment="glow", TimeFixture=Rickshaw.Fixtures.Time.Local()))
xat.Render()
```
```
let ya = Rickshaw.Graph.Axis.Y(YAxis(yraph, TicksTreatment="glow", TicksFormat=Rickshaw.Fixtures.Number.FormatKMBT))
ya.Render()
```

Two types of X axis exists, there is a number based, and there is a time based. To create a number based, use the `Rickshaw.Graph.Axis.X`, to create a time based, use the `Rickshaw.Graph.Axis.Time`.

To create the Y axis, use the `Rickshaw.Graph.Axis.Y`. The two number based axis can be created the same way. Pass them an `Axis` type object, which requires the chart object, and optionally you can add TicksTreatment (string, "glow"/"inverse") and TicksFormat (see chapter below).

To create the time based one you need to pass an argument, which type is `TimeAxis`, which requires the chart object, and optionally TicksTreatment (string, "glow"/"inverse") and TimeFixture (see chapter below). You can optionally set the Orientation ("left", "right"/ "top", "bottom") and the PixelsPerTick (integer) in both cases.

You can create your own scale on the Y axis, with `Rickshaw.Graph.Axis.Y.Scaled`, which needs a `ScaledAxis` as a parameter, which differs from the `Axis` type only by requiring a Scaled parameter, which needs an object as parameter, which is working well with the scales from [D3][d3]

## Time and number fixtures

In the example above, you can set a TicksFormat, when creating the number based axis. Rickshaw provides two formats: `Rickshaw.Fixtures.Number.FormatKMBT` and `Rickshaw.Fixtures.Number.FormatBase1024KMGTP`, or a function, which takes a number and returns a string.

The time based axis can use the `Rickshaw.Fixtures.Time()`, or the `Rickshaw.Fixtures.Time.Local()` or function, which takes a date and returns a string.

## Using random data

```
let place = divAttr [attr.id "diagram"] []
let series = Rickshaw.Series.FixedDuration([|FixDurArr("one")|], JS.Undefined, FixDurObj(250, 100, Date().GetTime()/1000))
let graph = Rickshaw.Graph(GraphData(Place.Dom,series,Width=900,Height=500,Renderer="line"))
```

You can pass a `Rickshaw.Series.FixedDuration` object to a Graph, as instead of a Series object. This needs an array of `FixedDurArr`-s (which needs a string as an argument, which defines the name of that data), a string, which defines the color palette (you can use `JS.Undefined`, if you don't want to pass a colorscheme) and a `FixDurObj` which takes 3 int as a parameter (time interval, max data points and time base). To add data to this object, let's see this example:

```
let mutable I = 0.
JS.SetInterval( (fun () ->
        let randInt = Math.Random()*100.
        I <- (I + 1.)
        let data = New [
            "one" => Math.Random() * 40. + 120.
            "two" => (Math.Sin( I / 40.) + 4.) * (randInt + 400.)
            "three" => randInt + 300.
        ]
        series.AddData(data)
        graph.Update() 
    )) 250 |> ignore
```

You can add data with the `AddData` function, which requires an object as a parameter. Just pass an anonym object, which can be the constructed with the `New` keyword. This takes a list, where the property names will be the name of each line, and the given values will be the y values of the chart. After adding data, update the graph to render.

[rickshaw]: http://code.shutterstock.com/rickshaw
[d3]: http://d3js.org
[jqueryui]: https://jqueryui.com/

