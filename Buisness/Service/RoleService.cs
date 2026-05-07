using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class RoleService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> Add(Role role)
        {
           
            if (await _context.Role.AnyAsync(x => x.Name == role.Name))
                return new Result(false, "Role already exists!");
            _context.Role.Add(role);
            return await Result.DBcommit(_context, "Save Successfully", null, role);
        }
        public async Task<Result> Update(Role role)
        {
            if(!await _context.Role.AnyAsync(x=>x.RoleID==role.RoleID))
                return new Result(false,("Role Not exit")); 
            _context.Role.Update(role);
            return await Result.DBcommit(_context, "Update successfully", null, role);
        }
        public async Task<Result> List()
        {
            try
            {
                var roles = await _context.Role.ToListAsync();
                return new Result(true, "Success", roles);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}