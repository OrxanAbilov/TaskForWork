using NuGet.Packaging.Signing;
using TaskForWork.Controllers;
using TaskForWork.Data;
using TaskForWork.Models;
using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories.Implements
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext dbCtx;
        public InvoiceRepository(AppDbContext _dbCtx)
        {
            dbCtx = _dbCtx;
        }

        public int AddInvoice(Invoice invoiceAdd)
        {
            dbCtx.Invoices.Add(invoiceAdd);
            int result = dbCtx.SaveChanges();
            return result;
        }

        public int DeleteInvoices(int[] data)
        {

            foreach (var id in data)
            {
                Invoice invoice = dbCtx.Invoices.Where(n => n.InvId == id).FirstOrDefault();
                dbCtx.Invoices.Remove(invoice);
            }
            int result = dbCtx.SaveChanges();
            return result;

        }

        public int EditInvoice(InvoiceDto invoiceEdit)
        {
            int result = 0;
            Invoice invoice = dbCtx.Invoices.Where(n=> n.InvId==invoiceEdit.InvId).FirstOrDefault();
            if (invoice != null)
            {
                invoice.Total = invoiceEdit.Total;
                invoice.Price = invoiceEdit.Price;
                invoice.Amount = invoiceEdit.Amount;
                invoice.SaleDate = Convert.ToDateTime(invoiceEdit.SaleDate);

                dbCtx.Invoices.Update(invoice);
                result = dbCtx.SaveChanges();
            }
                
            return result;


        }

        public InvoiceDto EditInvoice(int id)
        {
            InvoiceDto invoice = dbCtx.Invoices.Where(n => n.InvId == id).Select(n => new InvoiceDto()
            {
                InvId = n.InvId,
                ItemName = dbCtx.Items.Where(i => i.ItemId == n.ItemId).Select(i => i.ItemName).FirstOrDefault(),
                Price = n.Price,
                Amount = n.Amount,
                Total = n.Total,
                SaleDate = n.SaleDate.ToString("yyyy-MM-dd")
            }).FirstOrDefault();

            return invoice;
        }

        public List<InvoiceListDto> GetAllInvoices()
        {
            List<InvoiceListDto> invoices = dbCtx.Invoices.Select(n => new InvoiceListDto()
            {
                InvId = n.InvId,
                ItemName = dbCtx.Items.Where(i => i.ItemId == n.ItemId).Select(i => i.ItemName).FirstOrDefault(),
                Price=n.Price,
                Amount=n.Amount,
                Total =n.Total,
                SaleDate=n.SaleDate.ToShortDateString(),
            }).ToList();
            return invoices;

        }

    }
}
