using PhotoMap.Backend.Entities;
using PhotoMap.Backend.Helpers;
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
        Photo Insert(Photo photo);
        void Update(Photo photo);
        void Delete(Guid id);
        int Count();
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

        public Photo Insert(Photo photo)
        {
            if (_context.Photos.Any(x => x.PhotoPath == photo.PhotoPath))
                throw new AppException("Path\"" + photo.PhotoPath + "\" is not unique");

            _context.Photos.Add(photo);
            _context.SaveChanges();

            return photo;
        }

        public void Update(Photo photoParam)
        {
            var photo = _context.Photos.Find(photoParam.PhotoRowguid);

            if (photo == null)
                throw new AppException("Photo not found");

            //Update photo title
            if (!string.IsNullOrWhiteSpace(photoParam.Title))
            {
                photo.Title = photoParam.Title;
            }

            //Update photo description
            if (!string.IsNullOrWhiteSpace(photoParam.Description))
            {
                photo.Description = photoParam.Description;
            }

            _context.Photos.Update(photo);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var photo = _context.Photos.Find(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                _context.SaveChanges();
            }
        }

        public int Count()
        {
            var countPhotos = _context.Photos.Count<Photo>();
            return countPhotos;
        }
    }
}
