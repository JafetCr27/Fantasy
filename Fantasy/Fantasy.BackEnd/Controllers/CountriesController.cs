﻿namespace Fantasy.BackEnd.Controllers
{
    using Fantasy.BackEnd.UnitOfWork.Interfaces;
    using Fantasy.Shared.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unit, ICountriesUnitOfWork countriesUnitOfWork) : base(unit)
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _countriesUnitOfWork.GetComboAsync());
        }
        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _countriesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

    }
}
