using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;
using System.Collections.Generic;
using System.Linq;

namespace CoreFramework4.Implementations.Repository
{
    public class TutorialRepository : RepositoryBase<Tutorial>, ITutorialRepository
    {
        public IEnumerable<TutorialCategory> GetAllTutorialCategory
        {
            get
            {
                return GetQuery<TutorialCategory>().Include("TutorialType")
                                                   .ToList();
            }
        }

        public TutorialCategory GetTutorialCategoryByID (int categoryId)
        {
            return GetQuery<TutorialCategory>().FirstOrDefault(t => t.ID == categoryId);                
        }

        public IEnumerable<TutorialType> GetAllTutorialType
        {
            get
            {
                return GetQuery<TutorialType>().ToList();
            }
        }

        public IEnumerable<Tutorial> GetAllTutorialByCategoryID(int categoryId)
        {
            return GetQuery().Include("TutorialCategory")
                      .Where(t => t.TutorialCategory.ID == categoryId)
                      .Select(t => t)
                      .ToList();
        }

        public IEnumerable<Tutorial> GetAllTutorialWithCommentsByCategoryID(int categoryId)
        {
            return GetQuery()
                      .Include("TutorialCategory")
                      .Include("Comments")
                      .Where(t => t.TutorialCategory.ID == categoryId)
                      .Select(t => t)
                      .ToList();
        }

        public Tutorial GetByIDWithTutorialCategory(int id)
        {
            return GetQuery()
                    .Include("TutorialCategory")
                    .FirstOrDefault(t => t.ID == id);
        }

        public IEnumerable<Tutorial> GetAllTutorialWithCategoryWithComments
        {
            get 
            {
                return GetQuery().Include("TutorialCategory")
                                 .Include("Comments");
            }
        }
    }
}