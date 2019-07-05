# ![octopus](https://github.com/victorduartedefreitas/Freitas.Octopus/blob/master/res/octopus_64.png) Freitas.Octopus
Generate position values for your collection items.

You can download this library on NuGet: Freitas.Octopus (https://www.nuget.org/packages/Freitas.Octopus)

INTRO

### Now, you may be wondering: how can I use Freitas.Octopus? Follow me, I'll teach you!

First of all, you have to create a listitem class (or modify your current listitem class) that implements `IOctopusItem` interface.
Then, add `System.Collections.Generic` and `Freitas.Octopus` referentes to your main class.

If your List is already filled, you can use the `InitializeOctopusOrdination` extension method to initialize all items with a `Position` code.

IMAGE

For each new item added to your list, you have to call `OctopusPositionGenerator.Instance.GeneratePositionValue` to generate a position value for that item.

IMAGE

To sort your list, you can use the `OctopusOrderBy` extension method for IList<T>, ICollection<T> and IEnumerable<T>.

IMAGE

