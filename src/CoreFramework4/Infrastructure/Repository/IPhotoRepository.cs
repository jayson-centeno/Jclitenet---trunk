using System;
using CoreFramework4.Model;
namespace CoreFramework4.Infrastructure.Repository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void DeleteByUID(Guid uid);
    }
}
