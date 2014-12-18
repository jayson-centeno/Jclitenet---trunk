using System.Collections.Generic;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Repository
{
    public interface ITutorialRepository : IRepository<Tutorial>
    {
        IEnumerable<TutorialCategory> GetAllTutorialCategory { get; }
        TutorialCategory GetTutorialCategoryByID(int categoryId);
        IEnumerable<Tutorial> GetAllTutorialWithCategoryWithComments { get; }
        IEnumerable<TutorialType> GetAllTutorialType { get; }
        IEnumerable<Tutorial> GetAllTutorialByCategoryID(int categoryId);
        IEnumerable<Tutorial> GetAllTutorialWithCommentsByCategoryID(int categoryId);
        Tutorial GetByIDWithTutorialCategory(int id);
    }
}