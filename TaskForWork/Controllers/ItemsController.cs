using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System.Security.Principal;
using TaskForWork.Models;
using TaskForWork.Models.DTOs;
using TaskForWork.Repositories;

namespace TaskForWork.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemRepository itemRepository;
        public ItemsController(IItemRepository _itemRepository)
        {
            itemRepository = _itemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetAllItems()
        {
            List<ItemList> items = itemRepository.GetAllItems();
            return Json(items);
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            int result = itemRepository.AddItem(item);
            if (result > 0)
            {
                return Json('1');
            }
            else return Json('0');
            
        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            Item item = itemRepository.EditItem(id);
            if (item !=null)
            {
                return Json(item);
            }
            else return Json('0');

        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            int result = itemRepository.EditItem(item);
            if (result > 0)
            {
                return Json('1');
            }
            else return Json('0');

        }


        [HttpPost]
        public IActionResult DeleteItems(int[] data)
        {
            int result = itemRepository.DeleteItems(data);
            if (result > 0)
            {
                return Json('1');
            }
            else return Json('0');
        }


        public IActionResult GetAllItemsForInvoice()
        {
            List<ItemComboDto> items = itemRepository.GetAllItemsForInvoice();
            //string json = JsonConvert.SerializeObject(items);
            return Json(items);
        }



       
    }
}
