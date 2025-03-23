using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Support.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCorp.DemoBack.Application.Interface.UseCases
{
    public interface IDiscountsApplication
    {
        Task<Response<bool>> CreateAsync(DiscountDTO discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(DiscountDTO discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDTO>> GetAsync(int id, CancellationToken cancellationToken = default);
        Task<Response<List<DiscountDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}