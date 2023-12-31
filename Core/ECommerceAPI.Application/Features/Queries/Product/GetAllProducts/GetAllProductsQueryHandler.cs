﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.RequestParameters;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }


        //Gelen Request'e karşılık Response dönecek.
        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return new GetAllProductsQueryResponse
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
