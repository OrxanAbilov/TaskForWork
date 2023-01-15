﻿using System.Data.Common;
using TaskForWork.Data;
using TaskForWork.Models;
using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories.Implements
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext dbCtx;
        public ItemRepository(AppDbContext _dbCtx)
        {
            dbCtx = _dbCtx;
        }
        
        public int AddItem(Item itemAddDto)
        {
            Item item = new Item();
            item.ItemName = itemAddDto.ItemName;
            item.ItemPrice = itemAddDto.ItemPrice;
            item.ItemStock  = itemAddDto.ItemStock;

            dbCtx.Items.Add(item);
            var result = dbCtx.SaveChanges();
            return result;
        }

        public int DeleteItems(int[] ids)
        {
            foreach (var id in ids)
            {
                Item item = dbCtx.Items.Where(n => n.ItemId == id).FirstOrDefault();
                dbCtx.Items.Remove(item);
            }
            int result = dbCtx.SaveChanges();
            return result;
        }

        public int EditItem(Item itemAddDto)
        {
            int result = 0;
            Item item = dbCtx.Items.Where(n => n.ItemId == itemAddDto.ItemId).FirstOrDefault();
            if (item!=null)
            {
                item.ItemPrice = itemAddDto.ItemPrice;
                item.ItemName = itemAddDto.ItemName;
                item.ItemStock = itemAddDto.ItemStock;
                dbCtx.Items.Update(item);
                result = dbCtx.SaveChanges();
                
            }
            return result;

        }

        public Item EditItem(int id)
        {
            Item item = dbCtx.Items.Where(n => n.ItemId == id).FirstOrDefault();
            return item;
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = dbCtx.Items.ToList();
            return items;
        }

        public Item GetItemById(int id)
        {
            Item item = dbCtx.Items.Where(n => n.ItemId == id).FirstOrDefault();

            return item;
        }
    }
}
