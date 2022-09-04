using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    public class DBConfig 
    {
        private readonly IConfiguration Configuration;

        public DBConfig(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    }
}
