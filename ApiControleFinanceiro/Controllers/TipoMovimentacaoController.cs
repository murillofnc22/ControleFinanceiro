using ApiControleFinanceiro.DTOs;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiControleFinanceiro.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TipoMovimentacaoController : ControllerBase
    {
        private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
        private readonly IMapper _mapper;
        public TipoMovimentacaoController(ITipoMovimentacaoRepository tipoMovimentacaoRepository, IMapper mapper)
        {
            _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoMovimentacaoDTO>>> Get()
        {
            var tiposDeMovimentacao = await _tipoMovimentacaoRepository.GetAllAsync();
            if (tiposDeMovimentacao is null)
                return NotFound("Não existem Tipos de Movimentação cadastrados!");

            var tiposDeMovimentacaoDTO = _mapper.Map<IEnumerable<TipoMovimentacaoDTO>>(tiposDeMovimentacao);
            return Ok(tiposDeMovimentacaoDTO);
        }

        [HttpGet("{id:int}", Name = "GetTipoDeMovimentacao")]
        public async Task<ActionResult<TipoMovimentacaoDTO>> Get(int id)
        {
            var tipoDeMovimentacao = await _tipoMovimentacaoRepository.GetByIdAsync(id);
            if (tipoDeMovimentacao is null)
                return NotFound("O Tipo de Movimentação informado não existe!");

            var tipoDeMovimentacaoDTO = _mapper.Map<TipoMovimentacaoDTO>(tipoDeMovimentacao);
            return Ok(tipoDeMovimentacaoDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoMovimentacaoDTO tipoMovimentacaoDTO)
        {
            if (tipoMovimentacaoDTO is null)
                return BadRequest("Dados inválidos");

            var tipoMovimentacao = _mapper.Map<TipoMovimentacao>(tipoMovimentacaoDTO);
            await _tipoMovimentacaoRepository.CreateAsync(tipoMovimentacao);

            return new CreatedAtRouteResult("GetTipoMovimentacao", new { id = tipoMovimentacaoDTO.Id }, tipoMovimentacaoDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] TipoMovimentacaoDTO tipoMovimentacaoDTO)
        {
            if (id != tipoMovimentacaoDTO.Id)
                return BadRequest();

            if (tipoMovimentacaoDTO is null)
                return BadRequest();

            var tipoMovimentacao = _mapper.Map<TipoMovimentacao>(tipoMovimentacaoDTO);

            await _tipoMovimentacaoRepository.UpdateAsync(tipoMovimentacao);

            return Ok(tipoMovimentacaoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoMovimentacao = await _tipoMovimentacaoRepository.GetByIdAsync(id);
            if (tipoMovimentacao is null)
                return NotFound("O Tipo de Movimentacao informado não existe!");

            await _tipoMovimentacaoRepository.DeleteAsync(id);
            return Ok(tipoMovimentacao);
        }
    }
}
