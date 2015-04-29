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

            Console.WriteLine(textBox22.Controls.Count);
            Console.WriteLine(routPanel1.Controls.Count);

            tabPage_Resolution.BackColor = Color.LightGray;

            // Set all textboxes
            List<Control> TxtBoxes = new List<Control>();
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

            FillResolutionTab();
        }

        void FillResolutionTab()
        {
            this.textBox_res13.Text = cDcm.cKSpace.lBaseResolution.ToString();
            Console.WriteLine(cDcm.cKSpace.lBaseResolution.ToString());
            this.textBox_res14.Text = cDcm.cKSpace.dPhaseResolution.ToString();
            this.textBox_res15.Text = cDcm.cKSpace.dSliceResolution.ToString();
            this.res16.SelectedIndex = cDcm.cKSpace.ucPhasePartialFourier;
            this.res17.SelectedIndex = cDcm.cKSpace.ucSlicePartialFourier;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }




        void TestingTesting()
        {
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


    }
}
