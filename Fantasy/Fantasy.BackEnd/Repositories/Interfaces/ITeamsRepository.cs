﻿using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.BackEnd.Repositories.Interfaces
{
    public interface ITeamsRepository
    {
        Task<IEnumerable<Team>> GetComboAsync(int countryId);
        Task<ActionResponse<Team>> AddAsync(TeamDTO teamDTO);
        Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO);
        Task<ActionResponse<Team>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Team>>> GetAsync();

    }
}
