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
                string key = "ALLTUTORIALSWITHCOMMENTS";
                var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
                var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>(key);
                if (lst == null || lst.Count() == 0)
                {
                    lst = _tutorialRepository.GetQuery()
                                             .Include("TutorialCategory")
                                             .Include("Comments")
                                             .ToList();

                    applicationCachingServices.AddItem(key, lst, 5);
                }

                return lst;
            }
        }

        public IEnumerable<Tutorial> GetAllTutorial
        {
            get
            {
                string key = "ALLTUTORIALS";
                var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
                var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>(key);
                if (lst == null || lst.Count() == 0)
                {
                    lst = _tutorialRepository.GetQuery()
                                             .ToList();

                    applicationCachingServices.AddItem(key, lst, 5);
                }

                return lst;
            }
        }

        public IEnumerable<Tutorial> GetAllTutorialWithCategory
        {
            get
            {
                string key = "ALLTUTORIALSWITHCATEGORY";
                var applicationCachingServices = CachingFactory.ApplicationCachingEngineInstance;
                var lst = applicationCachingServices.GetItem<IEnumerable<Tutorial>>(key);
                if (lst == null || lst.Count() == 0)
                {
                    lst = _tutorialRepository.GetQuery()
                                             .Include("TutorialCategory")
                                             .ToList();

                    applicationCachingServices.AddItem(key, lst, 5);
                }

                return lst;
            }
        }
    }
}