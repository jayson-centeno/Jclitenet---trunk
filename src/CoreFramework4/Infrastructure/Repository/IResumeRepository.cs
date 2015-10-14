using CoreFramework4.EF4_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface IResumeRepository : IRepository<Resume>
    {
        Task<ResumeDetails> GetResumeDetailsByUserId(Guid userId);

        void UpdateIntroduction(Guid userId, string value);
    }
}
