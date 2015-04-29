using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dcm2prot
{
    class sPrepPulses
    {
        public int ucFatSat                     {get; set;}
        public int ucWaterSat                   {get; set;} 
        public int ucInversion                  {get; set;}
        public int ucSatRecovery                {get; set;}
        public int ucT2Prep                     {get; set;} 
        public int ucTIScout                    {get; set;}
        public int ucFatSatMode                 {get; set;} 
        public double dDarkBloodThickness       {get; set;} 
        public double dDarkBloodFlipAngle       {get; set;}
        public double dT2PrepDuration           {get; set;}
        public double dIRPulseThicknessFactor   {get; set;} 

    }
}
