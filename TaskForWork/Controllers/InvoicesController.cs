using Microsoft.AspNetCore.Mvc;
using TaskForWork.Models;
using TaskForWork.Models.DTOs;
using TaskForWork.Repositories;
using TaskForWork.Repositories.Implements;

namespace TaskForWork.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;
        public InvoicesController(IInvoiceRepository _invoiceRepository)
        {
            invoiceRepository= _invoiceRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllInvoices()
        {
            List<InvoiceListDto> invoices = invoiceRepository.GetAllInvoices();
            return Json(invoices);
        }

        public IActionResult AddInvoice(Invoice invoice)
        {
            int result = invoiceRepository.AddInvoice(invoice);

            if (result>0)
            {
                return Json('1');
            }
            return Json('0');
        }


        [HttpPost]
        public IActionResult DeleteInvoices(int[] data)
        {
            int result = invoiceRepository.DeleteInvoices(data);
            if (result > 0)
            {
                return Json('1');
            }
            else return Json('0');
        }

        public IActionResult EditInvoice(int id)
        {
            InvoiceDto invoice =invoiceRepository.EditInvoice(id);
            return Json(invoice);
        }
        [HttpPost]
        public IActionResult EditInvoice(InvoiceDto invoice)
        {
            int result = invoiceRepository.EditInvoice(invoice);
            if (result>0)
            {
                return Json('1');
            }
            return Json('0');
        }


    }
}
