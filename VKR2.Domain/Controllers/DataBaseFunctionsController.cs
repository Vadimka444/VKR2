using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;
using VKR2.DAL;
using VKR2.Domain.Services;


namespace VKR2.Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseFunctionsController : ControllerBase
    {
        private readonly DatabaseFunctionsService _service;

        public DatabaseFunctionsController(DatabaseFunctionsService service)
        {
            _service = service;
        }

        [HttpGet("group-schedule")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> GetGroupSchedule([FromQuery] string groupTitle)
        {
            try
            {
                if (string.IsNullOrEmpty(groupTitle))
                    return BadRequest("Название группы обязательно");

                var result = await _service.GetGroupSchedule(groupTitle);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pupil-attendance")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> GetPupilAttendance([FromQuery] string pupilFio)
        {
            try
            {
                if (string.IsNullOrEmpty(pupilFio))
                    return BadRequest("ФИО воспитанника обязательно");

                var result = await _service.GetPupilAttendance(pupilFio);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pupil-parents")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> GetPupilParents([FromQuery] string pupilFio)
        {
            try
            {
                if (string.IsNullOrEmpty(pupilFio))
                    return BadRequest("ФИО воспитанника обязательно");

                var result = await _service.GetPupilParents(pupilFio);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("group-pupils")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> GetGroupPupils([FromQuery] string groupTitle)
        {
            try
            {
                if (string.IsNullOrEmpty(groupTitle))
                    return BadRequest("Название группы обязательно");

                var result = await _service.GetGroupPupils(groupTitle);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("society-pupils")]
        [Authorize(Roles = "admin,worker")]
        public async Task<IActionResult> GetSocietyPupils([FromQuery] string societyTitle)
        {
            try
            {
                if (string.IsNullOrEmpty(societyTitle))
                    return BadRequest("Название кружка обязательно");

                var result = await _service.GetSocietyPupils(societyTitle);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("society-payments")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetSocietyPayments([FromQuery] string societyTitle, [FromQuery] DateOnly dateFrom, [FromQuery] DateOnly dateTo)
        {
            try
            {
                if (string.IsNullOrEmpty(societyTitle))
                    return BadRequest("Название кружка обязательно");

                var result = await _service.GetSocietyPayments(societyTitle, dateFrom, dateTo);
                return result == null
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("worker-society-profit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetWorkerSocietyProfit([FromQuery] string workerFio, [FromQuery] DateOnly dateFrom, [FromQuery] DateOnly dateTo)
        {
            try
            {
                if (string.IsNullOrEmpty(workerFio))
                    return BadRequest("ФИО сотрудника обязательно");

                var result = await _service.GetWorkerSocietyProfit(workerFio, dateFrom, dateTo);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Сводный отчёт по оплатам воспитанников
        [HttpGet("pupils-payments-report")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPupilsPaymentsReport([FromQuery] DateOnly dateFrom, [FromQuery] DateOnly dateTo)
        {
            try
            {
                var result = await _service.GetPupilsPaymentsReport(dateFrom, dateTo);
                return result.Count == 0
                    ? NotFound()
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}