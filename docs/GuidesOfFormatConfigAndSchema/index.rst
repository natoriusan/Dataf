=================================
Guides of formatConfig and schema
=================================

formatConfig
============

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


| ``separator`` is placed between each element.
| ``first`` is placed before the tag.
| ``last`` is placed after the tag.
| ``before`` is placed before each element.
| ``after`` is placed after each element.
| ``indent`` is placed on the beginning of each lines except first line.
| ``connector`` is placed between the name and value in each attribute. This is used only in attributes


See also
^^^^^^^^

`formatConfig </ClassDocuments#formatconfig>`_ on Class Documents


Schema
======

Tags
----
| ``<(name)>`` is replaced with name.
| ``<(attributes)>`` is replaced with attributes.
| ``<(children)>`` is replaced with children.
| ``<(values)>`` is replaced with values.
| ```n...n``` is replaced with ... if the length of the name is more than 0. It is replaced with "" otherwise.
| ```a...a``` is replaced with ... if the number of attributes is more than 0. It is replaced with "" otherwise.
| ```c...c``` is replaced with ... if the number of child data is more than 0. It is replaced with "" otherwise.
| ```v...v``` is replaced with ... if the number of values is more than 0. It is replaced with "" otherwise.
