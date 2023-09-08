using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mediatr
{
    public interface IRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, RespDto<TResponse>> where TRequest : IRequestWrapper<TResponse>
    {

    }
}
