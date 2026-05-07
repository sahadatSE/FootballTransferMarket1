using System;
using System.Collections.Generic;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class AgentInfoService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> Add(AgentInfo agent)
        {
            if (!await _context.Player.AnyAsync(x => x.PlayerId == agent.PlayerId))
                return new Result(false, "Player does not exist");

            if (await _context.AgentInfo.AnyAsync(x => x.AgentEmail == agent.AgentEmail))
                return new Result(false, "Agent with this email already exists");

            if (await _context.AgentInfo.AnyAsync(x => x.LicenseNumber == agent.LicenseNumber))
                return new Result(false, "Agent with this license number already exists");

            var player = await _context.Player.FindAsync(agent.PlayerId);
            if (player != null)
            {
                agent.PlayerName = player.PlayerName;
            }

            await _context.AgentInfo.AddAsync(agent);
            return await Result.DBcommit(_context, "Agent added successfully", "Failed to add agent", agent);
        }

        public async Task<Result> Update(AgentInfo agent)
        {
            if (!await _context.AgentInfo.AnyAsync(x => x.AgentId == agent.AgentId))
                return new Result(false, "Agent does not exist");

            _context.AgentInfo.Update(agent);
            return await Result.DBcommit(_context, "Agent updated successfully", null, agent);
        }

        public async Task<Result> Delete(AgentInfo agent)
        {
            if (!await _context.AgentInfo.AnyAsync(x => x.AgentId == agent.AgentId))
                return new Result(false, "Agent does not exist");

            _context.AgentInfo.Remove(agent);
            return await Result.DBcommit(_context, "Agent deleted successfully", null, agent);
        }

        public async Task<Result> List()
        {
            try
            {
                var agents = await _context.AgentInfo.ToListAsync();
                return new Result(true, "Success", agents);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> Single   (string agentId)
        {
            try
            {
                var agent = await _context.AgentInfo.FindAsync(agentId);

                if (agent == null)
                    return new Result(false, "Agent not found");

                return new Result(true, "Success", agent);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetPlayer(string playerId)
        {
            try
            {
                var agents = await _context.AgentInfo
                    .Where(x => x.PlayerId == playerId)
                    .ToListAsync();

                return new Result(true, "Success", agents);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetEmail(string email)
        {
            try
            {
                var agent = await _context.AgentInfo
                    .FirstOrDefaultAsync(x => x.AgentEmail == email);

                if (agent == null)
                    return new Result(false, "Agent not found");

                return new Result(true, "Success", agent);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetByLicense(string licenseNumber)
        {
            try
            {
                var agent = await _context.AgentInfo
                    .FirstOrDefaultAsync(x => x.LicenseNumber == licenseNumber);

                if (agent == null)
                    return new Result(false, "Agent not found");

                return new Result(true, "Success", agent);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}