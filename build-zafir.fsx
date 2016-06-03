#load "tools/includes.fsx"
open IntelliFactory.Build

let bt = BuildTool().PackageId("Zafir.Rickshaw")
            .VersionFrom("Zafir")
            .WithFSharpVersion(FSharp30)
            .WithFramework(fun fw -> fw.Net40)

let main = 
    bt.Zafir.Extension("WebSharper.Rickshaw")
        .SourcesFromProject()

(*
let simple = 
    bt.Zafir.BundleWebsite("Simple")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let ajax = 
    bt.Zafir.BundleWebsite("Ajax")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let bars = 
    bt.Zafir.BundleWebsite("Bars")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let colors = 
    bt.Zafir.BundleWebsite("Colors")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let extensions = 
    bt.Zafir.BundleWebsite("Extensions")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
                r.Project(main)
            ])

let fixedduration = 
    bt.Zafir.BundleWebsite("FixedDuration")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.NuGet("Zafir.UI.Next").Latest(true).Reference()
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