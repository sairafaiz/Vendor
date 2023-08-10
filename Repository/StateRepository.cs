﻿
using System.Linq.Expressions;
using VendorMVC.Data;
using VendorMVC.Entities;
using VendorMVC.Repository.IRepository;

namespace VendorMVC.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {

        private ApplicationDbContext _db;

        public StateRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      
        public void Update(State obj)
        {
            var objfromdb = _db.Country.FirstOrDefault(u => u.Id == obj.Id);

            if (objfromdb != null)
            {
                objfromdb.Name = obj.Name;
            }
        }

    }
}
