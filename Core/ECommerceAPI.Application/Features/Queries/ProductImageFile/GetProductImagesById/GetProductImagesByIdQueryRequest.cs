﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImagesById
{
    public class GetProductImagesByIdQueryRequest : IRequest<List<GetProductImagesByIdQueryResponse>>
    {
        public string Id { get; set; }
    }
}
