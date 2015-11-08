using CoreFramework4.EF4_1.Model;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using System;
using System.Threading.Tasks;
using pocoModel = jclitenet.Domain.Objects.Poco;

namespace CoreFramework4.Implementations.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeService(IResumeRepository resumeDetailsRepository)
        {
            _resumeRepository = resumeDetailsRepository;
        }

        public async Task<pocoModel.ResumeDetails> GetResumeDetails(Guid userId)
        {
            var asyncOutput = await _resumeRepository.GetResumeDetailsByUserId(userId);
            return AutoMapper.Mapper.Map<ResumeDetails, pocoModel.ResumeDetails>(asyncOutput);
        }

        public void UpdateIntroduction(Guid userId, string value)
        {
            _resumeRepository.UpdateIntroduction(userId, value);
        }
    }
}
