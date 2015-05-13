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
        public double[] asSlice_sNormal_dTra    { get; set; }
        public double[] asSlice_dThickness      { get; set; } // Thickness / # of partitions = Slice Thickness (3D only)
        public double[] asSlice_dPhaseFOV       { get; set; }
        public double[] asSlice_dReadoutFOV     { get; set; } // PhaseFoV/ReadoutFoV = FoV Phase %
        public long lSize                       { get; set; }
        public long lConc                       { get; set; }
        public int ucMode                       { get; set; }
        public int sTSat_dThickness             { get; set; }

    }
}
