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
        
        string xprot;
        string className; // This is going to be always changing.
        string propertyValue; // This is going to be always changing.
        string propertyName; // This is going to be always changing.
        int iArrayIndex;

        public MiscDicomFields cMiscDicomFields;
        public sKSpace cKSpace;
        

        public DicomContainer(string input)
        {
            xprot = input;

            // Initialize classes
            cKSpace = new sKSpace();
            cMiscDicomFields = new MiscDicomFields();

            parseKSpace();
        }

        public void parseKSpace()
        {
            StringReader strReader = new StringReader(xprot);
            StringBuilder sb = new StringBuilder();
            string line = "testing testing 123";
            string tmp;
            string[] check;

            while (! line.Contains("### ASCCONV BEGIN ###"))
            {
                line = strReader.ReadLine();
            }

            line = strReader.ReadLine();

            while ((!line.Contains("### ASCCONV END ###")) || (strReader.Peek() == -1))
            {
                tmp = line;
                check = tmp.Split('=');

                getClassAndPropertyAndPropertyValues(line);

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
                        setDcmPropertyValue(cKSpace);
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
                    case "sParametricMapping":
                        break;
                    case null:
                        setDcmPropertyValue(cMiscDicomFields);
                        break;
                    default:
                        break;

                }

                line = strReader.ReadLine();

            }
            
            Console.WriteLine("Done Reading!");
        }

        public void getClassAndPropertyAndPropertyValues(string line)
        {

            string[] parts = line.Split('=');

            setClassAndProperty(parts[0]);
            setPropertyValue(parts[1]);
            
        }

        public void setClassAndProperty(string str)
        {
            string[] sProperty;

            if (!str.Contains('.'))
            {
                // If there is no ".", it means that the parameter is not stored under any structure.
                // In this case, className is set to "null" and all values are stored under class "MiscDicomFields"
                className = null;

                propertyName = str;
                SetArrayIndex(propertyName);
                propertyName = RemoveArrayBrackets(propertyName);
                propertyName = Regex.Replace(str, " ", string.Empty);
            }
            else
            {
                sProperty = str.Split('.');
                className = sProperty[0];

                Console.WriteLine(sProperty[1]);

                SetArrayIndex(sProperty);

                // Removes all array brackets from every string in array
                sProperty = RemoveArrayBrackets(sProperty);
                // Replaces any extra "."s with "_" by concatenating the extra split strings
                propertyName = sProperty[1];

                for (int i = 0; i < (sProperty.Length - 2); i++)
                {
                    propertyName += "_";
                    propertyName += sProperty[i + 2];
                }

                // Remove all white space.
                propertyName = Regex.Replace(sProperty[1], " ", string.Empty);
            }

        }

        public void setPropertyValue(string str)
        {
            string val = Regex.Replace(str, " ", string.Empty);

            if (val.Contains("\""))
            {
                // If it contains quotations, remove them. 
                val = Regex.Replace(val, "\"", string.Empty);
                propertyValue = val;
            }
            else if ((val.Contains("x")) && ((val.Length == 3) || (val.Length == 4)))
            {
                // If hexadecimal value, convert to decimal
                propertyValue = hex2dec(val);
            }
            else
            {
                // Otherwise, do nothing.
                propertyValue = val;
            }

        }
        
        public void setDcmPropertyValue(object obj)
        {           
            
            // First check if property is valid
            if ( obj.GetType().GetProperty(propertyName) != null )
            {
                // Get the property value
                PropertyInfo propInfo = obj.GetType().GetProperty(propertyName);                
                
                // Convert propertyValue to the correct type
                if ( propInfo.PropertyType == typeof(string) )
                {
                    propInfo.SetValue(obj, propertyValue, null);
                }
                else if (propInfo.PropertyType.IsArray)
                {
                    Array arr = (Array) propInfo.GetValue(obj, null);
                    arr.SetValue(propertyValue, iArrayIndex);
                    propInfo.SetValue(obj, arr, null);
                }
                else
                {
                    var val = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                    propInfo.SetValue(obj, val, null);
                }

            }
            
        }

        public string RemoveArrayBrackets(string str)
        {
            string[] tmp = propertyName.Split('[');
            return tmp[0];
        }

        public string[] RemoveArrayBrackets(string[] str)
        {
            string[] output = new string [str.Length];
            string[] tmp;

            for (int i=1; i<str.Length; i++)
            {
                tmp = str[i].Split('[');
                output[i] = tmp[0];
            }

            return output;

        }

        public void SetArrayIndex(string str)
        {
            if ( str.Contains('['))
            {
                // Gets the FIRST array index it finds. Currently does NOT cover "asCoilSelectMeas"
                string[] tmp1 = propertyName.Split('[');
                iArrayIndex = tmp1[1][0];

            } else {
                iArrayIndex = 0;
            }
            
        }

        public void SetArrayIndex(string[] str)
        {
            bool isSet = false;
            int cnt = 0;
            iArrayIndex = 0;


            while ((!isSet) && (cnt < str.Length))
            {
               
                if (str[cnt].Contains('['))
                {
                    // Gets the FIRST array index it finds. Currently does NOT cover "asCoilSelectMeas"
                    string[] tmp = str[cnt].Split('[');
                    iArrayIndex = Convert.ToInt32(tmp[1][0]);
                    isSet = true;

                }
                else
                {
                    cnt++;
                }

            }

        }

        public string hex2dec(string str)
        {
            int tmp = Convert.ToInt32(str, 16);

            if ( (tmp%2) == 0 )
                tmp = tmp / 2;

            return Convert.ToString(tmp);
        }
           
    }
}
