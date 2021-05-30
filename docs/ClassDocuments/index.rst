===============
Class Documents
===============

dataf
=====
**Full name:** Dataf.dataf

Initialization
--------------

With Class
^^^^^^^^^^
.. code-block:: fsharp

    dataf(values:obj array, attributes:(string * string) array)

| ``values``: The array of values (optional)
| ``attributes``: The array of tuple of combination of name and value (optional)


With Computation Expression
^^^^^^^^^^^^^^^^^^^^^^^^^^^
.. code-block:: fsharp

    fuild(targetData:dataf) {
        attributes   attr:(string * string) array
        addAttribute attr:string * string
        children     ch  :dataf array
        addChild     ch  :dataf
        values       vl  :obj array
        addValue     vl  :obj
        name         nm  :string
    }

    // Or fu {...}

| ``targetData``: The data to make changes (optional)

| ``attributes``: Set attributes
| ``addAttribute``: Add an attribute
| ``children``: Set child data
| ``addChild``: Add child data
| ``values``: Set values
| ``addValue``: Add a value
| ``name``: Set name of the data


Properties
----------
.. code-block:: fsharp

    member name      :string                  // get; set
    member attributes:(string * string) array // get; set
    member childData :dataf array             // get; set
    member childNames:string array            // get
    member values    :obj array               // get; set

| ``name``: The name of data
| ``attributes``: The array of the attributes
| ``childData``: The array of the child data
| ``childNames``: The array of the names of child data
| ``values``: The array of the values


Methods
-------

.. code-block:: fsharp

    member addValue       :'a -> unit
    member removeValue    :int -> unit
    member addAttribute   :(string * string) -> unit
    member removeAttribute:string -> unit
    member addChild       :string -> dataf -> unit
    member removeChild    :string -> unit



| ``addValue``: Add a value
| ``removeValue``: Remove the value by its index
| ``addAttribute``: Add an attribute
| ``removeAttribute``: Remove the attribute by its name
| ``addChild``: Add the child data
| ``removeChild``: Remove the child data by its name




formatConfig
============
**Full name:** Dataf.formatConfig


Initialization
--------------

With Class
^^^^^^^^^^

.. code-block:: fsharp

    formatConfig(separator:string, first:string, last:string, before:string, after:string, indent:string, connector)


With Computation Expression
^^^^^^^^^^^^^^^^^^^^^^^^^^^

.. code-block:: fsharp

    cuild(targetFormatConfig:formatConfig) {
        separator sp :string
        first     f  :string
        last      l  :string
        before    bef:string
        after     aft:string
        indent    i  :string
        connector ct :string
    }

    // Or cu {...}


Properties
----------
.. code-block:: fsharp

    member separator:string // get; set
    member fist     :string // get; set
    member last     :string // get; set
    member before   :string // get; set
    member after    :string // get; set
    member indent   :string // get; set
    member connector:string // get; set




exporter
========
**Full name:** Dataf.exporter

Initialization
--------------

With Class
^^^^^^^^^^

.. code-block:: fsharp

    exporter()


With Computation Expression
^^^^^^^^^^^^^^^^^^^^^^^^^^^

.. code-block:: fsharp

    euild(targetExporter:exporter) {
        attribute    attrConf :formatConfig
        child        childConf:formatConfig
        value        valueConf:formatConfig
        dataStyle    dStyle   :obj -> string
        replaceCount rCount   :int
        schema       sc       :dataf -> string
    }

    // Or eu {...}


| ``targetExporter``: The exporter to make changes (optional)

| ``attribute``: Set the config class about attribute format
| ``child``: Set the config class about childData format
| ``value``: Set the config class about value format
| ``dataStyle``: Set the function to select data style
| ``replaceCount``: Set the number of times the exporter replaces
| ``schema``: Set the function that returns a schema for exporting data



Properties
----------

.. code-block:: fsharp

    member attribute   :formatConfig    // get; set
    member child       :formatConfig    // get; set
    member value       :formatConfig    // get; set
    member dataStyle   :obj -> string   // get; set
    member replaceCount:int             // get; set
    member schema      :dataf -> string // get; set


| ``attribute``: The config class about attribute format.
| ``child``: The config class about child format.
| ``value``: The config class about value format.
| ``replaceCount``: The number of times the exporter replaces.
| ``dataStyle``: The function to select data style.
| ``schema``: The function that returns a schema for exporting data.

Methods
-------

.. code-block:: fsharp

    member export:dataf -> string

| ``export``: Export given data as string.
