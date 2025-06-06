using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VKR2.Domain.DTOs;
using VKR2.DAL.Models;
using VKR2.DAL;


namespace VKR2.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "parent")]
    public class ParentPortalController : ControllerBase
    {
        private readonly PostgresContext _context;

        public ParentPortalController(PostgresContext context)
        {
            _context = context;
        }

        private int GetParentId()
        {
            var parentClaim = User.FindFirst("ParentCd");
            return parentClaim != null ? int.Parse(parentClaim.Value) : 0;
        }

        private List<int> GetChildIds(int parentId)
        {
            return _context.Familyconnections
                .Where(fc => fc.Parentcd == parentId)
                .Select(fc => fc.Pupilcd)
                .ToList();
        }

        [HttpGet("children")]
        public ActionResult<IEnumerable<PupilWithGenderDTO>> GetChildren()
        {
            int parentId = GetParentId();
            var children = _context.Familyconnections
                .Where(fc => fc.Parentcd == parentId)
                .Include(fc => fc.Pupil)
                    .ThenInclude(p => p.Groupdistributions)
                        .ThenInclude(gd => gd.Group)
                .Select(fc => new PupilWithGenderDTO
                {
                    Pupilcd = fc.Pupil.Pupilcd,
                    Fio = fc.Pupil.Fio,
                    Dateofbirth = fc.Pupil.Dateofbirth,
                    Birthcertificatenumber = fc.Pupil.Birthcertificatenumber,
                    Gender = fc.Pupil.Gender, // Теперь поле Gender прямо в таблице PUPILS
                    GroupTitle = fc.Pupil.Groupdistributions
                        .OrderByDescending(gd => gd.Distributioncd)
                        .FirstOrDefault().Group.Title
                })
                .ToList();

            return Ok(children);
        }

        [HttpGet("attendance")]
        public ActionResult<IEnumerable<AttendanceDTO>> GetAttendance()
        {
            int parentId = GetParentId();
            var pupilIds = GetChildIds(parentId);

            var attendances = _context.Attendances
                .Where(a => pupilIds.Contains(a.Pupilcd))
                .Select(a => new AttendanceDTO
                {
                    Pupilcd = a.Pupilcd,
                    Visitdate = a.Visitdate,
                    Availability = a.Availability
                })
                .ToList();

            return Ok(attendances);
        }

        [HttpGet("schedule")]
        public ActionResult<IEnumerable<ScheduleDTO>> GetSchedule()
        {
            var schedules = _context.Schedules
                .Select(s => new ScheduleDTO
                {
                    Schedulecd = s.Schedulecd,
                    Scheduledate = s.Scheduledate,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    Groupcd = s.Groupcd,
                    Lessoncd = s.Lessoncd,
                    Cabinetcd = s.Cabinetcd,
                    Workercd = s.Workercd
                })
                .ToList();

            return Ok(schedules);
        }

        [HttpGet("groups")]
        public ActionResult<IEnumerable<GroupFullDTO>> GetGroups()
        {
            var groups = _context.Groups
                .Include(g => g.Groupdistributions)
                .Select(g => new GroupFullDTO
                {
                    Groupcd = g.Groupcd,
                    Title = g.Title,
                    Minage = g.Minage,
                    Maxage = g.Maxage,
                    Numberofseats = g.Numberofseats,
                    CurrentCount = g.Groupdistributions.Count
                })
                .AsEnumerable() // Переключаем на клиентскую обработку
                .ToList();

            return Ok(groups);
        }

        [HttpGet("societies")]
        public ActionResult<IEnumerable<SocietyFullDTO>> GetSocieties()
        {
            var societies = _context.Societies
                .Include(g => g.Societydistributions)
                .Select(g => new SocietyFullDTO
                {
                    Societycd = g.Societycd,
                    Title = g.Title,
                    Minage = g.Minage,
                    Maxage = g.Maxage,
                    Price = g.Price,
                    Numberofseats = g.Numberofseats,
                    CurrentCount = g.Societydistributions.Count
                })
                .AsEnumerable() // Переключаем на клиентскую обработку
                .ToList();

            return Ok(societies);
        }

        [HttpGet("available-societies/{pupilId}")]
        public ActionResult<IEnumerable<SocietyDTO>> GetAvailableSocieties(int pupilId)
        {
            var pupil = _context.Pupils.FirstOrDefault(p => p.Pupilcd == pupilId);
            if (pupil == null) return NotFound("Ребёнок не найден.");

            var birthDate = pupil.Dateofbirth.ToDateTime(TimeOnly.MinValue);
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;

            var societies = _context.Societies
                .Where(s => s.Minage <= age && s.Maxage >= age)
                .Select(s => new SocietyDTO
                {
                    Societycd = s.Societycd,
                    Title = s.Title,
                    Minage = s.Minage,
                    Maxage = s.Maxage,
                    Price = s.Price
                })
                .ToList();

            return Ok(societies);
        }

        [HttpGet("enrollments")]
        public ActionResult<IEnumerable<EnrollmentDTO>> GetEnrollments()
        {
            int parentId = GetParentId();

            var enrollments = _context.Societydistributions
                .Include(sd => sd.Pupil)
                .Include(sd => sd.Society)
                .Where(sd => _context.Familyconnections
                    .Any(fc => fc.Parentcd == parentId && fc.Pupilcd == sd.Pupilcd))
                .Select(sd => new EnrollmentDTO
                {
                    EnrollmentId = sd.Distributioncd,
                    PupilName = sd.Pupil.Fio,
                    SocietyTitle = sd.Society.Title,
                    MinAge = sd.Society.Minage,
                    MaxAge = sd.Society.Maxage,
                    Price = sd.Society.Price
                })
                .ToList();

            return Ok(enrollments);
        }

        // Запись в кружок
        [HttpPost("enroll")]
        public ActionResult<SocietyEnrollmentDTO> EnrollInSociety([FromBody] SocietyDistributionCreateDTO dto)
        {
            // Проверка прав доступа родителя
            int parentId = GetParentId();
            if (!_context.Familyconnections.Any(fc => fc.Parentcd == parentId && fc.Pupilcd == dto.Pupilcd))
                return Forbid("Данный ребенок не принадлежит текущему родителю");

            // Проверка существования кружка
            var society = _context.Societies
                .Include(s => s.Societydistributions)
                .FirstOrDefault(s => s.Societycd == dto.Societycd);

            if (society == null)
                return NotFound("Кружок не найден");

            // Проверка возраста ребенка
            var pupil = _context.Pupils.FirstOrDefault(p => p.Pupilcd == dto.Pupilcd);
            if (pupil == null)
                return NotFound("Ребенок не найден");

            var birthDate = pupil.Dateofbirth.ToDateTime(TimeOnly.MinValue);
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;

            if (age < society.Minage || age > society.Maxage)
                return BadRequest($"Ребенок не подходит по возрасту (требуется: {society.Minage}-{society.Maxage} лет)");

            // Проверка наличия свободных мест
            if (society.Societydistributions.Count >= society.Numberofseats)
                return BadRequest("В кружке нет свободных мест");

            // Проверка дублирования записи
            if (_context.Societydistributions.Any(sd => sd.Pupilcd == dto.Pupilcd && sd.Societycd == dto.Societycd))
                return BadRequest("Ребенок уже записан в этот кружок");

            // Создание записи
            var distribution = new Societydistribution
            {
                Pupilcd = dto.Pupilcd,
                Societycd = dto.Societycd
            };

            _context.Societydistributions.Add(distribution);
            _context.SaveChanges();

            // Возвращаем полные данные для отображения
            var enrollment = _context.Societydistributions
                .Include(sd => sd.Pupil)
                .Include(sd => sd.Society)
                .First(sd => sd.Distributioncd == distribution.Distributioncd);

            return Ok(new SocietyEnrollmentDTO
            {
                DistributionId = enrollment.Distributioncd,
                PupilFio = enrollment.Pupil.Fio,
                SocietyTitle = enrollment.Society.Title,
                MinAge = enrollment.Society.Minage,
                MaxAge = enrollment.Society.Maxage,
                Price = enrollment.Society.Price
            });
        }

        // Отмена записи
        [HttpDelete("enroll/{distributionId}")]
        public IActionResult CancelEnrollment(int distributionId)
        {
            int parentId = GetParentId();

            var enrollment = _context.Societydistributions
                .Include(sd => sd.Pupil)
                .ThenInclude(p => p.Familyconnections)
                .FirstOrDefault(sd => sd.Distributioncd == distributionId);

            if (enrollment == null)
                return NotFound("Запись не найдена");

            // Проверка что родитель имеет доступ к этому ребенку
            if (!enrollment.Pupil.Familyconnections.Any(fc => fc.Parentcd == parentId))
                return Forbid("Нет прав для отмены этой записи");

            _context.Societydistributions.Remove(enrollment);
            _context.SaveChanges();

            return Ok("Запись отменена успешно");
        }



        [HttpGet("nachisleniya")]
        public ActionResult<IEnumerable<NachislSummaDTO>> GetNachisleniya()
        {
            int parentId = GetParentId();
            var pupilIds = GetChildIds(parentId);

            var nachisleniya = _context.Nachislsummas
            .Where(n => pupilIds.Contains(n.Pupilcd))
                .Select(n => new NachislSummaDTO
                {
                    Pupilcd = n.Pupilcd,
                    Nachislsum = n.Nachislsum,
                    Nachislmonth = n.Nachislmonth,
                    Nachislyear = n.Nachislyear,
                    Societycd = n.Societycd
                })
                .ToList();

            return Ok(nachisleniya);
        }

        [HttpGet("payments")]
        public ActionResult<IEnumerable<PaySummaDTO>> GetPayments()
        {
            int parentId = GetParentId();
            var pupilIds = GetChildIds(parentId);

            var payments = _context.Paysummas
                .Where(p => pupilIds.Contains(p.Pupilcd))
                .Select(p => new PaySummaDTO
                {
                    Pupilcd = p.Pupilcd,
                    Paysum = p.Paysum,
                    Paydate = p.Paydate,
                    Paymonth = p.Paymonth,
                    Payyear = p.Payyear,
                    Societycd = p.Societycd
                })
                .ToList();

            return Ok(payments);
        }
    }
}