using AutoMapper;
using FakeXieCheng.API.Dtos;
using FakeXieCheng.API.Models;
using FakeXieCheng.API.ResourceParameters;
using FakeXieCheng.API.Services;
using Microsoft.AspNetCore.Http;
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
            return CreatedAtRoute("GetTouristRouteById", new { touristRouteById = touristRouteToReturn.Id }, touristRouteToReturn);
        }

    }
}
