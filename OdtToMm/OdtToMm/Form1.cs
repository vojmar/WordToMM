﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OdtToMm
{
    public partial class Form1 : Form
    {
        private OpenFileDialog ODTofd;
        private SaveFileDialog MMsfd;
        private MMParser mmparser;
        private string ODTFilePath;
        private string MMFilePath;
        
        //DELEGATES FOR THREAD-SAFE OPERATIONS
        delegate void PB1AddCallback(int num);


        public Form1()
        {
            InitializeComponent();
            ODTofd = new OpenFileDialog();
            ODTofd.Filter = "Odt files |*.odt|"+
                "Word files |*.doc;*.docx|" +
                "All files (*.*)|*.*";
            ODTofd.Multiselect = false;
            MMsfd = new SaveFileDialog();
            mmparser = new MMParser();
            mmparser.OnNodeParseStep += Mmparser_OnNodeParseStep;
        }

        private void Mmparser_OnNodeParseStep(object sender, NodeParseStepEventArgs e)
        {
            int remaining = progressBar1.Maximum - progressBar1.Value;
            int add = remaining * e.CurrentCount / e.NodeCount;
            if (add + progressBar1.Value <= progressBar1.Maximum)
            {
                if (progressBar1.InvokeRequired)
                {
                    PB1AddCallback d = new PB1AddCallback((num) =>
                    {
                        progressBar1.Value += num;
                    });
                    this.Invoke(d, new object[] { add });                }
                else
                {
                    progressBar1.Value += add;
                }
            }
            else
            {
                progressBar1.Value = progressBar1.Maximum;
            }
        }

        private void ODTbtn_Click(object sender, EventArgs e)
        {
            if (ODTofd.ShowDialog() == DialogResult.OK)
            {
                ODTtb.Text = ODTofd.FileName;
                ODTFilePath = ODTofd.FileName;
            }
        }

        private void MMbtn_Click(object sender, EventArgs e)
        {
            if (MMsfd.ShowDialog() == DialogResult.OK)
            {
                MMtb.Text = MMsfd.FileName;
                MMFilePath = MMsfd.FileName;
            }
        }

        

        private async void Cbtn_Click(object sender, EventArgs e)
        {
            if (
                (ODTFilePath != null) &&
                (ODTFilePath != "") &&
                (MMFilePath != null) &&
                (MMFilePath != "")
               )
            {
                FreeMindNodeCollection fmnCollection = await OdtParser.ParseOdt(ODTFilePath);
                await mmparser.ParseAndSaveMM(MMFilePath, fmnCollection);
                MessageBox.Show("STUFF'S DONE");
                //TODO: Just add some magic - parse and save in separate thread, events, progress bar...
            }
        }
    }
}
