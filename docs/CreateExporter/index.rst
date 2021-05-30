===============
Create Exporter
===============

Goal
====
The goal is to create an exporter that exports like this:

.. code-block::

    data(
        value: 1, 2, "string"
        childData:
          data2(
              value: 3, 4
          )
          data2(
              value: 5, 6
          )
    )
    
See `Guides of formatConfig and schema </GuidesOfFormatConfigAndSchema>`_ about the formatConfig and schema.

The sample codes are F# script.

Create the Exporter
===================

First exporter
--------------

Code
^^^^
.. code-block:: fsharp

    #r "nuget: Dataf, 1.0.0-beta4"

    open Dataf
    open Dataf.Computation


    // Create exporter
    let sc = fun _ -> "<(name)>(
        value: <(values)>
        childData:
            <(children)>
    )"

    let e =
        euild() {
            schema sc
        }


    // Create data
    let data =
        fuild() {
            name "data"
            values [| 1; 2; "string" |]
            addChild (fu{ name "data2"; values [| 3; 4 |] })
            addChild (fu{ name "data2"; values [| 5; 6 |] })
        }

    // Export the data
    e.export data
        |> printfn "%s"

Output
^^^^^^
.. code-block::

    data(
        value: "1""2""string"
        childData:
            data2(
        value: "3""4"
        childData:
            
    )data2(
        value: "5""6"
        childData:
            
    )
    )

Add indent and separator
------------------------
Code
^^^^
.. code-block:: fsharp

    #r "nuget: Dataf, 1.0.0-beta4"

    open Dataf
    open Dataf.Computation


    // Create exporter
    let sc = fun _ -> "<(name)>(
        value: <(values)>
        childData:
          <(children)>
    )"

    let e =
        euild() {
            schema sc
            child (cu{ indent "      "; separator "\n" }) // Added this
        }


    // Create data
    let data =
        fuild() {
            name "data"
            values [| 1; 2; "string" |]
            addChild (fu{ name "data2"; values [| 3; 4 |] })
            addChild (fu{ name "data2"; values [| 5; 6 |] })
        }

    // Export the data
    e.export data
        |> printfn "%s"


Output
^^^^^^
.. code-block::

    data(
        value: "1""2""string"
        childData:
          data2(
              value: "3""4"
              childData:
                
          )
          data2(
              value: "5""6"
              childData:
                
          )
    )



Change "values" format
----------------------

Code
^^^^

.. code-block:: fsharp

    #r "nuget: Dataf, 1.0.0-beta4"

    open Dataf
    open Dataf.Computation


    // Create exporter
    let sc = fun _ -> "<(name)>(
        value: <(values)>
        childData:
          <(children)>
    )"

    let e =
        euild() {
            schema sc
            child (cu{ indent "      "; separator "\n" })
            value (cu{ separator ", " }) // Added this
            dataStyle // Added this
                (fun data ->
                    match data with
                    | :? int -> data.ToString()
                    | _      -> "\"" + data.ToString() + "\""
                )
        }


    // Create data
    let data =
        fuild() {
            name "data"
            values [| 1; 2; "string" |]
            addChild (fu{ name "data2"; values [| 3; 4 |] })
            addChild (fu{ name "data2"; values [| 5; 6 |] })
        }

    // Export the data
    e.export data
        |> printfn "%s"

Output
^^^^^^

.. code-block::

    data(
        value: 1, 2, "string"
        childData:
          data2(
              value: 3, 4
              childData:
                
          )
          data2(
              value: 5, 6
              childData:
                
          )
    )



Add the conditions
------------------

Code
^^^^

.. code-block:: fsharp

    #r "nuget: Dataf, 1.0.0-beta4"

    open Dataf
    open Dataf.Computation


    // Create exporter
    let sc = fun _ -> "<(name)>(`v
        value: <(values)>v``c
        childData:
          <(children)>c`
    )" // Changed this

    let e =
        euild() {
            schema sc
            child (cu{ indent "      "; separator "\n" })
            value (cu{ separator ", " })
            dataStyle
                (fun data ->
                    match data with
                    | :? int -> data.ToString()
                    | _      -> "\"" + data.ToString() + "\""
                )
        }


    // Create data
    let data =
        fuild() {
            name "data"
            values [| 1; 2; "string" |]
            addChild (fu{ name "data2"; values [| 3; 4 |] })
            addChild (fu{ name "data2"; values [| 5; 6 |] })
        }

    // Export the data
    e.export data
        |> printfn "%s"

Output
^^^^^^

.. code-block::

    data(
        value: 1, 2, "string"
        childData:
          data2(
              value: 3, 4
          )
          data2(
              value: 5, 6
          )
    )
