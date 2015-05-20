using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;



namespace dcm2prot
{
    public partial class Form1 : Form
    {

        DicomContainer cDcm;

        public Form1()
        {
            InitializeComponent();

            TestingTesting();

//            this.tabGroup.Alignment = TabAlignment.Bottom;
                        
            InitProtocolEntries();

        }


        void InitProtocolEntries()
        {
            Font textFont = new Font("Arial", 12.0f);
            string txtBoxName;

//            Console.WriteLine(textBox22.Controls.Count);

            tabPage_Resolution.BackColor = Color.LightGray;

            // Set all textboxes
            List<Control> TxtBoxes = new List<Control>();
            // Find all textboxes that have the name "textBox"
            FindControls(this, TxtBoxes, "textBox");

            foreach (TextBox tbox in TxtBoxes)
            {
                // Separate "value" textboxes from "labels" by accessible name
                tbox.Font = textFont;
                txtBoxName = tbox.AccessibleName;

                if (!txtBoxName.Contains("Value"))
                {

                    tbox.BorderStyle = BorderStyle.None;
                    tbox.BackColor = Color.LightGray;

                    if (txtBoxName.StartsWith("txtLab"))
                        tbox.TextAlign = HorizontalAlignment.Right;

                }
            }

            List<Control> Panels = new List<Control>();
            FindControls(this, Panels, "Panel");

            foreach (Panel pan in Panels)
            {
                pan.BackColor = Color.LightGray;
            }

            List<Control> ComboBoxes = new List<Control>();
            FindControls(this, ComboBoxes, "comboBox");

            foreach (ComboBox cbox in ComboBoxes)
            {
                cbox.Font = textFont;
            }

            List<Control> TabPgs = new List<Control>();
            FindControls(this, TabPgs, "tabPage");
            foreach (Panel tpg in TabPgs)
            {
                tpg.BackColor = Color.LightGray;
            }

            List<Control> TabCtrls = new List<Control>();
            FindControls(this, TabCtrls, "tabControl");
            Font tabFont = new Font("Franklin Gothic", 10.0f, FontStyle.Bold);
            foreach (TabControl tctrl in TabCtrls)
            {
                tctrl.SizeMode = TabSizeMode.Fixed;
                // Can either have appearance or alignment.
//                tctrl.Appearance = TabAppearance.FlatButtons;
                tctrl.Font = tabFont;                
            }
            this.tabControl.Alignment = TabAlignment.Bottom;
                      

        }
            
