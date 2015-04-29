using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sPhysioImaging
    {

        public long lSignal1                            {get;set;}
        public long lMethod1                            {get;set;}
        public long lSignal2                            {get;set;}
        public long lMethod2                            {get;set;}
        public long lPhases                             {get;set;}
        public long sPhysioECG_lScanWindow              {get;set;}
        public long sPhysioECG_lTriggerPulses           {get;set;}
        public long sPhysioECG_lTriggerWindow           {get;set;}
        public long sPhysioECG_lArrhythmiaDetection     {get;set;} 
        public long sPhysioECG_lCardiacGateOnThreshold  {get;set;} 
        public long sPhysioECG_lCardiacGateOffThreshold {get;set;}
        public long sPhysioECG_lTriggerIntervals        {get;set;} 
        public long sPhysioPulse_lTriggerPulses         {get;set;} 
        public long sPhysioPulse_lTriggerWindow         {get;set;} 
        public long sPhysioPulse_lArrhythmiaDetection   {get;set;} 
        public long sPhysioPulse_lCardiacGateOnThreshold{get;set;}
        public long sPhysioPulse_lCardiacGateOffThreshold {get;set;} 
        public long sPhysioPulse_lTriggerIntervals      {get;set;} 
        public long sPhysioExt_lTriggerPulses           {get;set;} 
        public long sPhysioExt_lTriggerWindow           {get;set;} 
        public long sPhysioExt_lArrhythmiaDetection     {get;set;} 
        public long sPhysioExt_lCardiacGateOnThreshold  {get;set;}
        public long sPhysioExt_lCardiacGateOffThreshold {get;set;}
        public long sPhysioExt_lTriggerIntervals        {get;set;} 
        public long sPhysioResp_lRespGateThreshold      {get;set;} 
        public long sPhysioResp_lRespGatePhase          {get;set;} 
        public double sPhysioResp_dGatingRatio          {get;set;} 
        public int sPhysioNative_ucMode                 {get;set;} 
        public int sPhysioNative_ucFlowSenMode          {get;set;} 

    }
}
