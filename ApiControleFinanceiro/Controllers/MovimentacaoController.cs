using ApiControleFinanceiro.DTOs;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiControleFinanceiro.Controllers
{
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IMapper _mapper;

        public MovimentacaoController(IMovimentacaoRepository movimentacaoRepository, IMapper mapper)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> Get()
        {
            var movimentacoes = await _movimentacaoRepository.GetAllAsync();
            if (movimentacoes is null)
                return NotFound("Não existem movimentações cadastradas!");
            
            var movimentacoesDTO = _mapper.Map<IEnumerable<MovimentacaoDTO>>(movimentacoes);
            return Ok(movimentacoesDTO);
        }
        [HttpGet("{id:int}", Name = "GetMovimentacao")]
        public async Task<ActionResult<MovimentacaoDTO>> Get(int id)
        {
            var movimentacao = await _movimentacaoRepository.GetByIdAsync(id);
            if (movimentacao is null)
                return NotFound("A movimentação informada não existe!");

            var movimentacaoDTO = _mapper.Map<MovimentacaoDTO>(movimentacao);
            return Ok(movimentacaoDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovimentacaoDTO movimentacaoDTO) 
        {
            if (movimentacaoDTO is null)
                return BadRequest();

            var movimentacao = _mapper.Map<Movimentacao>(movimentacaoDTO);
            await _movimentacaoRepository.CreateAsync(movimentacao);

            return new CreatedAtRouteResult("GetMovimentacao", new {id = movimentacaoDTO.Id}, movimentacaoDTO);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] MovimentacaoDTO movimentacaoDTO)
        {
            if(id != movimentacaoDTO.Id)
                return BadRequest();

            if(movimentacaoDTO is null)
                return BadRequest();

            var movimentacao = _mapper.Map<Movimentacao>(movimentacaoDTO);
            
            await _movimentacaoRepository.UpdateAsync(movimentacao);

            return Ok(movimentacaoDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movimentacao = await _movimentacaoRepository.GetByIdAsync(id);
            if (movimentacao is null)
                return NotFound("A movimentação informada não existe!");

            await _movimentacaoRepository.DeleteAsync(id);
            return Ok(movimentacao);
        }
    }
}
