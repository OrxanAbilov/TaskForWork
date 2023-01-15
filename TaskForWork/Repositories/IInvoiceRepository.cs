using TaskForWork.Models;
using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories
{
    public interface IInvoiceRepository
    {
        List<InvoiceListDto> GetAllInvoices();
        int AddInvoice(Invoice invoiceAdd);
        int EditInvoice(InvoiceDto invoiceEdit);
        InvoiceDto EditInvoice(int id);
        int DeleteInvoices(int[] data);
    }
}
