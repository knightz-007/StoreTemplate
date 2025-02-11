﻿using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(Product).IsAssignableFrom(type)
                || typeof(IEnumerable<Product>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Product>)
            {
                foreach (var product in (IEnumerable<Product>)context.Object)
                {
                    FormatCsv(buffer, product);
                }
            }
            else
            {
                FormatCsv(buffer, (Product)context.Object);
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, Product product)
        {
            buffer.AppendLine($"{product.ProductId},\"{product.ProductName},\"{product.UnitPrice},\"{product.UnitsInStock},\"{product.Category.CategoryName}\"");
        }
    }
}
