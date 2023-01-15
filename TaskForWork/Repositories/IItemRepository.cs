using TaskForWork.Models;
using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories
{
    public interface IItemRepository
    {
        List<ItemList> GetAllItems();
        Item GetItemById(int id);
        int AddItem(Item item);
        int EditItem(Item item);
        Item EditItem(int id);
        int DeleteItems(int[] ids);
        List<ItemComboDto> GetAllItemsForInvoice();
    }
}
