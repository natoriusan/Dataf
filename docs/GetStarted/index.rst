===========
Get Started
===========

The following codes are omitted the definitions of EntryPoint function.

Load from NuGet
===============

F#
--
.. code-block::

    $ dotnet add package Dataf --version 1.0.0


F# Script
---------
.. code-block:: text

    #r "nuget: Dataf, 1.0.0"



Create Data
===========
.. code-block:: fsharp

    open Dataf
    open Dataf.Computation

    // fuild (dataf-build)
    let data = 
        fuild() {
            name "data"
        }

    // Add child data
    data.["childData"] <-
        // fu is an abbreviation for fuild()
        fu {
            values [| 1; 2; "string" |]
        }

    // Pass target data as an argument
    fuild(data) {
        name "data2"
    } |> ignore
    // data.name is "data2"


See also
--------

`Create Exporter </CreateExporter>`_
