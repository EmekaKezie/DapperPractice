using DapperPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperPractice.Classes
{
    public interface IMemberRepository
    {
        public Task<IEnumerable<MemberModel>> GetMembers();
        public Task<MemberModel> GetMember(string MemberId);
        public Task<string> CreateMember(MemberCreationModel model);
        public Task<string> UpdateMember(string MemberId, MemberCreationModel model);
        public Task<bool> DeleteMember(string MemberId);
        public Task<bool> CreateMemberBulk(List<MemberCreationModel> model);
    }
}
