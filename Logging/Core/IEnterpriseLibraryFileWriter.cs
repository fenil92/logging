using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public interface IEnterpriseLibraryFileWriter
    {
        void LogToFile(LogEntity logEntity);
    }
}
