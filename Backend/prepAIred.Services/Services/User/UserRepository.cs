﻿using prepAIred.Data;

namespace prepAIred.Services
{
    public class UserRepository(IUserService userService) : IUserRepository
    {
        private readonly IUserService _userService = userService;

        public async Task<CurrentUserDTO> GetCurrentUserAsync()
        {
            CurrentUserDTO user = await _userService.GetCurrentUserAsync();
            return user;
        }

        public async Task DeleteCurrentUserAsync()
        {
            int currentUserID = await _userService.GetCurrentUserID();
            await _userService.DeleteUserAsync(currentUserID);
        }
    }
}
