using AutoMapper;
using PaymentEngine.Api.Dtos;
using PaymentEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Mapper
{
    public class ProcessPaymentMapper : Profile
    {
        public ProcessPaymentMapper()
        {
            CreateMap<ProcessPaymentDtos, Payments>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
