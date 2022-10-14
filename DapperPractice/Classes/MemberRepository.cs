using Dapper;
using DapperPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperPractice.Classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DapperContext _context;
        public MemberRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MemberModel>> GetMembers()
        {
            using (var connection = _context.CreateConnection())
            {
                var members = await connection.QueryAsync<MemberModel>(SQL.GetMembers);
                return members.ToList();
            }
        }


        public async Task<MemberModel> GetMember(string MemberId)
        {
            using (var connection = _context.CreateConnection())
            {
                var member = await connection.QuerySingleOrDefaultAsync<MemberModel>(SQL.GetMember, new { MemberId });
                return member;
            }
        }

        public async Task<string> CreateMember(MemberCreationModel model)
        {
            string data = string.Empty;
            int ret;

            string Id = Guid.NewGuid().ToString();

            var param = new DynamicParameters();
            param.Add("Id", Id);
            param.Add("Firstname", model.Firstname);
            param.Add("Lastname", model.Lastname);
            param.Add("Gender", model.Gender);
            param.Add("EntryDate", DateTime.Now);

            using (var connection = _context.CreateConnection())
            {
                ret = await connection.ExecuteAsync(SQL.CreateMember, param);
                if (ret > 0)
                {
                    data = Id;
                }
            }

            return data;
        }

        public async Task<string> UpdateMember(string MemberId, MemberCreationModel model)
        {
            string data = string.Empty;
            int ret;

            var param = new DynamicParameters();
            param.Add("Firstname", model.Firstname);
            param.Add("Lastname", model.Lastname);
            param.Add("Gender", model.Gender);
            param.Add("Id", MemberId);

            using (var connection = _context.CreateConnection())
            {
                ret = await connection.ExecuteAsync(SQL.UpdateMember, param);
                if (ret > 0)
                {
                    data = MemberId;
                }
            }

            return data;
        }

        public async Task<bool> DeleteMember(string MemberId)
        {
            bool data = false;
            int ret;

            var param = new DynamicParameters();
            param.Add("Id", MemberId);

            using (var connection = _context.CreateConnection())
            {
                ret = await connection.ExecuteAsync(SQL.DeleteMember, param);
                if (ret > 0)
                {
                    data = true;
                }
            }

            return data;
        }

        public async Task<bool> CreateMemberBulk(List<MemberCreationModel> model)
        {
            bool data = false;
            int ret;

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var i in model)
                    {
                        string Id = Guid.NewGuid().ToString();

                        var param = new DynamicParameters();
                        param.Add("Id", Id);
                        param.Add("Firstname", i.Firstname);
                        param.Add("Lastname", i.Lastname);
                        param.Add("Gender", i.Gender);
                        param.Add("EntryDate", DateTime.Now);

                        ret = await connection.ExecuteAsync(SQL.CreateMember, param, transaction: transaction);
                        if (ret > 0)
                        {
                            data = true;
                        }
                    }
                    if (data) transaction.Commit();
                }
                connection.Close();
            }

            return data;
        }
    }
}
