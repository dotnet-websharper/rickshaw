#load "tools/includes.fsx"
open IntelliFactory.Build

let bt = BuildTool().PackageId("WebSharper.Rickshaw")
            .VersionFrom("WebSharper")
            .WithFSharpVersion(FSharp30)
            .WithFramework(fun fw -> fw.Net40)

let main = 
    bt.WebSharper4.Extension("WebSharper.Rickshaw")
        .SourcesFromProject()

(*
let simple = 
    bt.WebSharper4.BundleWebsite("Simple")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let ajax = 
    bt.WebSharper4.BundleWebsite("Ajax")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let bars = 
    bt.WebSharper4.BundleWebsite("Bars")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let colors = 
    bt.WebSharper4.BundleWebsite("Colors")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let extensions = 
    bt.WebSharper4.BundleWebsite("Extensions")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let fixedduration = 
    bt.WebSharper4.BundleWebsite("FixedDuration")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("WebSharper.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])
*)

bt.Solution [
    main
(*
    ajax
    bars
    colors
    extensions
    simple
    fixedduration
*)

    bt.NuGet.CreatePackage()
        .Add(main)
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Rickshaw-1.5.1"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.rickshaw"
                Description = "WebSharper Extensions for Rickshaw 1.5.1"
                RequiresLicenseAcceptance = true })
]
|> bt.Dispatch
