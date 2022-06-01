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
            var cities = await uow.cityRepository.GetCitiesAsync();

            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);



            // var citiesDto = from c in cities

            // select new CityDto()
            // {
            //    Id = c.Id, 
            //    Name = c.Name 
            // };
            return Ok(citiesDto);
        }


        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        { 


            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;


            // var city = new City {
            //     Name = cityDto.Name,
            //     LastUpdatedBy = 1,
            //     LastUpdatedOn = DateTime.Now
            // };

            uow.cityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
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