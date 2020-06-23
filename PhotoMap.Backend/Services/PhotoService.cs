using PhotoMap.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMap.Backend.Services
{
    public interface IPhotoService
    {
        IEnumerable<Photo> GetAll();
        Photo GetById(Guid id);
        IEnumerable<Photo> GetByUserId(Guid userId);
        //Photo Insert(Photo photo);
        //void Update(Photo photo);
        //void Delete(Guid id);
    }

    public class PhotoService : IPhotoService
    {
        private PhotoMapDbContext _context;

        public PhotoService(PhotoMapDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Photo> GetAll()
        {
            return _context.Photos;
        }

        public Photo GetById(Guid id)
        {
            return _context.Photos.Find(id);
        }

        public IEnumerable<Photo> GetByUserId(Guid userId)
        {
            var photos = _context.Photos
                .Where(x => x.UserRowguid == userId);

            return photos;
        }
    }
}
