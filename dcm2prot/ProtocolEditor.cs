using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace dcm2prot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

//            this.tabGroup.Alignment = TabAlignment.Bottom;
                        
            initProtocolEntries();

        }

        void initProtocolEntries()
        {
            Font textFont = new Font("Arial", 12.0f);
            string txtBoxName;

            Console.WriteLine(textBox22.Controls.Count);
            Console.WriteLine(routPanel1.Controls.Count);

            tabPage_Resolution.BackColor = Color.LightGray;


            List<Control> TxtBoxes = new List<Control>();
            FindControls(this, TxtBoxes, "textBox");

            foreach (TextBox tbox in TxtBoxes)
            {
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

                if (dec == 10)
                {
                    charValue[0] = Convert.ToChar(dec);
                    sb.Append(charValue[0].ToString());
                }
                else if (dec != 10 && dec != 0)
                {
                    charValue[0] = Convert.ToChar(dec);
                    sb.Append(charValue[0].ToString());
                }
            }

            DicomContainer cDcm = new DicomContainer(sb.ToString());

            this.res13.Text = cDcm.cKSpace.lBaseResolution.ToString();
            Console.WriteLine(cDcm.cKSpace.lBaseResolution.ToString());
            this.res14.Text = cDcm.cKSpace.dPhaseResolution.ToString();
            this.res15.Text = cDcm.cKSpace.dSliceResolution.ToString();
            this.res16.SelectedIndex = cDcm.cKSpace.ucPhasePartialFourier;
            this.res17.SelectedIndex = cDcm.cKSpace.ucSlicePartialFourier;

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
