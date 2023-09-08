using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.ProductValidator
{
     public class CreateProductValidator: AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            //Name
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Ürün adı boş olamaz.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Ürün adı minimum 2 karakter olmalıdır.");
            RuleFor(x => x.Name).MaximumLength(250).WithMessage("Ürün adı maximum 250 karakter olabilir.");

            //Stock
            RuleFor(x => x.Stock).NotNull().WithMessage("Stok bilgisi boş olamaz.");
            RuleFor(x => x.Stock).Must(s => s >= 0).WithMessage("Stok bilgisi minimum 0 olabilir.");

            //Price
            RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("Fiyat bilgisi boş bırakılamaz.");
            RuleFor(x => x.Price).Must(s => s > 0).WithMessage("Fiyat bilgisi 0'dan büyük olmalıdır.");

        }
    }
}
