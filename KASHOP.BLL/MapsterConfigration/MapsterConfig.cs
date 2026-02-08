using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL.MapsterConfigration
{
    public class MapsterConfig
    {
        public static void MapsterConfRegister()
        {
            // Add your Mapster configurations here
            //TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
            //    .Map(dest => dest., source => source.Id).TwoWays();

            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.CreatedBy, source => source.User.UserName);

            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig()
                .Map(dest => dest.Name, source => source.Translations
                    .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                    .Select(t => t.Name)
                    .FirstOrDefault());

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(dest => dest.MainImage, source => $"http://localhost:5162/images/{source.MainImage}");

            TypeAdapterConfig<Product, ProductUserResponse>.NewConfig()
     .Map(dest => dest.MainImage, source => $"http://localhost:5162/images/{source.MainImage}")
     .Map(dest => dest.Name, source => source.Translations
         .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
         .Select(t => t.Name)
         .FirstOrDefault());

            TypeAdapterConfig<Product, ProductUserDetails>.NewConfig()
    .Map(dest => dest.Name,
        source => source.Translations
            .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
            .Select(t => t.Name)
            .FirstOrDefault())
    .Map(dest => dest.Description,
        source => source.Translations
            .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
            .Select(t => t.Description)
            .FirstOrDefault());

            TypeAdapterConfig<Order, OrderResponse>.NewConfig()
                .Map(dest => dest.userName, source => source.User.UserName);

            TypeAdapterConfig<Review, ReviewResponse>.NewConfig()
    .Map(dest => dest.UserName, source => source.User.UserName);


        }
    }
}
