using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ShiftsCalendarASP.Models.Repository
{
    public class ProjectsRepository : GenericRepository<Project>
    {
        private ILogger<ProjectsRepository> _logger;
        private ShiftsCalendarContext _context;

        public ProjectsRepository(ShiftsCalendarContext context, ILogger<ProjectsRepository> logger) : base(context, logger)
        {
             _logger = logger;
            _context = context;
        }

        public Project GetSingle(int shiftId)
        {
            var query = GetAll().FirstOrDefault(x => x.Id == shiftId);
            return query;
        }

        //public override IQueryable<Project> GetAll()
        //{
        //    return base.GetAll().Include(p => p.Shifts);
        //}

        public override IQueryable<Project> FindBy(Expression<Func<Project, bool>> predicate)
        {
            var query = _context.Set<Project>()
                .Include(p => p.Shifts)
                .Where(predicate);
            return query;
          
        }

        public void AddShiftForProject(int projectId, Shift newShift)
        {
            var project = this.FindBy(p => p.Id == projectId).FirstOrDefault();
            if (project != null)
            {
                project.Shifts.Add(newShift);
                _context.Shifts.Add(newShift);
            }
        }

        public void DeleteShift(int shiftId)
        {
            var delShift = _context.Shifts.First(s => s.Id == shiftId);
          
            _context.Shifts.Remove(delShift);

        }

    }
}

