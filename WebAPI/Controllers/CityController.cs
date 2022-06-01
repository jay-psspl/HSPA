using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            throw new UnauthorizedAccessException();
            var cities = await uow.cityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }


        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            uow.cityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {

            try {
                if(id != cityDto.Id)
                    return BadRequest("Update not allowed");
                var cityFromDb = await uow.cityRepository.FindCity(id);

                if (cityFromDb == null)
                    return BadRequest("Update not allowed");               
                cityFromDb.LastUpdatedBy = 1;
                cityFromDb.LastUpdatedOn = DateTime.Now;
                mapper.Map(cityDto, cityFromDb);
                throw new Exception("Some unknown error occured");
                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch 
            {
                return StatusCode(500, "Some unknown error occured");
            }  
        }  

        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        {
            var cityFromDb = await uow.cityRepository.FindCity(id);
             
            cityFromDb.LastUpdatedBy = 1;
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.cityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }

    }   
}