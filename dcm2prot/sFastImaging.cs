using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sFastImaging
    {
        public long lEPIFactor              {get;set;} 
        public long lTurboFactor            {get;set;} 
        public long lSliceTurboFactor       {get;set;} 
        public long lSegments               {get;set;} 
        public int ulEnableRFSpoiling       {get;set;} 
        public int ucPhaseEncRE             {get;set;} 
        public int ucSegmentationMode       {get;set;} 
        public long lShots                  {get;set;} 
        public long lEchoTrainDuration      {get;set;} 
    }
}
