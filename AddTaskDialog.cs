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

        int StartHour,FinishHour;
        int StartMinutes,FinishMinutes;

        public AddTaskDialog()
        {
            InitializeComponent();
           
            cmbTaskType.Items.Add("Памятка");
            cmbTaskType.Items.Add("Встреча");
            cmbTaskType.Items.Add("Дело");
            
            cmbTaskType.SelectedIndex = 0;

            StartHour = 0; 
            FinishHour = 0;
            StartMinutes = 0; 
            FinishMinutes = 0;
            // 
      
            DateTime dt = new DateTime(1, 1, 1, 0, 0, 0);
         
           for (int i = 0; i < 48; i++)
            {
              cmbStartTime.Items.Add(dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00"));
              cmbFinishTime.Items.Add(dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00"));
              dt = dt.AddMinutes(30);
            }

            cmbStartTime.SelectedIndex = 0;
            cmbFinishTime.SelectedIndex = 0;
        }



        public DateTime CurrentDate
        {
          set {
                dtpStartDate.Value = value;
                dtpFinishDate.Value = value; 
              }
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

        public DateTime StartDateTime
        {
          get {
                textToTime(ref StartHour, ref StartMinutes, cmbStartTime.Text);
                dtpStartDate.Value = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
                dtpStartDate.Value = dtpStartDate.Value.AddHours(StartHour);
                dtpStartDate.Value = dtpStartDate.Value.AddMinutes(StartMinutes);
                return dtpStartDate.Value;
              }
          set {
                cmbStartTime.Text = value.Hour.ToString("00") +":" + value.Minute.ToString("00");
                StartHour = value.Hour;
                StartMinutes = value.Minute;
                dtpStartDate.Value = new DateTime(value.Year,value.Month,value.Day,0,0,0);
            }
        }

        public DateTime FinishDateTime
        {
          get {
                textToTime(ref FinishHour, ref FinishMinutes, cmbFinishTime.Text);
                dtpFinishDate.Value = new DateTime(dtpFinishDate.Value.Year, dtpFinishDate.Value.Month, dtpFinishDate.Value.Day, 0, 0, 0);
                dtpFinishDate.Value = dtpFinishDate.Value.AddHours(FinishHour);
                dtpFinishDate.Value = dtpFinishDate.Value.AddMinutes(FinishMinutes);
                return dtpFinishDate.Value;
            }
          set { 
                cmbFinishTime.Text = value.Hour.ToString("00") + ":" + value.Minute.ToString("00");
                FinishHour = dtpFinishDate.Value.Hour;
                FinishMinutes = dtpFinishDate.Value.Minute;
                dtpFinishDate.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
              }
        }


        private void textToTime(ref int cHour,ref int cMinutes, string text)
        {
          int position; 
          string tmp,tmp2 = ""; 

            position = text.IndexOf(":");
            // Производим проверку на корректность ввода времени.  
            if (position > 2)
            { 
              MessageBox.Show("Не верный формат записи времени");
              return; 
            }
            tmp = text.Substring(0,position);
            tmp2 = text.Substring(position+1).Trim();

            try
            {
               cHour = Convert.ToInt32(tmp);
               cMinutes = Convert.ToInt32(tmp2);
            }
            catch(Exception e)
            {
               MessageBox.Show("Не верный формат записи времени"+ e.ToString());
            }
        }

        private void chbAllDay_CheckedChanged(object sender, EventArgs e)
        {
          if (chbAllDay.Checked)
           {
              cmbStartTime.SelectedIndex = 0;
              cmbFinishTime.SelectedIndex = 0;
              cmbFinishTime.Enabled = false;

              cmbStartTime.Enabled = false;
              cmbFinishTime.Enabled = false;
              cmbStartTime.Enabled = false;
            }
           else
           {
                if (cmbTaskType.Text != "Памятка") cmbFinishTime.Enabled = true;
                cmbStartTime.Enabled = true;
           }
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
            textToTime(ref StartHour, ref StartMinutes, cmbStartTime.Text);
        }

        private void cmbEndTime_Leave(object sender, EventArgs e)
        {
            textToTime(ref FinishHour, ref FinishMinutes, cmbFinishTime.Text);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if ((cmbTaskType.Text == "Памятка")||(dtpFinishDate.Value < dtpStartDate.Value)) 
                dtpFinishDate.Value = dtpStartDate.Value;
        }

        private void cmbTaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (cmbTaskType.Text == "Встреча")
            {
              txbLocation.Enabled = true;
              cmbFinishTime.Enabled = true;
                dtpFinishDate.Enabled = true; 
            }

           if (cmbTaskType.Text == "Памятка")
            {
              txbLocation.Enabled = false;
              cmbFinishTime.Enabled = false;
              dtpFinishDate.Enabled = false;
            }
          
           if (cmbTaskType.Text == "Дело")
            {
                txbLocation.Enabled = false;
                cmbFinishTime.Enabled = true;
                dtpFinishDate.Enabled = true;
            }
        }

        private void txbTaskName_TextChanged(object sender, EventArgs e)
        {
            if ((txbTaskName.Text != "") ? label1.Visible = false : label1.Visible = true) { }
        }


       

    }


}
