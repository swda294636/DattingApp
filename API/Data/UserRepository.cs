
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task<AppUser> GetAppUserByIdAsync(int id)
        {
            return  await _context.Users.FindAsync(id);        
        }

        public async Task<AppUser> GetAppUserByNameAsync(string name)
        {
            return await _context.Users
                            .Include(p => p.Photos)
                            .SingleOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<IEnumerable<AppUser>> GetAppUsersAsync()
        {
            return await _context.Users
                            .Include(p => p.Photos)
                            .ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string name)
        {
            return await _context.Users
                            .Where(x => x.UserName == name)
                            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}