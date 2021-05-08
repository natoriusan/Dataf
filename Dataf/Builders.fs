module Dataf.Computation


type fuild(?targetData:dataf) =
    member this.Yield (_) =
        match targetData with
        | Some d -> d
        | None   -> dataf()
        
    [<CustomOperation ("attributes")>]
    member this.attributes(dataObj:dataf, attributes) =
        dataObj.attributes <- attributes
        dataObj

    [<CustomOperation ("addAttribute")>]
    member this.addAttribute(dataObj:dataf, attr) =
        dataObj.addAttribute attr
        dataObj

    [<CustomOperation ("children")>]
    member this.children(dataObj:dataf, children) =
        dataObj.childData <- children
        dataObj

    [<CustomOperation ("addChild")>]
    member this.addChild(dataObj:dataf, child:dataf) =
        dataObj.addChild child.name child
        dataObj

    [<CustomOperation ("name")>]
    member this.name(dataObj:dataf, name) =
        dataObj.name <- name
        dataObj

    [<CustomOperation ("values")>]
    member this.values(dataObj:dataf, values) =
        dataObj.values <- values
        dataObj

    [<CustomOperation ("addValue")>]
    member this.addValue(dataObj:dataf, value) =
        dataObj.addValue value
        dataObj


        
type euild(?targetExporter:exporter) =
    member this.Yield (_) =
        match targetExporter with
        | Some e -> e
        | None   -> exporter()

    [<CustomOperation ("attribute")>]
    member this.attribute(e:exporter, attr) =
        e.attribute <- attr
        e

    [<CustomOperation ("child")>]
    member this.child(e:exporter, child) =
        e.child <- child
        e

    [<CustomOperation ("dataStyle")>]
    member this.datastyle(e:exporter, dStyle) =
        e.dataStyle <- dStyle
        e

    [<CustomOperation ("replaceCount")>]
    member this.replaceCount(e:exporter, reCount) =
        e.replaceCount <- reCount
        e

    [<CustomOperation ("schema")>]
    member this.schema(e:exporter, schema) =
        e.schema <- schema
        e

    [<CustomOperation ("value")>]
    member this.value(e:exporter, value) =
        e.value <- value
        e



type cuild(?targetFormatConfig:formatConfig) =
    member this.Yield (_) =
        match targetFormatConfig with
        | Some f -> f
        | None   -> formatConfig()

    [<CustomOperation ("separator")>]
    member this.datastyle(f:formatConfig, sep) =
        f.separator <- sep
        f

    [<CustomOperation ("first")>]
    member this.first(f:formatConfig, first) =
        f.first <- first
        f

    [<CustomOperation ("last")>]
    member this.last(e:formatConfig, last) =
        e.last <- last
        e

    [<CustomOperation ("before")>]
    member this.before(e:formatConfig, before) =
        e.before <- before
        e

    [<CustomOperation ("after")>]
    member this.after(e:formatConfig, after) =
        e.after <- after
        e

    [<CustomOperation ("indent")>]
    member this.indent(e:formatConfig, indent) =
        e.indent <- indent
        e

    [<CustomOperation ("connector")>]
    member this.connector(e:formatConfig, cn) =
        e.connector <- cn
        e
        


let fu = fuild()
let eu = euild()
let cu = cuild()
