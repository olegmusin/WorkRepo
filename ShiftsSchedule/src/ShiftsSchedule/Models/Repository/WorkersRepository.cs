using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Execution;
using ShiftsSchedule.Models;

namespace ShiftsSchedule.Models.Repository
{
    public class WorkersRepository : GenericRepository<Worker>
    {
        private ShiftsCalendarContext _context;
        private ILogger<WorkersRepository> _logger;

        public WorkersRepository(ShiftsCalendarContext context, ILogger<WorkersRepository> logger)
            : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }

        public Worker GetSingle(int workerId)
        {
            var query = base.GetAll().FirstOrDefault(x => x.Id == workerId);
            if (query != null)
                _logger.LogInformation($"Worker  with Id: {workerId} fetched");
            else
                _logger.LogError($"Worker  with Id: {workerId} CAN'T BE fetched!");
            return query;
        }

        public IQueryable<Shift> GetWorkerShifts(int Id)
        {
            var q = from s in _context.Shifts
                    where s.Workers.Any(ws => ws.WorkerId == Id)
                    select s;
            return q;

        }
        public void Delete(int workerId)
        {
            _context.Entry(new Worker { Id = workerId }).State = EntityState.Deleted;
        }

    }
}