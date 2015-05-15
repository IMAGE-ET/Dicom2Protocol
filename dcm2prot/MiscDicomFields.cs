using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class MiscDicomFields
    {
        public string ulVersion             { get; set; }
        public string tSequenceFileName     { get; set; }
        public string tProtocolName         { get; set; }
        public string tReferenceImage0      { get; set; }
        public string tReferenceImage1      { get; set; }
        public string tReferenceImage2      { get; set; }
        public double lScanRegionPosTra     { get; set; }
        public int ucScanRegionPosValid     { get; set; }
        public int ucTablePositioningMode   { get; set; }
        public int ucEnableNoiseAdjust      { get; set; }             
        public long[] alTR                  { get; set; }
        public long[] alTI                  { get; set; }
        public long lContrasts              { get; set; }
        public long[] alTE                  { get; set; }
        public double acFlowComp            { get; set; }
        public long lCombinedEchoes         { get; set; }
        public int ucDisableChangeStoreImages { get; set; }
        public int ucAAMode                 { get; set; }
        public int ucAARegionMode           { get; set; }
        public int ucAARefMode              { get; set; }
        public int ucReconstructionMode     { get; set; }
        public int ucOneSeriesForAllMeas    { get; set; }
        public int ucPHAPSMode              { get; set; }
        public int ucDixon                  { get; set; }
        public int ucDixonSaveOriginal      { get; set; }
        public long lAverages               { get; set; }
        public double dAveragesDouble       { get; set; }
        public double adFlipAngleDegree     { get; set; }
        public long lScanTimeSec            { get; set; }
        public long lTotalScanTimeSec       { get; set; }
        public double dRefSNR               { get; set; }
        public double dRefSNR_VOI           { get; set; }
        public string tdefaultEVAProt       { get; set; }
        public int ucCineMode               { get; set; }
        public int ucSequenceType           { get; set; }
        public int ucCoilCombineMode        { get; set; }
        public int ucFlipAngleMode          { get; set; }
        public long lTOM                    { get; set; }
        public long lProtID                 { get; set; }
        public int ucReadOutMode            { get; set; }
        public int ucBold3dPace             { get; set; }
        public int ucForcePositioningOnNDIS { get; set; }
        public int ucInternalTablePosValid  { get; set; }
        public long lBreathholds            { get; set; }
        public int ucAutoAlignInit          { get; set; }

        // Constructor
        public MiscDicomFields()
        {
            // Initialize arrays
            alTR = new long [100];
            alTE = new long [100];
            alTI = new long [100];

        }


    }
}
