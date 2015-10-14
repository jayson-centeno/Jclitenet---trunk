using jclitenet.Domain.Objects.Poco;

namespace CoreFramework4
{
    using EFModel = EF4_1.Model;

    public class ObjectMappings
    {
        public static void MapDomainObjects() 
        {
            AutoMapper.Mapper
                .CreateMap<EFModel.ResumeDetails, ResumeDetails>()
                .BeforeMap((s,d) =>
                {   
                    d.ResumeID = s.Resume.ID;
                });
        }

    }
}
