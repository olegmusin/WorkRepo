using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ShiftsSchedule.Models.Repository
{
    public class ShiftsRepository : GenericRepository<Shift>
    {
        private ShiftsCalendarContext _context;
        private ILogger<ShiftsRepository> _logger;

        public ShiftsRepository(ShiftsCalendarContext context, ILogger<ShiftsRepository> logger) : base(context, logger)
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



    }
}

