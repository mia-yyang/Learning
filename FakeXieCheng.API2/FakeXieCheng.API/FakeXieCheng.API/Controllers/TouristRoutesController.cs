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
using Microsoft.AspNetCore.Authorization;

namespace FakeXieCheng.API.Controllers
{
    [Route("api/[controller]")] // api/touristRoutes/
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private readonly ITouristRouteRepostitory _touristRouteRepostitory;
        private readonly IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepostitory touristRouteRepostitory,
            IMapper mapper)
        {
            _touristRouteRepostitory = touristRouteRepostitory;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetTouristRoutes(
            [FromQuery] TouristRouteResourceParameters parameters
        )
        {
            var touristRoutesFromRepo = await _touristRouteRepostitory
                .GetTouristRoutesAsync(
                    parameters.Keyword,
                    parameters.RatingOperator,
                    parameters.RatingValue,
                    0, 0,
                    parameters.OrderBy);
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);
            return Ok(touristRoutesDto.ShapeData(parameters.fields));
        }

        //[HttpGet("{touristRouteId:Guid}")]
        [HttpGet("{touristRouteId}", Name = "GetTouristRouteById")]
        public async Task<IActionResult> GetTouristRouteById(
            Guid touristRouteId,
            string fields
        )
        {
            var touristRouteFromRepo = await _touristRouteRepostitory.GetTouristRouteAsync(touristRouteId);
            if (touristRouteFromRepo == null)
            {
                return NotFound("旅游路线{touristRouteId}找不到");
            }
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);

            return Ok(touristRouteDto.ShapeData(fields));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
        {
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
            _touristRouteRepostitory.AddTouristRoute(touristRouteModel);
            await _touristRouteRepostitory.SaveAsync();
            var touristRouteToReturn = _mapper.Map<TouristRouteDto>(touristRouteModel);
            // 响应的Headers里Location
            return CreatedAtRoute(
                "GetTouristRouteById",
                new { touristRouteById = touristRouteToReturn.Id },
                touristRouteToReturn);
        }

        [HttpPut("{touristRouteId}")]
        public async Task<IActionResult> UpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!(await _touristRouteRepostitory.TouristRouteExistsAsync(touristRouteId)))
            {
                return NotFound("旅游路线不存在");
            }
            var touristRouteFromRepo = await _touristRouteRepostitory.GetTouristRouteAsync(touristRouteId);

            // 1.映射Dto 2.更新Dto 3.映射model
            _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);

            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> patchDocument
            )
        {
            if (!(await _touristRouteRepostitory.TouristRouteExistsAsync(touristRouteId)))
            {
                return NotFound("旅游路线不存在");
            }

            var touristRouteFromRepo = await _touristRouteRepostitory.GetTouristRouteAsync(touristRouteId);

            var touristRouteToPatch = _mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
            patchDocument.ApplyTo(touristRouteToPatch, ModelState);

            if (!TryValidateModel(touristRouteToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(touristRouteToPatch, touristRouteFromRepo);
            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{touristRouteId}")]
        public async Task<IActionResult> DeleteTouristRoute([FromRoute] Guid touristRouteId)
        {
            if (!(await _touristRouteRepostitory.TouristRouteExistsAsync(touristRouteId)))
            {
                return NotFound("旅游路线不存在");
            }

            var touristRouteFromRepo = await _touristRouteRepostitory.GetTouristRouteAsync(touristRouteId);
            _touristRouteRepostitory.DeleteTouristRoute(touristRouteFromRepo);
            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }

        [HttpDelete("({touristIDs})")]
        public async Task<IActionResult> DeleteByIDs(
            [ModelBinder(BinderType = typeof(CustomArrayModeBinder))][FromRoute] IEnumerable<Guid> touristIDs
        )
        {
            if (touristIDs == null)
            {
                return BadRequest();
            }
            var touristRoutesFromRepo = await _touristRouteRepostitory.GetTouristRoutesByIDListAsync(touristIDs);
            _touristRouteRepostitory.DeleteTouristRoutes(touristRoutesFromRepo);
            await _touristRouteRepostitory.SaveAsync();

            return NoContent();
        }
    }
}
