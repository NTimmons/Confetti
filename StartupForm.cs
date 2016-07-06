using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Confetti
{
    public partial class StartupForm : Form
    {
        public List<String> m_nodes = new List<string>();
        public int m_sourcePort;
        public int m_destinationPort;
        public int m_requestID;
        public int m_outgoingID;
        public String m_filename;

        public StartupForm()
        {
            InitializeComponent();

            m_nodes.Add("127.0.0.1");

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            IpDisplay.Items.Clear();
            for(int i =0; i < m_nodes.Count(); i++)
            {
                IpDisplay.Items.Add(m_nodes[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_sourcePort = int.Parse(srcPortBox.Text);
            m_destinationPort = int.Parse(dstPortBox.Text);
            m_requestID = int.Parse(reqIdBox.Text);
            m_outgoingID = int.Parse(outgoingIdBox.Text);
            m_filename = filenameBox.Text;

            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String text = IpEntry.Text;

            //Validate()

            m_nodes.Add(text);

            UpdateListBox();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = IpDisplay.SelectedIndex;

            if(index != -1)
            {
                m_nodes.RemoveAt(index);
                UpdateListBox();
            }
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            int size = -1;

            DialogResult result = DialogResult.OK;
            result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                filenameBox.Text = file;
            }
        }
    }
}
