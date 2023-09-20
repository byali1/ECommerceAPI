﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ProductImageFileWriteRepository:WriteRepository<ProductImageFile>,IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
