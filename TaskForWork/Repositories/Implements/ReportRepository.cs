using TaskForWork.Data;
using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories.Implements
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext dbCtx;
        public ReportRepository(AppDbContext _dbCtx)
        {
            dbCtx = _dbCtx;
        }

        public List<SaleReportDto> GetSaleReport()
        {
            List<SaleReportDto> report = (from inv in dbCtx.Invoices
                                          join itm in dbCtx.Items on inv.ItemId equals itm.ItemId
                                          group inv by new { itm.ItemName } into g
                                          select new SaleReportDto()
                                          {
                                              Name = g.Key.ItemName,
                                              SumAmount = g.Sum(inv => inv.Amount),
                                              SumTotal = g.Sum(inv => inv.Total)
                                          }).ToList();
            return report;
        }
    }
}
