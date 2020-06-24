using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMap.Backend.Models.Photos
{
    public class PhotoResponseModel
    {
        public Guid PhotoROWGUID { get; set; }
        public Guid UserRowguid { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhotoPath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
