
using API.DTOs;
using API.Entities;

namespace API.Interface
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetAppUsersAsync();
        Task<AppUser> GetAppUserByIdAsync(int id);
        Task<AppUser> GetAppUserByNameAsync(string name);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string name);
        
    }
}