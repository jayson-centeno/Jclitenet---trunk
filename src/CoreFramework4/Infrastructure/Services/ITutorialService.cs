using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreFramework4.Model;

namespace CoreFramework4.Infrastructure.Services
{
    public interface ITutorialService : IService
    {
        IEnumerable<Tutorial> GetAllTutorialWithCategoryWithComments { get; }
    }
}
