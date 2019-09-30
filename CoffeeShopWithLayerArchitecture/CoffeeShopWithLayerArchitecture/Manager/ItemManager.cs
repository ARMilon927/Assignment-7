using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShopWithLayerArchitecture.Repository;

namespace CoffeeShopWithLayerArchitecture.Manager
{
    public class ItemManager
    {
        ItemRepository _itemRepository = new ItemRepository();

        public bool ExistItem(string name, int id)
        {
            return _itemRepository.ExistItem(name, id);
        }

        public string InsertItem(string name, int price)
        {
            string message;
            message = _itemRepository.InsertItem(name, price) > 0 ? "Item is Saved" : "Item is not saved";
            return message;
        }

        public DataTable ShowItem()
        {
            return _itemRepository.ShowItem();
        }

        public int DeleteItem(int id)
        {
            return _itemRepository.DeleteItem(id);
        }

        public int UpdateItem(string name, int price, int id)
        {
            return _itemRepository.UpdateItem(name, price, id);

        }
        public DataTable SearchItem(string name, int price)
        {
            return _itemRepository.SearchItem(name, price);
        }
    }
}
