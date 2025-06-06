using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;
using VKR2.DAL;


namespace VKR2.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaySummaController : ControllerBase
    {
        private readonly PostgresContext _context;

        public PaySummaController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<PaySummaDTO>>> GetPaySummas()
        {
            return await _context.Paysummas
                .Select(p => new PaySummaDTO
                {
                    Payfactcd = p.Payfactcd,
                    Pupilcd = p.Pupilcd,
                    Societycd = p.Societycd,
                    Paysum = p.Paysum,
                    Paydate = p.Paydate,
                    Paymonth = p.Paymonth,
                    Payyear = p.Payyear
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PaySummaDTO>> GetPaySumma(int id)
        {
            var pay = await _context.Paysummas.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }

            return new PaySummaDTO
            {
                Payfactcd = pay.Payfactcd,
                Pupilcd = pay.Pupilcd,
                Societycd = pay.Societycd,
                Paysum = pay.Paysum,
                Paydate = pay.Paydate,
                Paymonth = pay.Paymonth,
                Payyear = pay.Payyear
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<PaySummaDTO>> CreatePaySumma(PaySummaCreateDTO dto)
        {
            var pay = new Paysumma
            {
                Pupilcd = dto.Pupilcd,
                Societycd = dto.Societycd,
                Paysum = dto.Paysum,
                Paydate = dto.Paydate,
                Paymonth = dto.Paymonth,
                Payyear = dto.Payyear
            };

            _context.Paysummas.Add(pay);
            await _context.SaveChangesAsync();

            var result = new PaySummaDTO
            {
                Payfactcd = pay.Payfactcd,
                Pupilcd = pay.Pupilcd,
                Societycd = pay.Societycd,
                Paysum = pay.Paysum,
                Paydate = pay.Paydate,
                Paymonth = pay.Paymonth,
                Payyear = pay.Payyear
            };

            return CreatedAtAction(nameof(GetPaySumma), new { id = pay.Payfactcd }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePaySumma(int id, PaySummaCreateDTO dto)
        {
            var pay = await _context.Paysummas.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }

            pay.Pupilcd = dto.Pupilcd;
            pay.Societycd = dto.Societycd;
            pay.Paysum = dto.Paysum;
            pay.Paydate = dto.Paydate;
            pay.Paymonth = dto.Paymonth;
            pay.Payyear = dto.Payyear;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePaySumma(int id)
        {
            var pay = await _context.Paysummas.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }

            _context.Paysummas.Remove(pay);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}