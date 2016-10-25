using System;
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
        private string ODTFilePath;
        private string MMFilePath;
        public Form1()
        {
            InitializeComponent();
            ODTofd = new OpenFileDialog();
            MMsfd = new SaveFileDialog();
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

        private void Cbtn_Click(object sender, EventArgs e)
        {

            if (
                (ODTFilePath != null) &&
                (ODTFilePath != "") &&
                (MMFilePath != null) &&
                (MMFilePath != "")
               )
            {
                OdtParser odtParser = new OdtParser(ODTFilePath);
                MMParser mmParser = new MMParser(MMFilePath);
                var fmnCollection = odtParser.GetOdtContent();
                //TODO: Just add some magic
            }
        }
    }
}
