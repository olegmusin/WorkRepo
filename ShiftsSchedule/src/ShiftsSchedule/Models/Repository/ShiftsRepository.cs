using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using ShiftsSchedule.Data;

namespace ShiftsSchedule.Models.Repository
{
    public class ShiftsRepository : GenericRepository<Shift>
    {
        private ShiftsScheduleContext _context;
        private ILogger<ShiftsRepository> _logger;

        public ShiftsRepository(ShiftsScheduleContext context, ILogger<ShiftsRepository> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }

        public Shift GetSingle(int shiftId)
        {
            var query = GetAll().FirstOrDefault(x => x.Id == shiftId);
            return query;
        }

        public IEnumerable<Shift> GetAllWithProjects()
        {
            return _context.Shifts.Include(s => s.Project);

        }

        public IEnumerable<Worker> WorkersAttendingShift(int shiftId)
        {
            var shift = _context.Shifts
                .Include(s => s.Workers)
                .ThenInclude(ws => ws.Worker)
                .Single(s => s.Id == shiftId);
            return shift.Workers.Select(ws => ws.Worker);
        }

    }
}

