using CoreFramework4.Model;
using jclitenet.Models;

namespace jclitenet.Helper
{
    public static class Hydrators
    {
        public static SideMenuModelItem HydrateSideMenuModel(TutorialCategory tutorial)
        {
            return new SideMenuModelItem
            {
                ID = tutorial.ID,
                Title = tutorial.Name
            };
        }
    }
}