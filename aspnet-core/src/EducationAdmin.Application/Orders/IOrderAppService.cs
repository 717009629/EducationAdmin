using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EducationAdmin.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationAdmin.Orders
{
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, long, PagedOrderResultRequestDto, CreateOrderDto, EditOrderDto>
    {
        Task<OrderDto> Audite(AuditeOrderDto input);
    }
}
