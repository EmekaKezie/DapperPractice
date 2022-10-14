using DapperPractice.Classes;
using DapperPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepo;
        public MemberController(IMemberRepository companyRepo)
        {
            _memberRepo = companyRepo;
        }


        [HttpGet(Name = "GetMembers")]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await _memberRepo.GetMembers();
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{Id}", Name = "GetMember")]
        public async Task<IActionResult> GetMember(string Id)
        {
            try
            {
                var member = await _memberRepo.GetMember(Id);

                if (member == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(member);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost(Name = "CreateMember")]
        public async Task<IActionResult> CreateMember([FromBody] MemberCreationModel model)
        {
            try
            {
                string member = await _memberRepo.CreateMember(model);

                if (String.IsNullOrEmpty(member))
                {
                    return BadRequest();
                }
                else
                {
                    return StatusCode(201, member);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("Bulk", Name = "CreateMemberBulk")]
        public async Task<IActionResult> CreateMemberBulk([FromBody] List<MemberCreationModel> model)
        {
            try
            {
                bool member = await _memberRepo.CreateMemberBulk(model);

                if (!member)
                {
                    return BadRequest();
                }
                else
                {
                    return StatusCode(201, member);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{Id}", Name = "UpdateMember")]
        public async Task<IActionResult> UpdateMember([FromBody] MemberCreationModel model, string Id)
        {
            try
            {
                string member = await _memberRepo.UpdateMember(Id, model);

                if (String.IsNullOrEmpty(member))
                {
                    return BadRequest();
                }
                else
                {
                    return StatusCode(200, member);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{Id}", Name = "DeleteMember")]
        public async Task<IActionResult> DeleteMember(string Id)
        {
            try
            {
                bool member = await _memberRepo.DeleteMember(Id);

                if (member)
                {
                    return StatusCode(200, member);    
                }
                else
                {
                    return BadRequest("Item not found for deletion");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
