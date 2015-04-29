using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sAngio
    {
        public int sFlowArray_asElm_nVelocity       {get;set;} 
        public int sFlowArray_asElm_nDir            {get;set;} 
        public long sFlowArray_lSize                {get;set;}
        public int ucPCFlowMode                     {get;set;} 
        public int ucTOFInflow                      {get;set;} 
        public int ucRephasedImage                  {get;set;} 
        public int ucPhaseImage                     {get;set;} 
        public long lDynamicReconMode               {get;set;}
        public long lTemporalInterpolation          {get;set;}
    }
}
