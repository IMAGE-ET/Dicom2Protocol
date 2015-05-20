using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sGroupArray
    {
        public double[] asGroup_nSize       { get; set; }
        public double[] asGroup_dDistFact   { get; set; }
        public double[] asGroup_nLow        { get; set; }
        public double[] asGroup_anMember    { get; set; }
        public long lSize                   { get; set; }
        public double sPSat_dThickness      { get; set; }
        public double sPsat_dGap            { get; set; }

        public sGroupArray()
        {
            asGroup_nSize = new double[100];
            asGroup_dDistFact = new double[100];
            asGroup_nLow = new double[100];
            asGroup_anMember = new double[100];
            
        }

    }
}
