using AutoMapper;
using FakeXieCheng.API.Dtos;
using FakeXieCheng.API.Models;
using FakeXieCheng.API.ResourceParameters;
using FakeXieCheng.API.Services;
using FakeXieCheng.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FakeXieCheng.API.Controllers
{
    [Route("api/[controller]")] // api/touristRoutes/
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepostitory _touristRouteRepostitory;
        private IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepostitory touristRouteRepostitory,
            IMapper mapper)
        {
            _touristRouteRepostitory = touristRouteRepostitory;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public IActionResult GetTouristRoutes(
            [FromForm] TouristRouteResourceParameters parameters
        )
        {
            var touristRoutesFromRepo = _touristRouteRepostitory.GetTouristRoutes(parameters.Keyword, parameters.RatingOperator, parameters.RatingValue);
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);
            return Ok(touristRoutesDto);
        }

        //[HttpGet("{touristRouteId:Guid}")]
        [HttpGet("{touristRouteId}", Name = "GetTouristRouteById")]
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepostitory.GetTouristRoute(touristRouteId);
            if (touristRouteFromRepo == null)
            {
                return NotFound("旅游路线{touristRouteId}找不到");
            }
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);

            return Ok(touristRouteDto);
        }

        [HttpPost]
        public IActionResult CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
        {
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
            _touristRouteRepostitory.AddTouristRoute(touristRouteModel);
            _touristRouteRepostitory.Save();
            var touristRouteToReturn = _mapper.Map<TouristRouteDto>(touristRouteModel);
            // 响应的Headers里Location
            return CreatedAtRoute(
                "GetTouristRouteById",
                new { touristRouteById = touristRouteToReturn.Id },
                touristRouteToReturn);
        }

        [HttpPut("{touristRouteId}")]
        public IActionResult UpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }
            var touristRouteFromRepo = _touristRouteRepostitory.GetTouristRoute(touristRouteId);

            // 1.映射Dto 2.更新Dto 3.映射model
            _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);

            _touristRouteRepostitory.Save();

            return NoContent();
        }

        [HttpPatch]
        public IActionResult PartiallyUpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> patchDocument
            )
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var touristRouteFromRepo = _touristRouteRepostitory.GetTouristRoute(touristRouteId);

            var touristRouteToPatch = _mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
            patchDocument.ApplyTo(touristRouteToPatch, ModelState);

            if (!TryValidateModel(touristRouteToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(touristRouteToPatch, touristRouteFromRepo);
            _touristRouteRepostitory.Save();

            return NoContent();
        }

        [HttpDelete("{touristRouteId}")]
        public IActionResult DeleteTouristRoute([FromRoute] Guid touristRouteId)
        {
            if (!_touristRouteRepostitory.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }

            var touristRouteFromRepo = _touristRouteRepostitory.GetTouristRoute(touristRouteId);
            _touristRouteRepostitory.DeleteTouristRoute(touristRouteFromRepo);
            _touristRouteRepostitory.Save();

            return NoContent();
        }

        [HttpDelete("({touristIDs})")]
        public IActionResult DeleteByIDs(
            [ModelBinder(BinderType = typeof(CustomArrayModeBinder))][FromRoute] IEnumerable<Guid> touristIDs
        )
        {
            if (touristIDs == null)
            {
                return BadRequest();
            }
            var touristRoutesFromRepo = _touristRouteRepostitory.GetTouristRoutesByIDList(touristIDs);
            _touristRouteRepostitory.DeleteTouristRoutes(touristRoutesFromRepo);
            _touristRouteRepostitory.Save();

            return NoContent();
        }
    }
}
