using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace dcm2prot
{
    class DicomContainer
    {

        string sPrepPulses;
        string sLineTag;
        string sGridTag;
        string sKSpaceInfo;
        string sFastImaging;
        string sPhysioImaging;
        string sSpecPara;
        string sDiffusion;
        string sAngio;
        string sPreScanNormalizeFilter;
        string sDistortionCorrFilter;
        string sPat;

        string xprot;
        string className; // This is going to be always changing.
        string propertyValue; // This is going to be always changing.
        string propertyName; // This is going to be always changing.
        

        public sKSpace cKSpace;

        public DicomContainer(string input)
        {
            xprot = input;

            // Initialize classes
            cKSpace = new sKSpace();

            parseKSpace();
        }

        public void parseKSpace()
        {
            StringReader strReader = new StringReader(xprot);
            StringBuilder sb = new StringBuilder();
            string line = "testing testing 123";

            while (! line.Contains("### ASCCONV BEGIN ###"))
            {
                line = strReader.ReadLine();
            }

            Console.WriteLine(line);

            while ((!line.Contains("### ASCCONV END ###")) || (strReader.Peek() == -1))
            {
                line = strReader.ReadLine();
                // This line contains "." isn't so hot because the right side of the equation can also have a '.'
                if (line.Contains("."))
                {
                    getClassAndPropertyValues(line);

                    switch (className)
                    {
                        case "sProtConsistencyInfo":
                            break;
                        case "sGRADSPEC":
                            break;
                        case "sTXSPEC":
                            break;
                        case "sRXSPEC":
                            break;
                        case "sAdjData":
                            break;
                        case "sSliceArray":
                            break;
                        case "sGroupArray":
                            break;
                        case "sNavigatorArray":
                            break;
                        case "sAutoAlign":
                            break;
                        case "sNavigatorPara":
                            break;
                        case "sBladePara":
                            break;
                        case "sPrepPulses":
                            break;
                        case "sLineTag":
                            break;
                        case "sGridTag":
                            break;
                        case "sKSpace":
                            setClassAndPropertyValues(cKSpace);
                            break;
                        case "sFastImaging":
                            break;
                        case "sPhysioImaging":
                            break;
                        case "sDiffusion":
                            break;
                        case "sAngio":
                            break;
                        case "sPreScanNormalizeFilter":
                            break;
                        case "sDistortionCorrFilter":
                            break;
                        case "sPat":
                            break;
                        case "sMDS":
                            break;
                        case "asCoilSelectMeas":
                            break;
                        case "sWiPMemBlock":
                            break;
                        case "sIR":
                            break;
                        case "sAsl":
                            break;

                    }
                }
            }
            
            sKSpaceInfo = sb.ToString();
            Console.WriteLine("Done Reading!");

        }

        public void getClassAndPropertyValues(string line)
        {

            string[] sProperty;

            string[] parts = line.Split('=');

            if (!parts[0].Contains('.'))
            {
                className = null;
                propertyName = null;
                propertyValue = null;
                return;
            }
            else
            {
                sProperty = parts[0].Split('.');
            }

            className = sProperty[0];
            propertyName = Regex.Replace(sProperty[1], " ", string.Empty);
            string val = Regex.Replace(parts[1], " ", string.Empty);

            if ((val.Contains("x")) && ((val.Length == 3) || (val.Length == 4)) )
            {
                Console.WriteLine("Contains x: " + val);
                int tmp = Convert.ToInt32(val, 16);
                tmp = tmp / 2;
                propertyValue = Convert.ToString(tmp);
            }
            else
            {
                propertyValue = val;
            }

            Console.WriteLine("TEST: " + propertyValue);

        }

        public void setClassAndPropertyValues(object obj)
        {           
            
            Console.Write(className + " AND " + propertyName + " AND ");

            // First check if property is valid
            if ( obj.GetType().GetProperty(propertyName) != null )
            {
                // Get the property value
                PropertyInfo propInfo = obj.GetType().GetProperty(propertyName);                
                // Convert propertyValue to the correct type
                var val = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                propInfo.SetValue(obj, val, null);

                Console.WriteLine(propInfo.GetValue(cKSpace,null));
                
            }
            
            
        }      
   
    }
}