        public void FindControls(Control owner, List<Control> list, string name)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.Name.Contains(name))
                {
                    list.Add(c);
                }
                if (c.HasChildren) FindControls(c, list, name);
            }
        }
                
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (files.Length > 1)
                MessageBox.Show("Please only load one file at a time");

            StringBuilder sb = new StringBuilder();

            byte[] fileBytes = File.ReadAllBytes(files[0]);

            int dec;
            char[] charValue = new char[1];

            for (int i = 0; i < fileBytes.Length; i++)
            {

                dec = Convert.ToInt32(fileBytes[i]);

                if ((dec == 10) || (dec != 10 && dec != 0))
                {
                    charValue[0] = Convert.ToChar(dec);
                    sb.Append(charValue[0].ToString());
                }
                
            }

            // Upon construction of DicomContainer, necessary fields will be sorted.
            cDcm = new DicomContainer(sb.ToString());



            FillRoutineTab();
            FillResolutionTab();
        }

        void FillRoutineTab()
        {
            if (cDcm.cKSpace.ucDimension == 2)
            {
                // Dimension = 3D;
                this.textBox_rout19.Text = cDcm.cKSpace.lImagesPerSlab.ToString();

            }
            else
            {
                // Dimension = 2D; Turn off all 3D textboxes
                this.textBox_routL11.Text = "Slices";
                this.textBox_routL18.Visible = false;
                this.textBox_routL19.Visible = false;
                this.textBox_routU18.Visible = false;
                this.textBox_rout18.Visible = false;
                this.textBox_rout19.Visible = false;

                this.textBox_rout11.Text = cDcm.cGroupArray.asGroup_nSize[0].ToString();
                this.textBox_rout12.Text = (cDcm.cGroupArray.asGroup_dDistFact[0] * 100).ToString();


            }

            if (cDcm.cSliceArray.asSlice_sNormal_dSag[0] == 1)
            {
                this.comboBox_rout14.SelectedIndex = 0;

            }
            else if (cDcm.cSliceArray.asSlice_sNormal_dCor[0] == 1)
            {
                this.comboBox_rout14.SelectedIndex = 1;
            }
            else if (cDcm.cSliceArray.asSlice_sNormal_dTra[0] == 1) 
            {

                this.comboBox_rout14.SelectedIndex = 2;

            } else {

                Console.WriteLine("ProtocolEditor::FillRoutineTab -> Error. Cannot find orientation of slice");

            }
            

            this.textBox_rout20.Text = cDcm.cSliceArray.asSlice_dReadoutFOV[0].ToString();
            this.textBox_rout21.Text = (cDcm.cSliceArray.asSlice_dPhaseFOV[0]*100/cDcm.cSliceArray.asSlice_dReadoutFOV[0]).ToString();
            this.textBox_rout22.Text = cDcm.cSliceArray.asSlice_dThickness[0].ToString();
            this.textBox_rout23.Text = (cDcm.cMiscDicomFields.alTR[0]/1000).ToString();
            this.textBox_rout24.Text = ((Convert.ToDouble(cDcm.cMiscDicomFields.alTE[0]))/1000).ToString();
            this.textBox_rout25.Text = cDcm.cMiscDicomFields.lAverages.ToString();
            // Not sure about concatenations, Filter, or Coil elements

        }

        void FillContrastTab()
        {



        }


        void FillResolutionTab()
        {
            this.textBox_res13.Text = cDcm.cKSpace.lBaseResolution.ToString();
            this.textBox_res14.Text = cDcm.cKSpace.dPhaseResolution.ToString();
            this.textBox_res15.Text = cDcm.cKSpace.dSliceResolution.ToString();
            this.comboBox_res16.SelectedIndex = cDcm.cKSpace.ucPhasePartialFourier;
            this.comboBox_res17.SelectedIndex = cDcm.cKSpace.ucSlicePartialFourier;

            // PAT Tab
            this.comboBox_res20.SelectedIndex = cDcm.cPat.ucPATMode;
            if ( ( cDcm.cPat.ucPATMode ) > 0 )
            {
                this.textBox_res21.Text = cDcm.cPat.lAccelFactPE.ToString();
                
                this.textBox_res23.Text = cDcm.cPat.lAccelFact3D.ToString();
                this.comboBox_res27.SelectedIndex = cDcm.cPat.ucRefScanMode;
                
            }
            
        }


        void FillSequenceTab()
        {
            this.comboBox_seq15.SelectedIndex = cDcm.cKSpace.ucAveragingMode;
            this.comboBox_seq16.SelectedIndex = cDcm.cKSpace.ucMultiSliceMode;
            this.comboBox_res17.SelectedIndex = cDcm.cKSpace.unReordering;
            

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        
        void TestingTesting()
        {
            Console.WriteLine("Testing testing");

            //test DoThis = new test();
            //for (int i = 0; i < 10; i++)
            //{
            //    DoThis.testArray[i] = i;
            //    DoThis.testList.Add(i);

            //}

            //PropertyInfo propInfo = DoThis.GetType().GetProperty("testArray");
            //PropertyInfo propInfo2 = DoThis.GetType().GetProperty("testInt");
            //Console.Write("Test 1: ");
            //Console.Write(DoThis.GetType().GetProperty("testInt") != null);

            //Console.WriteLine(propInfo2.PropertyType.IsArray);
            //Console.Write("Test 2: ");
            //Console.WriteLine(propInfo.PropertyType.IsArray);

            //string testing = "sPhysioImaging.lPhases                   = 1";
            //string[] testing1 = testing.Split('=');
            //string[] testing2 = testing1[0].Split('.');
            //Console.WriteLine(testing2.Length);
            //string final = testing2[1];
            //for (int i = 0; i < (testing2.Length - 2); i++)
            //{
            //    final += "_";
            //    final += testing2[i + 2];
            //}
            //Console.WriteLine(final);



            //Console.WriteLine("Finished");

        }

        // To bind all other textboxes to each other
        private void textBox_rout20_TextChanged(object sender, EventArgs e)
        {
            // Set other textboxes FoV read
            this.textBox_res10.Text = this.textBox_rout20.Text;
            this.textBox_geo20.Text = this.textBox_rout20.Text;
            this.textBox_card20.Text = this.textBox_rout20.Text;

        }

        private void textBox_rout21_TextChanged(object sender, EventArgs e)
        {
            // Bind other textboxes FoV phase
            this.textBox_res11.Text = this.textBox_rout21.Text;
            this.textBox_geo21.Text = this.textBox_rout21.Text;
            this.textBox_rout21.Text = this.textBox_rout21.Text;

        }

        private void textBox_rout22_TextChanged(object sender, EventArgs e)
        {
            // Bind other textboxes Slice Thickness
            this.textBox_res12.Text = this.textBox_rout22.Text;
            this.textBox_geo22.Text = this.textBox_rout22.Text;
        }

        private void textBox_rout23_TextChanged(object sender, EventArgs e)
        {
            // Bind other textboxes TR
            this.textBox_con10.Text = this.textBox_rout23.Text;
            this.textBox_geo23.Text = this.textBox_rout23.Text;
        }

        private void textBox_rout24_TextChanged(object sender, EventArgs e)
        {
            // Bind other textboxes TE
            this.textBox_con11.Text = this.textBox_rout24.Text;

        }



    }
}
