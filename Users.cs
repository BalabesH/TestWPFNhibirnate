using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWPF
{
    public class Users
    {
        public virtual int ID { get; set; }
        public virtual string LOGIN { get; set; }
        public virtual string FIRST_NAME { get; set; }
        public virtual string LAST_NAME { get; set; }
        public virtual string MIDDLE_NAME { get; set; }
                   
    }
}
