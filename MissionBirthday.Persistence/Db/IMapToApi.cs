using System.Collections.Generic;
using System.Linq;

namespace MissionBirthday.Persistence.Db
{
    public interface IMapToApi<TApi>
    {
        void CopyFromApi(TApi api);

        TApi ToApi();
    }
}