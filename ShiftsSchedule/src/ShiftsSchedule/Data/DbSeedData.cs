﻿using ShiftsSchedule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShiftsSchedule.Data
{
    public class DbSeedData
    {
        private readonly ShiftsScheduleContext _context;

        public DbSeedData(ShiftsScheduleContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Users.Any())
            {
                var roleAdmins = new IdentityRole() { Id = "1", Name = "admins" };
                var roleWorkers = new IdentityRole() { Id = "2", Name = "workers" };
                var roleOperators = new IdentityRole() { Id = "3", Name = "operators" };

                var wkr1 = new Worker
                {
                    Name = "Andrew Piters",
                    Salary = 300,
                    Specialty = "Carpenter",
                    Shifts = new List<WorkerShift>()
                };

                var wkr2 = new Worker
                {
                    Name = "Brice Lambson",
                    Salary = 500,
                    Specialty = "Locksmith",
                    Shifts = new List<WorkerShift>()
                };


                var wkr3 = new Worker
                {
                    Name = "Rowan Miller",
                    Salary = 450,
                    Specialty = "Fitter",
                    Shifts = new List<WorkerShift>()
                };
                _context.Workers.AddRange(wkr1, wkr2, wkr3);

                var sft1 = new Shift
                {
                    Date = new System.DateTime(2016, 11, 01),
                    Workers = new List<WorkerShift>()
                };
                var sft2 = new Shift
                {
                    Date = new System.DateTime(2016, 11, 03),
                    Workers = new List<WorkerShift>()
                };
                var sft3 = new Shift
                {
                    Date = new System.DateTime(2016, 11, 04),
                    Workers = new List<WorkerShift>()
                };
                var sft4 = new Shift
                {
                    Date = new System.DateTime(2016, 08, 23),
                    Workers = new List<WorkerShift>()
                };
                var sft5 = new Shift
                {
                    Date = new System.DateTime(2016, 08, 24),
                    Workers = new List<WorkerShift>()
                };

                _context.Shifts.AddRange(sft1, sft2, sft3, sft4, sft5);


                var pr1 = new Project()
                {
                    Name = "Pines",
                    Address = "25/1, Pine ave",
                    NumberOfWorkers = 2,
                    Shifts = new List<Shift> { sft1, sft2, sft3 }
                };
                var pr2 = new Project
                {
                    Name = "Midwaters",
                    Address = "34, Midwaters drive",
                    NumberOfWorkers = 3,
                    Shifts = new List<Shift> { sft4, sft5 }
                };
                _context.Projects.AddRange(pr1, pr2);

                var wrkshft1 = new WorkerShift { Worker = wkr1, Shift = sft1 };
                var wrkshft2 = new WorkerShift { Worker = wkr1, Shift = sft2 };
                var wrkshft3 = new WorkerShift { Worker = wkr1, Shift = sft3 };
                var wrkshft4 = new WorkerShift { Worker = wkr2, Shift = sft4 };
                var wrkshft5 = new WorkerShift { Worker = wkr2, Shift = sft5 };
                var wrkshft6 = new WorkerShift { Worker = wkr3, Shift = sft4 };
                var wrkshft7 = new WorkerShift { Worker = wkr3, Shift = sft5 };
                var wrkshft8 = new WorkerShift { Worker = wkr3, Shift = sft1 };
                var wrkshft9 = new WorkerShift { Worker = wkr3, Shift = sft2 };
                var wrkshft10 = new WorkerShift { Worker = wkr3, Shift = sft3 };

                wkr1.Shifts.Add(wrkshft1);
                wkr1.Shifts.Add(wrkshft2);
                wkr1.Shifts.Add(wrkshft3);
                wkr2.Shifts.Add(wrkshft4);
                wkr2.Shifts.Add(wrkshft5);
                wkr3.Shifts.Add(wrkshft6);
                wkr3.Shifts.Add(wrkshft7);
                wkr3.Shifts.Add(wrkshft8);
                wkr3.Shifts.Add(wrkshft9);
                wkr3.Shifts.Add(wrkshft10);

                var user1 = new ApplicationUser {UserName = "worker1"};
                user1.Roles.Add(new IdentityUserRole<string> { RoleId = "2", UserId = user1.Id });
                wkr1.UserId = user1.Id;
                var user2 = new ApplicationUser { UserName = "worker2" };
                user2.Roles.Add(new IdentityUserRole<string> { RoleId = "2", UserId = user2.Id });
                wkr2.UserId = user2.Id;
                var user3 = new ApplicationUser { UserName = "worker3" };
                user3.Roles.Add(new IdentityUserRole<string> { RoleId = "2", UserId = user3.Id });
                wkr3.UserId = user3.Id;

                var admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "oleg.y.musin@gmail.com",
                };
                _context.Roles.Add(roleWorkers);
                _context.Roles.Add(roleOperators);
                _context.Roles.Add(roleAdmins);
     
                admin.Roles.Add(new IdentityUserRole<string> { RoleId = "1", UserId = admin.Id });
                _context.Users.AddRange(user1,user2,user3,admin);
               
            }

            await _context.SaveChangesAsync();
        }
    }
}
