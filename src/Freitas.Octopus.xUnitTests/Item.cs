namespace Freitas.Octopus.xUnitTests
{
    public class Item : IOctopusItem
    {
        public Item(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            return $"Item: {Name}; Position: {Position}";
        }
    }
}
