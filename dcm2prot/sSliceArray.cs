using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sSliceArray
    {
        public double[] asSlice_sPosition_dSag  { get; set; }
        public double[] asSlice_sPosition_dCor  { get; set; }
        public double[] asSlice_sPosition_dTra  { get; set; }
        public double[] asSlice_sNormal_dSag    { get; set; }
        public double[] asSlice_sNormal_dCor    { get; set; }
        public double[] asSlice_sNormal_dTra    { get; set; }
        public double[] asSlice_dThickness      { get; set; } // Thickness / # of partitions = Slice Thickness (3D only)
        public double[] asSlice_dPhaseFOV       { get; set; }
        public double[] asSlice_dReadoutFOV     { get; set; } // PhaseFoV/ReadoutFoV = FoV Phase %
        public double[] anAsc                   { get; set; } // Ascending...?
        public long lSize                       { get; set; }
        public long lConc                       { get; set; }
        public int ucMode                       { get; set; }
        public int sTSat_dThickness             { get; set; }
        public int lSag                         { get; set; }
        public int lCor                         { get; set; }
        public int lTra                         { get; set; }

        // Constructor
        public sSliceArray()
        {
            // Initialize some variables
            
            // Initialize arrays
            asSlice_sPosition_dSag = new double[100];
            asSlice_sPosition_dCor = new double[100];
            asSlice_sPosition_dTra = new double[100];
            asSlice_sNormal_dSag = new double[100];
            asSlice_sNormal_dCor = new double[100];
            asSlice_sNormal_dTra = new double[100];
            asSlice_dThickness = new double[100];
            asSlice_dPhaseFOV = new double[100];
            asSlice_dReadoutFOV = new double[100];
            anAsc = new double[100];

            // Is initialization really necessary...?
            for (int i=0; i<100; i++)
            {
                asSlice_sPosition_dSag[i] = 0;
                asSlice_sPosition_dCor[i] = 0;

            }

        }

    }

}
