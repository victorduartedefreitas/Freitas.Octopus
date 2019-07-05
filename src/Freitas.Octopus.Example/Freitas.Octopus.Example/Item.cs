namespace Freitas.Octopus.Example
{
    public class Item : IOctopusItem
    {
        public Item(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Position { get; set; }
    }
}
