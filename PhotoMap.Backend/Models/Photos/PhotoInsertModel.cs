using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMap.Backend.Models.Photos
{
    public class PhotoInsertModel
    {
        public Guid PhotoRowguid => Guid.NewGuid();
        public Guid? UserRowguid { get; set; }

        [StringLength(20, ErrorMessage = "Latitude can not be more than 20")]
        public string Latitude { get; set; }

        [StringLength(20, ErrorMessage = "Longitude can not be more than 20")]
        public string Longitude { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "PhotoPath can not be more than 200")]
        public string PhotoPath { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title can not be more than 100")]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt => DateTime.Now;
    }
}
