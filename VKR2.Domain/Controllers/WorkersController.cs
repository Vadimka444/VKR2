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
    public class WorkersController : ControllerBase
    {
        private readonly PostgresContext _context;

        public WorkersController(PostgresContext context) => _context = context;

        [HttpGet]
        [Authorize(Roles = "admin,worker, parent")]
        public async Task<ActionResult<IEnumerable<WorkerDTO>>> GetWorkers()
        {
            return await _context.Workers
                .Select(w => new WorkerDTO
                {
                    Workercd = w.Workercd,
                    Fio = w.Fio,
                    Passport = w.Passport,
                    Phone = w.Phone,
                    Address = w.Address,
                    Dateofbirth = w.Dateofbirth
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,worker")]
        public async Task<ActionResult<WorkerDTO>> GetWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) return NotFound();

            return new WorkerDTO
            {
                Workercd = worker.Workercd,
                Fio = worker.Fio,
                Passport = worker.Passport,
                Phone = worker.Phone,
                Address = worker.Address,
                Dateofbirth = worker.Dateofbirth
            };
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<WorkerDTO>> CreateWorker(WorkerCreateDTO dto)
        {
            var worker = new Worker
            {
                Fio = dto.Fio,
                Passport = dto.Passport,
                Phone = dto.Phone,
                Address = dto.Address,
                Dateofbirth = dto.Dateofbirth
            };

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorker), new { id = worker.Workercd }, new WorkerDTO
            {
                Workercd = worker.Workercd,
                Fio = worker.Fio,
                Passport = worker.Passport,
                Phone = worker.Phone,
                Address = worker.Address,
                Dateofbirth = worker.Dateofbirth
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateWorker(int id, WorkerCreateDTO dto)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) return NotFound();

            worker.Fio = dto.Fio;
            worker.Passport = dto.Passport;
            worker.Phone = dto.Phone;
            worker.Address = dto.Address;
            worker.Dateofbirth = dto.Dateofbirth;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null) return NotFound();

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}