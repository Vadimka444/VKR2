using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VKR2.DAL.QueryResults;
using VKR2.DAL;

namespace VKR2.Domain.Services
{
    public class DatabaseFunctionsService
    {
        private readonly PostgresContext _context;

        public DatabaseFunctionsService(PostgresContext context)
        {
            _context = context;
        }

        public async Task<List<GroupScheduleResult>> GetGroupSchedule(string groupTitle)
        {
            return await _context.GroupScheduleResults
                .FromSqlRaw("SELECT * FROM get_group_schedule_by_name({0})", groupTitle)
                .ToListAsync();
        }

        public async Task<List<PupilAttendanceResult>> GetPupilAttendance(string pupilFio)
        {
            return await _context.PupilAttendanceResults
                .FromSqlRaw("SELECT * FROM get_pupil_attendance_by_name({0})", pupilFio)
                .ToListAsync();
        }

        public async Task<List<PupilParentResult>> GetPupilParents(string pupilFio)
        {
            return await _context.PupilParentsResults
                .FromSqlRaw("SELECT * FROM get_pupil_parents_by_name({0})", pupilFio)
                .ToListAsync();
        }

        public async Task<List<PupilsInfoResult>> GetGroupPupils(string groupTitle)
        {
            return await _context.GroupPupilsResults
                .FromSqlRaw("SELECT * FROM get_group_pupils({0})", groupTitle)
                .ToListAsync();
        }

        public async Task<List<PupilsInfoResult>> GetSocietyPupils(string societyTitle)
        {
            return await _context.SocietyPupilsResults
                .FromSqlRaw("SELECT * FROM get_society_pupils({0})", societyTitle)
                .ToListAsync();
        }

        public async Task<SocietyPaymentResult> GetSocietyPayments(string societyTitle, DateOnly dateFrom, DateOnly dateTo)
        {
            return await _context.SocietyPaymentsResults
                .FromSqlRaw("SELECT * FROM get_total_payment_for_society({0}, {1}, {2})", societyTitle, dateFrom, dateTo)
                .FirstOrDefaultAsync();
        }

        public async Task<List<WorkerSocietyProfitResult>> GetWorkerSocietyProfit(string workerFio, DateOnly dateFrom, DateOnly dateTo)
        {
            return await _context.WorkerSocietyProfitResults
                .FromSqlRaw("SELECT * FROM get_worker_society_profit({0}, {1}, {2})", workerFio, dateFrom, dateTo)
                .ToListAsync();
        }

        public async Task<List<PupilsPaymentsReportResult>> GetPupilsPaymentsReport(DateOnly dateFrom, DateOnly dateTo)
        {
            return await _context.PupilsPaymentsReportResults
                .FromSqlRaw("SELECT * FROM get_pupils_payments_report({0}, {1})", dateFrom, dateTo)
                .ToListAsync();
        }

    }
}