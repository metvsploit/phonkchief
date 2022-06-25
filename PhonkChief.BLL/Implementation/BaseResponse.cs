using PhonkChief.Domain.Entities;
using PhonkChief.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhonkChief.BLL.Implementation
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
