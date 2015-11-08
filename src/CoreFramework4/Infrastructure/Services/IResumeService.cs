
using pocoModel = jclitenet.Domain.Objects.Poco;
using System;
using System.Threading.Tasks;

namespace CoreFramework4.Infrastructure.Services
{
    public interface IResumeService : IService
    {
        Task<pocoModel.ResumeDetails> GetResumeDetails(Guid userId);

        void UpdateIntroduction(Guid userId, string value);
    }
}
