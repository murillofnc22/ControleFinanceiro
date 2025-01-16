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
    public class InstituicaoFinanceiraController : ControllerBase
    {
        private readonly IInstituicaoFinanceiraRepository _instituicaoFinanceiraRepository;
        private readonly IMapper _mapper;

        public InstituicaoFinanceiraController(IInstituicaoFinanceiraRepository instituicaoFinanceiraRepository,
            IMapper mapper)
        {
            _instituicaoFinanceiraRepository = instituicaoFinanceiraRepository; 
            _mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstituicaoFinanceiraDTO>>> Get()
        {
            var instituicoes = await _instituicaoFinanceiraRepository.GetAllAsync();
            if (instituicoes is null)
            {
                return NotFound("Não existem Instituções Financeiras cadastradas.");
            }
            var instituicoesDto = _mapper.Map<IEnumerable<InstituicaoFinanceiraDTO>>(instituicoes);
            return Ok(instituicoesDto);
        }
        [HttpGet("{id:int}", Name = "GetInstituicao")]
        public async Task<ActionResult<InstituicaoFinanceiraDTO>> Get(int id)
        {
            var instituicoes = await _instituicaoFinanceiraRepository.GetByIdAsync(id);
            if (instituicoes is null)
            {
                return NotFound("Instituição não encontrada");
            }

            var instituicoesDto = _mapper.Map<InstituicaoFinanceiraDTO>(instituicoes);
            return Ok(instituicoesDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InstituicaoFinanceiraDTO instituicaoFinanceiraDTO)
        {
            if (instituicaoFinanceiraDTO == null)
                return BadRequest("Dados inválidos");

            var instituicao = _mapper.Map<InstituicaoFinanceira>(instituicaoFinanceiraDTO);

            await _instituicaoFinanceiraRepository.CreateAsync(instituicao);

            return new CreatedAtRouteResult("GetInstituicao", new { id = instituicaoFinanceiraDTO.Id },
                instituicaoFinanceiraDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] InstituicaoFinanceiraDTO instituicaoFinanceiraDTO)
        {
            if (id != instituicaoFinanceiraDTO.Id)
                return BadRequest();

            if (instituicaoFinanceiraDTO == null)
                return BadRequest();

            var instituicao = _mapper.Map<InstituicaoFinanceira>(instituicaoFinanceiraDTO);

            await _instituicaoFinanceiraRepository.UpdateAsync(instituicao);

            return Ok(instituicaoFinanceiraDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var instituicao = await _instituicaoFinanceiraRepository.GetByIdAsync(id);
            if (instituicao == null)
            {
                return NotFound("Instituição não encontrada");
            }

            await _instituicaoFinanceiraRepository.DeleteAsync(id);

            return Ok(instituicao);
        }
    }
}
