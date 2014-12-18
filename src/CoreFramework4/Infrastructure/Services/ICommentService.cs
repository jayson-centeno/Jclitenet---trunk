using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Services
{
    public interface ICommentService : IService
    {
        IEnumerable<Comment> GetAllDisApproved();
        void SaveChanges();
        IEnumerable<Comment> GetAll();
        Comment GetById(int id);
        void DeleteById(int id);
        IEnumerable<Comment> GetCommentsWithParent();
    }
}
