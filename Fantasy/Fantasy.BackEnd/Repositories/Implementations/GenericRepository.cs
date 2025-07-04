﻿using Fantasy.BackEnd.Data;
using Fantasy.BackEnd.Repositories.Interfaces;
using Fantasy.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.BackEnd.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }
    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionResponse();
        }
        catch (Exception ex)
        {
            return ExceptionActionResponse(ex);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "ERROO1"
            };
        }
        _entity.Remove(row);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,

            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "ERROO2",

            };
        }
    }
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
    {
        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "ERROO1",

            };
        }
        return new ActionResponse<T>
        {
            WasSuccess = true,
            Result = row
        };

    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionResponse();
        }
        catch (Exception ex)
        {
            return ExceptionActionResponse(ex);
        }
    }


    private ActionResponse<T> ExceptionActionResponse(Exception ex)
    {
        return new ActionResponse<T>
        {
            WasSuccess = false,
            Message = ex.Message,
        };
    }

    private ActionResponse<T> DbUpdateExceptionResponse()
    {
        return new ActionResponse<T>
        {
            WasSuccess = false,
            Message = "ERROR3"
        };
    }
}
