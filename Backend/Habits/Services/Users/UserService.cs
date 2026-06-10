using FluentValidation.Results;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;
using Habits.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Habits.Services.Users
{
    public class UserService
    {
        private readonly PatchUserRequestValidation _validation;
        private readonly HttpContext _context;
        private readonly HabitsContext _db;
        public UserService(PatchUserRequestValidation validation, HttpContext context, HabitsContext db)
        {
            _validation = validation;
            _context = context;
            _db = db;
        }
        public async Task<Result<PatchUserRequest>> ModifyUser(JsonPatchDocument json)
        {
            var username = _context.User.Identity?.Name;

            if (username == null) return Result<PatchUserRequest>.Failure(Status.NotOwner, "No tienes acceso a este recurso");

            var userDb = await _db.Users.SingleOrDefaultAsync(user => user.UserName == username);

            if (userDb == null) return Result<PatchUserRequest>.Failure(Status.NotOwner, "No tienes acceso a este recurso");

            var dtoPatchUser = userDb.ToPatchRequest();
            json.ApplyTo(dtoPatchUser);
            var result = await _validation.ValidateAsync(dtoPatchUser);

            if (!result.IsValid) return Result<PatchUserRequest>.Failure(Status.InvalidData, result.Errors.ToArray());

            return Result<PatchUserRequest>.Success(dtoPatchUser);
        }
    }
}
