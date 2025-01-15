using ApiControleFinanceiro.DTOs;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories;
using ApiControleFinanceiro.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiControleFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TipoDeContaController : ControllerBase
    {
        private readonly ITipoDeContaRepository _tipoDeContaRepository;
        private readonly IMapper _mapper;
        public TipoDeContaController(ITipoDeContaRepository tipoDeContaRepository, IMapper mapper)
        {
            _tipoDeContaRepository = tipoDeContaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeContaDTO>>> Get()
        {
            var tiposDeConta = await _tipoDeContaRepository.GetAllAsync();
            if (tiposDeConta is null)
                return NotFound("Não existem Tipos de Conta cadastradas!");

            var tiposDeContasDTO = _mapper.Map<IEnumerable<TipoDeContaDTO>>(tiposDeConta);
            return Ok(tiposDeContasDTO);
        }

        [HttpGet("{id:int}", Name = "GetTiposDeConta")]
        public async Task<ActionResult<TipoDeContaDTO>> Get(int id)
        {
            var tipoDeConta = await _tipoDeContaRepository.GetByIdAsync(id);
            if (tipoDeConta is null)
                return NotFound("O Tipo de Conta informado não existe!");

            var tipoDeContaDTO = _mapper.Map<TipoDeContaDTO>(tipoDeConta);
            return Ok(tipoDeContaDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoDeContaDTO tipoDeContaDTO)
        {
            if (tipoDeContaDTO is null)
                return BadRequest("Dados inválidos");

            var tipoDeConta = _mapper.Map<TipoDeConta>(tipoDeContaDTO);
            await _tipoDeContaRepository.CreateAsync(tipoDeConta);

            return new CreatedAtRouteResult("GetTipoDeConta", new { id = tipoDeContaDTO.Id }, tipoDeContaDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] TipoDeContaDTO tipoDeContaDTO)
        {
            if (id != tipoDeContaDTO.Id)
                return BadRequest();

            if (tipoDeContaDTO is null)
                return BadRequest();

            var tipoDeConta = _mapper.Map<TipoDeConta>(tipoDeContaDTO);

            await _tipoDeContaRepository.UpdateAsync(tipoDeConta);

            return Ok(tipoDeContaDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoDeConta = await _tipoDeContaRepository.GetByIdAsync(id);
            if (tipoDeConta is null)
                return NotFound("O Tipo de Conta informado não existe!");

            await _tipoDeContaRepository.DeleteAsync(id);
            return Ok(tipoDeConta);
        }
    }
}
