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
        private MMParser mmparser;
        private string ODTFilePath;
        private string MMFilePath;
        
        //DELEGATES FOR THREAD-SAFE OPERATIONS
        delegate void Form1Callback<T>(T args);


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
            mmparser.OnMMParseStarted += Mmparser_OnMMParseStarted;
            mmparser.OnMMParseEnded += Mmparser_OnMMParseEnded;
        }

        private void Mmparser_OnMMParseEnded(object sender, MMParseEndedEventArgs e)
        {
            string outputText = (e.successful) ? "Done! Ready for input!" : "Error creating MM File!";
            if (label1.InvokeRequired)
            {
                Form1Callback<string> editText = new Form1Callback<string>((text) =>
                {
                    label1.Text = text;
                });
                this.Invoke(editText, new object[] { outputText });
            }
            else
            {
                label1.Text = outputText;
            }
        }

        private void Mmparser_OnMMParseStarted(object sender, EventArgs e)
        {
            if(label1.InvokeRequired)
            {
                Form1Callback<string> editText = new Form1Callback<string>((text)=>
                {
                    label1.Text = text;
                });
                this.Invoke(editText, new object[] { "Creating MM File" });
            }
            else
            {
                label1.Text = "Creating MM File";
            }
        }

        private void Mmparser_OnNodeParseStep(object sender, NodeParseStepEventArgs e)
        {
            int remaining = progressBar1.Maximum - progressBar1.Value;
            int add = remaining * e.CurrentCount / e.NodeCount;
            if (add + progressBar1.Value > progressBar1.Maximum)
            {
                add = progressBar1.Maximum - progressBar1.Value;
            }
            if (progressBar1.InvokeRequired) //THREAD SAFE CALLS TO WINDOWS FORMS CONTROLS -> Create a delegate and invoke from Form1
            {
                Form1Callback<int> addDel = new Form1Callback<int>((num) =>
                {
                    progressBar1.Value += num;
                });
                this.Invoke(addDel, new object[] { add });                }
            else //IF IT IS SAFE TO CALL (should never happen, but who knows)
            {
                progressBar1.Value += add;
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
            }
        }
    }
}
