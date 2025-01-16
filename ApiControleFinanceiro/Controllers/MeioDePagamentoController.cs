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
    public class MeioDePagamentoController : ControllerBase
    {
        IMeioDePagamentoRepository _meioDePagamentoRepository;
        IMapper _mapper;
        public MeioDePagamentoController(IMeioDePagamentoRepository meioDePagamentoRepository, IMapper mapper)
        {
            _meioDePagamentoRepository = meioDePagamentoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeioDePagamentoDTO>>> Get()
        {
            var meioPagamento = await _meioDePagamentoRepository.GetAllAsync();
            if(meioPagamento is null) 
                return NotFound("Nenhum Meio de Pagamento foi encontrado!");

            var meioPagamentoDto = _mapper.Map<IEnumerable<MeioDePagamentoDTO>>(meioPagamento);
            return Ok(meioPagamentoDto);
        }

        [HttpGet("{id:int}", Name = "GetMeioDePagamento")]
        public async Task<ActionResult<MeioDePagamentoDTO>> Get(int id)
        {
            var meioPagamento = await _meioDePagamentoRepository.GetByIdAsync(id);
            if (meioPagamento is null)
                return NotFound("O Meio de Pagamento informado não foi encontrado!");

            var meioPagamentoDto = _mapper.Map<MeioDePagamentoDTO>(meioPagamento);
            return Ok(meioPagamentoDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MeioDePagamentoDTO meioDePagamentoDTO)
        {
            if (meioDePagamentoDTO is null)
                return BadRequest("Dados Inválidos");

            var meioPagamento = _mapper.Map<MeioDePagamento>(meioDePagamentoDTO);
            await _meioDePagamentoRepository.CreateAsync(meioPagamento);

            return new CreatedAtRouteResult("GetMeioDePagamento", new { id = meioDePagamentoDTO.Id }, meioDePagamentoDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] MeioDePagamentoDTO meioDePagamentoDTO)
        {
            if (id != meioDePagamentoDTO.Id)
                return BadRequest();

            if (meioDePagamentoDTO is null)
                return BadRequest();

            var meioDePagamento = _mapper.Map<MeioDePagamento>(meioDePagamentoDTO);

            await _meioDePagamentoRepository.UpdateAsync(meioDePagamento);

            return Ok(meioDePagamentoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var meioDePagamento = await _meioDePagamentoRepository.GetByIdAsync(id);
            if (meioDePagamento is null)
                return NotFound("O Meio de Pagamento informado não existe!");

            await _meioDePagamentoRepository.DeleteAsync(id);
            return Ok(meioDePagamento);
        }
    }
}
