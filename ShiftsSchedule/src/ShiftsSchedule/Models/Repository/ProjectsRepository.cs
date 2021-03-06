﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShiftsSchedule.Data;

namespace ShiftsSchedule.Models.Repository
{
    public class ProjectsRepository : GenericRepository<Project>
    {
        private ILogger<ProjectsRepository> _logger;
        private ShiftsScheduleContext _context;

        public ProjectsRepository(ShiftsScheduleContext context, ILogger<ProjectsRepository> logger) : base(context, logger)
        {
             _logger = logger;
            _context = context;
        }

        public Project GetSingle(int projectId)
        {
            var query = GetAll().FirstOrDefault(x => x.Id == projectId);
            return query;
        }

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
        public void Delete(int projectId)
        {
            _context.Projects.Single(p => p.Id == projectId).IsCanceled = true;
            //_context.Entry(new Project { Id = projectId }).State = EntityState.Deleted;
        }

        public Project ProjectByShiftId(int shiftId)
        {
            return _context.Set<Project>()
                           .Single(p => p.Shifts.Any(s => s.Id == shiftId));       
        }

        public void DeleteShift(int shiftId)
        {
            var delShift = _context.Shifts.First(s => s.Id == shiftId);
            //_context.Shifts.Remove(delShift);
            delShift.IsCanceled = true;
        }

        public List<Specialty> GetAllSpecialties()
        {
            return _context.Specialties.ToList();
        }
    }
}

