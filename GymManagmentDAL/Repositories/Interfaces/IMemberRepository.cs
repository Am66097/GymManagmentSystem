using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GymManagmentDAL.Entities;



namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {

        IEnumerable<Member> GetAll();
        Member? GetById(int Id);
        int Add(Member member);
        int Update(Member member);
        int Delete(int Id);
    }
}
