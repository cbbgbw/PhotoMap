using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using PhotoMap.Backend.Entities;
using PhotoMap.Backend.Helpers;
using PhotoMap.Backend.Models.Photos;
using PhotoMap.Backend.Services;

namespace PhotoMap.Backend.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public PhotoController(
            IPhotoService photoService,
            IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var photos = _photoService.GetAll();
            var model = _mapper.Map<IList<PhotoResponseModel>>(photos);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var photo = _photoService.GetById(id);
            var model = _mapper.Map<PhotoResponseModel>(photo);
            return Ok(model);
        }

        [HttpGet("user/{userid}")]
        public IActionResult GetByUserId(Guid userid)
        {
            var photos = _photoService.GetByUserId(userid);
            var model = _mapper.Map<IList<PhotoResponseModel>>(photos);
            return Ok(model);
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody]PhotoInsertModel model)
        {
            if (model.PhotoRowguid == Guid.Empty){
                model.PhotoRowguid = Guid.NewGuid();
            }

            if(model.Latitude.Contains(','))
            {
                model.Latitude.Replace(',', '.');
            }
            if (model.Longitude.Contains(','))
            {
                model.Longitude.Replace(',', '.');
            }

            var photo = _mapper.Map<Photo>(model);
            
            try
            {
                //Insert photo
                _photoService.Insert(photo);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{photoid}")]
        public IActionResult Update(Guid photoid, [FromBody]PhotoUpdateModel model)
        {
            var photo = _mapper.Map<Photo>(model);
            photo.PhotoRowguid = photoid;

            try
            {
                _photoService.Update(photo);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{photoid}")]
        public IActionResult Delete(Guid photoid)
        {
            _photoService.Delete(photoid);
            return Ok();
        }

        [HttpGet("count")]
        public IActionResult Count()
        {
            var counter = _photoService.Count();
            return Ok(counter);
        }
    }
}