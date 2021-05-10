namespace Dataf

open System
open System.Text.RegularExpressions
open type System.Text.RegularExpressions.Regex


type dataf (?values:obj array, ?attributes:(string * string) array) =
    let mutable lchildData:dataf array = [||]
    let mutable lname = ""
    let mutable lvalues =
        match values with
            | Some v -> v
            | None   -> [||]
    let mutable lattr =
        match attributes with
            | Some attr -> attr
            | None      -> [||]


    member this.addValue value =
        lvalues <- Array.append lvalues [| value |]

    member this.removeValue index =
        lvalues <- Array.append lvalues.[..index-1] lvalues.[index+1..]

    member this.addAttribute (attr:string * string) =
        lattr <- Array.append lattr [| attr |]

    member this.removeAttribute name =
        for i in 0..lattr.Length-1 do
            if i < lattr.Length then
                match lattr.[i] with
                    | (n, _) when n = name -> lattr <- Array.append lattr.[..i-1] lattr.[i+1..]
                    | _                    -> 0 |> ignore

    member this.addChild name (value:dataf) =
        if value.name = "" then
            value.name <- name
        lchildData <- Array.append lchildData [| value |]

    member this.removeChild name =
        for i in 0..lchildData.Length do
            if i < lchildData.Length && lchildData.[i].name = name then
                lchildData <- Array.append lchildData.[..i-1] lchildData.[..i+1]


    member this.name
        with get ()    = lname
        and  set value = lname <- value

    member this.values
        with get ()    = lvalues
        and  set value = lvalues <- value

    member this.attributes
        with get ()    = lattr
        and  set value = lattr <- value

    member this.childData
        with get () = lchildData
        and  set value = lchildData <- value

    member this.childNames =
        [| for i in lchildData -> i.name |]

    member this.Item
        with get index =
            lchildData.[Array.IndexOf(this.childNames, index)]

        and  set index (value:dataf) =
            let arrayIndex = Array.IndexOf(this.childNames, index)
            if arrayIndex = -1 then
                if value.name = "" then
                    value.name <- index
                lchildData <- Array.append lchildData [|value|]
            else
                lchildData.[arrayIndex] <- value



type formatConfig (?separator:string, ?first:string, ?last:string, ?before:string, ?after:string, ?indent:string, ?connector:string) =
    let mutable lseparator = match separator with | Some s -> s | None -> ""
    let mutable lfirst     = match first with     | Some s -> s | None -> ""
    let mutable llast      = match last with      | Some s -> s | None -> ""
    let mutable lbefore    = match before with    | Some s -> s | None -> ""
    let mutable lafter     = match after with     | Some s -> s | None -> ""
    let mutable lindent    = match indent with    | Some s -> s | None -> ""
    let mutable lconnector = match connector with | Some s -> s | None -> ""

    member this.separator
        with get () = lseparator
        and  set value = lseparator <- value
    member this.first
        with get () = lfirst
        and  set value = lfirst <- value
    member this.last
        with get () = llast
        and  set value = llast <- value
    member this.before
        with get () = lbefore
        and  set value = lbefore <- value
    member this.after
        with get () = lafter
        and  set value = lafter <- value
    member this.indent
        with get () = lindent
        and  set value = lindent <- value
    member this.connector
        with get () = lconnector
        and  set value = lconnector <- value



type exporter () =
    let mutable lreplaceCount = 1

    let mutable lvalue = formatConfig ()
    let mutable lattr  = formatConfig ()
    let mutable lchild = formatConfig ()

    let mutable lschema:dataf -> string =
        fun dataObj ->
            ""
    let mutable ldataStyle:obj -> string =
        fun o ->
            match o with
                | _ -> "\"" + o.ToString () + "\""


    member this.export (dataObj:dataf) =
        let mutable str = this.schema (dataObj)

        // replace `nn`, `aa`, `cc` and `vv`

        let mutable replaceConditions = [||]
        let mutable removeConditions = [||]
        
        let addReplaceConditions tagName =
            replaceConditions <- Array.append replaceConditions [| $"{tagName}(?<value>.*?){tagName}" |]
        let addRemoveConditions tagName =
            removeConditions <- Array.append removeConditions [| $"{tagName}(?<value>.*?){tagName}" |]

        if dataObj.name.Length > 0       then addReplaceConditions "n" else addRemoveConditions "n"
        if dataObj.attributes.Length > 0 then addReplaceConditions "a" else addRemoveConditions "a"
        if dataObj.childData.Length > 0  then addReplaceConditions "c" else addRemoveConditions "c"
        if dataObj.values.Length > 0     then addReplaceConditions "v" else addRemoveConditions "v"

        let replaceRegex = Regex($"""(?<=([^nacv`]|^)(``)*|[nacv](``)*`)`({ String.concat "|" replaceConditions })`(?=`(``)*[nacv]|(``)*([^`]|$))""", RegexOptions.Singleline)
        let removeRegex  = Regex($"""(?<=([^nacv`]|^)(``)*|[nacv](``)*`)`({ String.concat "|" removeConditions  })`(?=`(``)*[nacv]|(``)*([^`]|$))""", RegexOptions.Singleline)

        for _ in 0..lreplaceCount do
            str <- replaceRegex.Replace(str, fun (m:Match) -> m.Groups.["value"].Value)
            str <- removeRegex.Replace(str, "")

        str <- Replace(str, "``", "`")


        // replace <(name)>, <(values)>,  <(attributes)> and <(children)>

        let addIndent (indent:string) (str:string) =
            str.Split '\n' |> String.concat ("\n" + indent)

        let replace (pattern:string) (replacement:string) (input:string) =
            Replace(input, pattern, replacement)

        let values =
            let v = this.value
            let vstr =
                [|
                    for i in dataObj.values -> v.before + ldataStyle (i) + v.after
                |]
                |> String.concat v.separator |> addIndent v.indent

            v.first + vstr + v.last

        let attr =
            let a = this.attribute
            let astr =
                [|
                    for i in dataObj.attributes ->
                        a.before +
                        (match i with | (n, v) -> n + a.connector + v) +
                        a.after
                |]
                |> String.concat a.separator |> addIndent a.indent

            a.first + astr + a.last

        let child =
            let c = this.child
            let cstr =
                [|
                    for i in dataObj.childData ->
                        c.before + this.export i + c.after
                |]
                |> String.concat c.separator |> addIndent c.indent

            c.first + cstr + c.last


        str <-
            replace "<\( *name *\)>" dataObj.name str
            |> replace "<\( *values *\)>" values
            |> replace "<\( *attributes *\)>" attr
            |> replace "<\( *children *\)>" child


        str


    member this.schema
        with get ()    = lschema
        and  set value = lschema <- value

    member this.replaceCount
        with get ()    = lreplaceCount
        and  set value = lreplaceCount <- value

    member this.dataStyle
        with get ()    = ldataStyle
        and  set value = ldataStyle <- value

    member this.value
        with get () :formatConfig = lvalue
        and  set value            = lvalue <- value

    member this.attribute
           with get () :formatConfig = lattr
           and  set value            = lattr <- value

    member this.child
           with get () :formatConfig = lchild
           and  set value            = lchild <- value
