using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    public class sKSpace
    {
        
        // Couldn't figure out how to get enums working on the SetValue, so I'm just going to keep them as integers.

        //public enum eReordering {Linear=1,Centric};
        //public enum eTrajectory {Cartesian=1,Radial};
        //public enum ePAT {None=1,GRAPPA,mSENSE};
        //public enum eMatrixCoilMode {Auto=1,CP,Dual,Triple};
        //public enum eMultiSliceMode {Sequential=1, Interleaved=2,SingleShot=4};
        //public enum eDim {x_1D = 1,x_2D=2,x_3D=4};
        //public enum ePartialFourier {Half = 1, x_5_8=2,x_6_8=4,x_7_8=8,Off=16,Auto = 32}
        //public enum eAverageMode {ShortTerm=1,LongTerm};
        //public enum eViewShare { Off = 1, SHPHS = 2, KtBLAST = 4, TWIST = 8 };
        //public enum ePOCS { Off = 1, Read_Slice = 2, Read_Phase = 4 }; // Projection onto convex sets recon mode
        //public enum eOnOff { Off = 1, On };

        public double ddPhaseResolution;
        public double ddSliceResolution;

        public double dPhaseResolution { get { return ddPhaseResolution; } set { ddPhaseResolution = value * 100; } }
        public double dSliceResolution { get { return ddSliceResolution; } set { ddSliceResolution = value * 100; } }
        public double dAngioDynCentralRegionA { get; set; }
        public double dAngioDynSamplingDensityB { get; set; }
        public long lBaseResolution { get; set; }
        public long lPhaseEncodingLines { get; set; }
        public long lPartitions { get; set; }
        public long lImagesPerSlab { get; set; }
        public long lRadialViews { get; set; }
        public long lRadialInterleavesPerImage { get; set; }
        public long lLinesPerShot { get; set; }
        public int unReordering { get; set; }
        public double dSeqPhasePartialFourierForSNR { get; set; }
        public int ucPhasePartialFourier { get; set; }
        public int ucSlicePartialFourier { get; set; }
        public int uc2DInterpolation { get; set; }
        public int ucAveragingMode { get; set; }
        public int ucMultiSliceMode { get; set; }
        public int ucDimension { get; set; }
        public int ucAsymmetricEchoAllowed { get; set; }
        public int ucTrajectory { get; set; }
        public int ucViewSharing { get; set; }
        public int ucAsymmetricEchoMode { get; set; }
        public int ucPOCS { get; set; }
        
    }
}
