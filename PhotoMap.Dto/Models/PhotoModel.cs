using System;

namespace PhotoMap.Dto.Models
{
    public class PhotoModel
    {
        public Guid PhotoRowguid { get; set; }
        public Guid? UserRowguid { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string PhotoPath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
