using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Model;
using CoreFramework4.Tool;

namespace CoreFramework4.Implementations.Services
{
    public class TutorialService : ITutorialService
    {
        private ITutorialRepository _tutorialRepository;

        public TutorialService(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }

        public IEnumerable<Tutorial> GetAllTutorialWithCategoryWithComments
        {
            get
            {
                var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
                var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>("ALLTUTORIALS");
                if (lst == null || lst.Count() == 0)
                {
                    lst = _tutorialRepository.GetQuery()
                                 .Include("TutorialCategory")
                                 .Include("Comments")
                                 .ToList();

                    applicationCachingServices.AddItem("ALLTUTORIALS", lst, 5);
                }

                return lst;
            }
        }
    }
}