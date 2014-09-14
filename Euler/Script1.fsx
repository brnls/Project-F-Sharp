#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Net.Http.dll"
#r @"E:\GitHub\Project F Sharp\packages\HtmlAgilityPack.1.4.6\lib\Net45\HtmlAgilityPack.dll"
open System
open System.Net
open System.Net.Http
open HtmlAgilityPack
open System.Linq


let client = new HttpClient()

let url = @"http://www.cboe.com/products/indexcomponents.aspx?DIR=OPIndexComp&FILE=snp100.doc"

let fullHtml = client.GetStringAsync(url).Result
let node = HtmlNode.CreateNode(fullHtml)

let tickers = 
    node.SelectNodes("//table/tbody/tr")
    |>Seq.map(fun x-> x.ChildNodes)
    |>Seq.map(fun x -> x.Skip(3).First().InnerText)
    |>Seq.skip 1
    |>Array.ofSeq


tickers.[0..10]
    


