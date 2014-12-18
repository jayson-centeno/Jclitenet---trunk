using CoreFramework4.Infrastructure.Services;
using System.Collections.Generic;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;
using System.Linq;

namespace CoreFramework4.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IEnumerable<Comment> GetAllDisApproved()
        {
            return _commentRepository.GetAll()
                                     .Where(c => !c.IsApproved);
        }

        public Comment GetById(int id)
        {
            return _commentRepository.Get(id);
        }

        public void DeleteById(int id)
        {
            _commentRepository.Delete(
                _commentRepository.Get(id)
            );
        }

        public void SaveChanges()
        {
            _commentRepository.SaveChanges();
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public IEnumerable<Comment> GetCommentsWithParent()
        {
            return _commentRepository.GetQuery()
                                     .Include("Tutorial.TutorialCategory")
                                     .ToList();
        }
    }
}
