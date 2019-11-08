using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary
{
    public partial class AddTaskDialog : Form
    {
        public AddTaskDialog()
        {
            InitializeComponent();
           
            cmbTaskType.Items.Add("Памятка");
            cmbTaskType.Items.Add("Встреча");
            cmbTaskType.Items.Add("Дело");
            
            cmbTaskType.SelectedIndex = 0;
         // 
            int i;
            DateTime dt = new DateTime(1, 1, 1, 0, 0, 0);
         

            for (i = 0; i < 48; i++)
            {
              cmbStartTime.Items.Add(dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00"));
              cmbEndTime.Items.Add(dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00"));
              dt = dt.AddMinutes(30);
            }
            cmbStartTime.SelectedIndex = 0;
            cmbEndTime.SelectedIndex = 0;
           
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmbStartTime_Leave(object sender, EventArgs e)
        {
         ////string s = cmbstarttime.text;
         ////decimal result;

         ////if (decimal.tryparse(s, out result))
         ////{
         ////    cmbstarttime.text = result.tostring("hh:mm");
         ////    // d[0] = convert.todatetime("01.01.2016 15:25");
         ////}
        }

        public string TaskName
        {
            get { return txbTaskName.Text; }
            set { txbTaskName.Text = value; }
        }

        public string TaskType
        {
            get { return cmbTaskType.Text; }
            set { cmbTaskType.Text = value; }
        }

        public string TaskLocation
        {
            get { return txbLocation.Text; }
            set { txbLocation.Text = value; }
        }

        public string StartTime
        {
            get { return cmbStartTime.Text; }
            set { cmbStartTime.Text = value; }
        }

        public string EndTime
        {
            get { return cmbEndTime.Text; }
            set { cmbEndTime.Text = value; }
        }

        private void chbAllDay_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAllDay.Checked)
            {
              cmbStartTime.SelectedIndex = 0;
              cmbEndTime.SelectedIndex = 0;
              cmbEndTime.Enabled = false;
              cmbStartTime.Enabled = false;
            }
            else
            {
                if (cmbTaskType.Text != "Памятка") cmbEndTime.Enabled = true;
                cmbStartTime.Enabled = true;

            }
        }

        private void cmbTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (cmbTaskType.Text == "Встреча")
            {
              txbLocation.Enabled = true;
              cmbEndTime.Enabled = true;
            }

           if (cmbTaskType.Text == "Памятка")
            {
              txbLocation.Enabled = false;
              cmbEndTime.Enabled = false;
            }
          
           if (cmbTaskType.Text == "Дело")
            {
                txbLocation.Enabled = false;
                cmbEndTime.Enabled = true;
            }
        }

        private void txbTaskName_TextChanged(object sender, EventArgs e)
        {
            if ((txbTaskName.Text != "") ? label1.Visible = false : label1.Visible = true) { }
        }
    }
}
