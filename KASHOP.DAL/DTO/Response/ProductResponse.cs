using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KASHOP.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        public string CreatedBy { get; set; }

        public string MainImage { get; set; }

        public List<CategoryTranslationResponse> Translations { get; set; }

    }
}
