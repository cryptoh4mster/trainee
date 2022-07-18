using System;
using System.Collections.Generic;
using System.Text;
using traineeDAL.EF;
using traineeDAL.Entities;
using traineeDAL.Interfaces;

namespace traineeDAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TraineeDbContext _context) : base(_context)
        {

        }
    }
}
