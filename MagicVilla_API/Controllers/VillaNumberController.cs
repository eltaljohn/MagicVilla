using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class VillaNumberController : ControllerBase
	{

		private readonly ILogger<VillaNumberController> _logger;
		private readonly IVillaRepository _villaRepo;
		private readonly IVillaNumberRepository _villaNumberRepo;
        private readonly IMapper _mapper;
		protected APIResponse _response;

		public VillaNumberController(ILogger<VillaNumberController> logger, IVillaRepository villaRepo, IVillaNumberRepository villaNumberRepo, IMapper mapper)
		{
			_logger = logger;
			_villaRepo = villaRepo;
            _villaNumberRepo = villaNumberRepo;
			_mapper = mapper;
			_response = new() { IsSuccessful = true };
		}

		[HttpGet]
		public async Task<ActionResult<APIResponse>> GetVillasNumber()
		{
			try
			{
                _logger.LogInformation("Get villas");
                IEnumerable<VillaNumber> villaList = await _villaNumberRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaNumberDto>>(villaList);

                return Ok(_response);
            }
			catch (Exception ex)
			{
				_response.IsSuccessful = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
			try
			{
                if (id == 0)
                {
                    _logger.LogError("Error getting villas for id:" + id);
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }

                var villaNumber = await _villaNumberRepo.Get(v => v.VillaNo == id);

                if (villaNumber == null)
                {
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }

				_response.Result = _mapper.Map<VillaNumberDto>(villaNumber);

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
		public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
		{
			try
			{
                if (!ModelState.IsValid)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                if (await _villaNumberRepo.Get(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NameExist", "Name already exist");
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("ForeignKey", "The villa id doesn't exist");
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(createDto);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(createDto);

                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                await _villaNumberRepo.Create(model);
                _response.Result = model;

                return CreatedAtRoute("GetVillaNumber", new { id = model.VillaNo }, _response);
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
        public async Task<IActionResult> DeleteVillaNumber(int id)
		{
            try
            {
                if (id == 0)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }
                var villaNumber = await _villaNumberRepo.Get(v => v.VillaNo == id);

                if (villaNumber == null)
                {
                    _response.IsSuccessful = false;
                    return NotFound(_response);
                }
                await _villaNumberRepo.Remove(villaNumber);

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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaNumberUpdateDto updateDto)
		{
            try
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    _response.IsSuccessful = false;
                    return BadRequest(_response);
                }

                if (await _villaRepo.Get(v => v.Id == updateDto.VillaId) == null)
                {
                    ModelState.AddModelError("ForeignKey", "The villa id doesn't exist");
                    _response.IsSuccessful = false;
                    return BadRequest(ModelState);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

                await _villaNumberRepo.Update(model);
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
        public async Task<IActionResult> PatchVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDto> patchDto)
        {
            try
            {
                if (patchDto == null || id == 0)
                {
                    _response.IsSuccessful = false;
                    return BadRequest();
                }
                var villa = await _villaNumberRepo.Get(v => v.VillaNo == id, tracked: false);

                VillaNumberUpdateDto villaDto = _mapper.Map<VillaNumberUpdateDto>(villa);

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

                VillaNumber model = _mapper.Map<VillaNumber>(villaDto);

                await _villaNumberRepo.Update(model);

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

