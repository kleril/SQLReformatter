﻿using System;
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
                if (nextLine.Equals("GO"))
                {
                    f.Remove(nextLine);
                }
            }
            return f;
        }
    }
}
