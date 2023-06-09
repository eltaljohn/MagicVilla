﻿using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class VillaController : ControllerBase
	{

		private readonly ILogger<VillaController> _logger;
		private readonly IVillaRepository _villaRepo;
		private readonly IMapper _mapper;
		protected APIResponse _response;

		public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
		{
			_logger = logger;
			_villaRepo = villaRepo;
			_mapper = mapper;
			_response = new() { IsSuccessful = true };
		}

		[HttpGet]
		public async Task<ActionResult<APIResponse>> GetVillas()
		{
			try
			{
                _logger.LogInformation("Get villas");
                IEnumerable<Villa> villaList = await _villaRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaDto>>(villaList);

                return Ok(_response);
            }
			catch (Exception ex)
			{
				_response.IsSuccessful = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

        [HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
			try
			{
                if (id == 0)
                {
                    _logger.LogError("Error getting villas for id:" + id);
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }

                var villa = await _villaRepo.Get(v => v.Id == id);

                if (villa == null)
                {
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }

				_response.Result = _mapper.Map<VillaDto>(villa);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto createDto)
		{
			try
			{
                if (!ModelState.IsValid)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("NameExist", "Name already exist");
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(createDto);
                }

                Villa model = _mapper.Map<Villa>(createDto);

                model.CreationDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                await _villaRepo.Create(model);
                _response.Result = model;

                return CreatedAtRoute("GetVilla", new { id = model.Id }, _response);
            }
			catch (Exception ex)
			{
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
		}

		[HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
		{
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var villa = await _villaRepo.Get(v => v.Id == id);

                if (villa == null)
                {
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }
                await _villaRepo.Remove(villa);

                _response.IsSuccessful = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

		[HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
		{
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }

                Villa model = _mapper.Map<Villa>(updateDto);

                await _villaRepo.Update(model);
                _response.Result = model;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            try
            {
                if (patchDto == null || id == 0)
                {
                    _response.IsSuccessful = false;
                    return BadRequest();
                }
                var villa = await _villaRepo.Get(v => v.Id == id, tracked: false);

                VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

                if (villa == null)
                {
                    _response.IsSuccessful = false;
                    return BadRequest();
                }

                patchDto.ApplyTo(villaDto, ModelState);

                if (!ModelState.IsValid)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                Villa model = _mapper.Map<Villa>(villaDto);

                await _villaRepo.Update(model);

                _response.Result = model;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccessful = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }
    }
}

