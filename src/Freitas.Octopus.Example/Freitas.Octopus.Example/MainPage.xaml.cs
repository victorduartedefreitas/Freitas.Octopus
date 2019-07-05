using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Freitas.Octopus.Example
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private ICommand _moveUpCommand;
        public ICommand MoveUpCommand
        {
            get
            {
                if (_moveUpCommand == null)
                {
                    _moveUpCommand = new Command(() => MoveUp());
                }
                return _moveUpCommand;
            }
        }

        private ICommand _moveDownCommand;
        public ICommand MoveDownCommand
        {
            get
            {
                if (_moveDownCommand == null)
                {
                    _moveDownCommand = new Command(() => MoveDown());
                }
                return _moveDownCommand;
            }
        }

        private IList<Item> items;
        public IList<Item> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<Item>
                    {
                        new Item("Item 001"),
                        new Item("New item"),
                        new Item("Cellphone"),
                        new Item("Notebook"),
                        new Item("Mouse"),
                        new Item("Tablet"),
                        new Item("Photo"),
                        new Item("Water bottle"),
                        new Item("Bible"),
                        new Item("Book"),
                        new Item("Macbook"),
                        new Item("iMac"),
                        new Item("Trash"),
                        new Item("Paper"),
                        new Item("Chair"),
                        new Item("Table"),
                        new Item("TV"),
                        new Item("Cup"),
                        new Item("Trash"),
                        new Item("F1"),
                        new Item("F2"),
                        new Item("F3"),
                        new Item("F4"),
                        new Item("F5"),
                        new Item("F6"),
                        new Item("F7"),
                        new Item("F8"),
                        new Item("F9"),
                        new Item("F10"),
                        new Item("F11"),
                        new Item("F12"),
                        new Item("LetterA"),
                        new Item("LetterB"),
                        new Item("LetterC"),
                        new Item("LetterD"),
                        new Item("LetterE"),
                        new Item("LetterF"),
                        new Item("LetterG"),
                        new Item("LetterH"),
                        new Item("LetterI"),
                        new Item("LetterJ"),
                        new Item("LetterK"),
                        new Item("LetterL"),
                        new Item("LetterM"),
                        new Item("LetterN"),
                        new Item("LetterO"),
                        new Item("LetterP"),
                        new Item("LetterQ"),
                        new Item("LetterR"),
                        new Item("LetterS"),
                        new Item("LetterT"),
                        new Item("LetterU"),
                        new Item("LetterV"),
                        new Item("LetterW"),
                        new Item("LetterX"),
                        new Item("LetterY"),
                        new Item("LetterZ")
                    };

                    items.InitializeOctopusOrdination();
                    items = items.OctopusOrderBy();
                }

                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public Item SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private Item selectedItem;

        public void MoveUp()
        {
            Items = Items.MoveUp(SelectedItem);
            SelectedItem = selectedItem;
        }

        public void MoveDown()
        {
            Items = Items.MoveDown(SelectedItem);
            SelectedItem = selectedItem;
        }
    }
}
