using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    public partial class DateTimePickerBox : Form
    {
        public event Action<DateTime> DateTiemPick;

        public DateTimePickerBox()
        {
            InitializeComponent();
            cBoxSetTime();

        }

        private void cBoxSetTime()
        {
            cBoxHour.DropDownStyle = ComboBoxStyle.DropDownList;
            cBoxMinute.DropDownStyle = ComboBoxStyle.DropDownList;
            
            // cBoxHour에 00시부터 24시까지 추가
            for (int i = 0; i <= 24; i++)
            {
                cBoxHour.Items.Add(i.ToString("D2"));
            }
            
            // cBoxMinit에 00분부터 59분까지 추가
            for (int i = 0; i < 60; i++)
            {
                cBoxMinute.Items.Add(i.ToString("D2"));
            }
        }

        public void GetDateTime(DateTime dateTime,bool selection)
        {
            dtpBox.Value = dateTime;
            if(selection == true)
            {
                cBoxHour.SelectedItem = dateTime.Hour.ToString("D2");
                cBoxMinute.SelectedItem = dateTime.Minute.ToString("D2");
            }
            else
            {
                cBoxHour.Visible = false;
                cBoxMinute.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                cBoxHour.SelectedIndex = 0;
                cBoxMinute.SelectedIndex = 0;
            }
            
        }
        
        private void bntOk_Click(object sender, EventArgs e)
        {
            DateTime getDatetime = new DateTime( 
                dtpBox.Value.Year, 
                dtpBox.Value.Month, 
                dtpBox.Value.Day, 
                int.Parse(cBoxHour.SelectedItem.ToString()), 
                int.Parse(cBoxMinute.SelectedItem.ToString()),
                0);

            DateTiemPick?.Invoke(getDatetime);
            Close();
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
