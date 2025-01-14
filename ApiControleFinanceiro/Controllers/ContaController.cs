using ApiControleFinanceiro.DTOs;
using ApiControleFinanceiro.Entities;
using ApiControleFinanceiro.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiControleFinanceiro.Controllers
{
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;
        public ContaController(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaDTO>>> Get()
        {
            var contas = await _contaRepository.GetAllAsync();
            if(contas is null)
                return NotFound("Não existem Contas cadastradas!");
            
            var contasDTO = _mapper.Map<IEnumerable<ContaDTO>>(contas);
            return Ok(contasDTO);
        }

        [HttpGet("{id:int}", Name = "GetConta")]
        public async Task<ActionResult<ContaDTO>> Get(int id)
        {
            var conta = await _contaRepository.GetByIdAsync(id);
            if (conta is null)
                return NotFound("A Conta informada não existe!");

            var contaDTO = _mapper.Map<ContaDTO>(conta);
            return Ok(contaDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ContaDTO contaDTO)
        {
            if (contaDTO is null)
                return BadRequest("Dados inválidos");
            
            var conta = _mapper.Map<Conta>(contaDTO);
            await _contaRepository.CreateAsync(conta);
            
            return new CreatedAtRouteResult("GetConta", new { id = contaDTO.Id }, contaDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ContaDTO contaDTO)
        {
            if (id != contaDTO.Id)
                return BadRequest();
            
            if (contaDTO is null)
                return BadRequest();

            var conta = _mapper.Map<Conta>(contaDTO);
            
            await _contaRepository.UpdateAsync(conta);
            
            return Ok(contaDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var conta = await _contaRepository.GetByIdAsync(id);
            if (conta is null)
                return NotFound("A Conta informada não existe!");

            await _contaRepository.DeleteAsync(id);
            return Ok(conta);
        }
    }
}
