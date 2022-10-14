using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperPractice.Classes
{
    public class SQL
    {
        public const string GetMembers = "SELECT member_id MemberId, firstname, lastname, gender, entry_date EntryDate FROM members";
        public const string GetMember = "SELECT member_id MemberId, firstname, lastname, gender, entry_date EntryDate FROM members where member_id = @MemberId";
        public const string CreateMember = "insert into members (member_id, firstname, lastname, gender, entry_date) values (@Id, @Firstname, @Lastname, @Gender, @EntryDate)";
        public const string UpdateMember = "update members set firstname=@Firstname, lastname=@Lastname, gender=@Gender where member_id=@Id";
        public const string DeleteMember = "delete from members where member_id = @Id";
    }
}
