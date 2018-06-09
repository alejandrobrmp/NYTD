using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.Lib.Services.Interfaces
{
    public interface IYTApiService
    {
        IYouTubeApiService Service { get; }
    }
}
