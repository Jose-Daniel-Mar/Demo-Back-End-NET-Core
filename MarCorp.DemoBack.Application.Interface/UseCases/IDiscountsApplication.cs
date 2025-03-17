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
        Task<Response<bool>> Create(DiscountDTO discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(DiscountDTO discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default);
        Task<Response<List<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default);
    }
}