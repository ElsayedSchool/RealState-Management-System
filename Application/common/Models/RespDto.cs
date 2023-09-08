using Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class RespDto<T>
    {
        public string Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public bool Error { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(RespDto<>), typeof(RespDto<>));
        }

        public RespDto<T> Get400Error(string ErrorMessage)
        {
            return new RespDto<T> { Message = ErrorMessage, Error = true, StatusCode = 400 };
        }

        public RespDto<T> Get500Error(string ErrorMessage)
        {
            return new RespDto<T> { Message = ErrorMessage, Error = true, StatusCode = 500 };
        }

        public bool HasError()
        {
            return Error;
        }

        public RespDto<bool> GetNotFoundError(string message)
        {
            return new RespDto<bool>() { Error = true, StatusCode = 400, Data = false, Message = $"Entity {message} Was not Found" };
        }

    }
}