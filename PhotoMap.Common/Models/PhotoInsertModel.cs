using System;

namespace PhotoMap.DTO.Models
{
    public class PhotoInsertModel
    {
        public Guid PhotoRowguid => Guid.NewGuid();
        public Guid? UserRowguid { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string PhotoPath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
