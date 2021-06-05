module FsDatali.Exporters

open FsDatali.Computation


let XMLExporter =
    let f = fun _ -> $"""<<(name)>`a a`<(attributes)>>`v
    v`<(values)>`c
    c`<(children)>
</<(name)>>"""

    eu{
        schema f
        value (cu{ separator "\n"; indent "    " })
        attribute (cu{ before "\""; after "\""; separator " "; connector "\"=\"" })
        child (cu{ separator "\n"; indent "    " })
        dataStyle
            (fun data -> data.ToString())
    }


let JSONExporter =
    let f = fun _ -> """{
  "<(name)>":{`v
    "values": [<(values)>]`c,c`v``c
    "children": [
      <(children)>
    ]c`
  }
}"""

    eu{
        schema f
        value (cu{ separator "," })
        child (cu{ separator ",\n"; indent "      " })
        dataStyle
            (fun data ->
                match data with
                | :? int -> data.ToString()
                | _ -> "\"" + data.ToString() + "\"")
        replaceCount 2
    }
