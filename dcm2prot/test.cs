using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class test
    {
        public List<int> testList { get; set; }
        public int[] testArray { get; set; }
        public int testInt {get;set;}


        public test()
        {
            testArray = new int [100];
            testList = new List<int>();
            testInt = 2;

        }

    }
}
