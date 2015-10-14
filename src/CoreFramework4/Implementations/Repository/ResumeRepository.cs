using CoreFramework4.EF4_1.Model;
using CoreFramework4.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework4.Implementations.Repository
{
    public class ResumeRepository : RepositoryBase<Resume>, IResumeRepository
    {
        public async Task<ResumeDetails> GetResumeDetailsByUserId(Guid userId)
        {
            return await Task.Run(() => {

                    var details = GetQuery().Include("User")
                                            .Include("Details");

                    if(details.FirstOrDefault(r => r.UserID == userId) == null) return null;
                    
                    return details.FirstOrDefault(r => r.UserID == userId).Details;

            });
        }

        public void UpdateIntroduction(Guid userId, string value)
        {
            var details = GetQuery().Include("User")
                                            .Include("Details");

            if (details.FirstOrDefault(r => r.UserID == userId) == null) return;

            var resumeDetails = details.FirstOrDefault(r => r.UserID == userId).Details;
            resumeDetails.Introduction = value;

            SaveChanges();
        }

    }
}
