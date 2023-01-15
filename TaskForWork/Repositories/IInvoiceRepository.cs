using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories
{
    public interface IInvoiceRepository
    {
        int AddInvoice(InvoiceAddDto invoiceAddDto);
        int EditInvoice(InvoiceAddDto invoiceAddDto);
        int EditInvoice(int id);
        int DeleteInvoice(int[] data);
    }
}
