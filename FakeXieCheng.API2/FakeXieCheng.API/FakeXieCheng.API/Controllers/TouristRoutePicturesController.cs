using AutoMapper;
using FakeXieCheng.API.Dtos;
using FakeXieCheng.API.Models;
using FakeXieCheng.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Controllers
{
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepostitory _touristRouteRepostitory;
        private IMapper _mapper;
        public TouristRoutePicturesController(ITouristRouteRepostitory touristRouteRepostitory,
            IMapper mapper)
        {
            _touristRouteRepostitory = touristRouteRepostitory ??
                throw new ArgumentNullException(nameof(touristRouteRepostitory));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var picturesFromRepo = _touristRouteRepostitory.GetPicturesByTouristRouteId(touristRouteId);
            if (picturesFromRepo == null || picturesFromRepo.Count() <= 0)
            {
                return NotFound("照片不存在");
            }

            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
        }

        [HttpGet("{pictureId}", Name = "GetPicture")]
        public IActionResult GetPicture(Guid touristRouteId, int pictureId)
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var pictureFromRepo = _touristRouteRepostitory.GetPicture(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("照片不存在");
            }

            return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
        }

        [HttpPost]
        public IActionResult CreateTouristRoutePicture(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRoutePictureForCreationDto touristRoutePictureForCreationDto
        )
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var pictureModel = _mapper.Map<TouristRoutePicture>(touristRoutePictureForCreationDto);
            _touristRouteRepostitory.AddTouristRoutePicture(touristRouteId, pictureModel);
            _touristRouteRepostitory.Save();
            var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureModel);
            return CreatedAtRoute(
                "GetPicture",
                new
                {
                    touristRouteId = pictureModel.TouristRouteId,
                    pictureId = pictureModel.Id
                },
                pictureToReturn
                );
        }

        [HttpDelete("{pictureId}")]
        public IActionResult DeletePicture(
            [FromRoute] Guid touristRouteId,
            [FromRoute] int pictureId
        )
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var picture = _touristRouteRepostitory.GetPicture(pictureId);
            _touristRouteRepostitory.DeleteTouristRoutePicture(picture);
            _touristRouteRepostitory.Save();

            return NoContent();
        }
    }
}
