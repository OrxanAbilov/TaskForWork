using TaskForWork.Models.DTOs;

namespace TaskForWork.Repositories
{
    public interface IReportRepository
    {
        List<SaleReportDto> GetSaleReport();
    }
}
