using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLReformatter
{
    class Reformatter
    {
        //Tom's magical nonsense
        public static List<String> reformat(List<String> f)
        {
            foreach (string nextLine in f)
            {
                if (nextLine.Equals("GO") || nextLine.Equals("--declare @testString varchar(100)") || nextLine.Equals("--SELECT '*' as item") || nextLine.Equals("--UNION ALL"))
                {
                    f.Remove(nextLine);
                }
            }
            return f;
        }
    }
}
