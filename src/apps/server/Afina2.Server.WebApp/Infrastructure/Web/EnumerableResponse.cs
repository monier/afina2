using System.Collections.Generic;

namespace Afina2.Server.WebApp.Infrastructure.Web;

public class EnumerableResponse<T>
{
    public IEnumerable<T>? Items { get; set; }
}
