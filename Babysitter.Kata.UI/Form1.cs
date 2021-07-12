using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babysitter.Kata.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void
        OnCalculateButtonClick(object sender, EventArgs e)
        {
            var startTime = DateTime.Today;
            var endTime   = DateTime.Today;
            var bedtime   = DateTime.Today;

            // if AM, make it tomorrow
            if (this.startTimePicker.Value.Hour < 12) { startTime = startTime.AddDays(1); }
            if (this.endTimePicker  .Value.Hour < 12) { endTime   = endTime  .AddDays(1); }
            if (this.bedtimePicker  .Value.Hour < 12) { bedtime   = bedtime  .AddDays(1); }

            startTime = startTime.AddHours(this.startTimePicker.Value.Hour).AddMinutes(this.startTimePicker.Value.Minute).AddSeconds(this.startTimePicker.Value.Second);
            endTime   = endTime  .AddHours(this.endTimePicker  .Value.Hour).AddMinutes(this.endTimePicker  .Value.Minute).AddSeconds(this.endTimePicker  .Value.Second);
            bedtime   = bedtime  .AddHours(this.bedtimePicker  .Value.Hour).AddMinutes(this.bedtimePicker  .Value.Minute).AddSeconds(this.bedtimePicker  .Value.Second);

            var babysitter = new Babysitter.Kata.Core.Babysitter() { StartTime = startTime, EndTime = endTime, BedTime = bedtime };
            var result     = babysitter.CalculateRate();
            if (result == Core.Babysitter.ERROR_START_TOO_EARLY) {
                this.outputLabel.Text = "Can't start earlier than 5:00PM";
            }
            if (result == Core.Babysitter.ERROR_END_TOO_LATE) {
                this.outputLabel.Text = "Can't end later than 4:00AM";
            }
            if (result == Core.Babysitter.ERROR_BEDTIME_TOO_LATE) {
                this.outputLabel.Text = "Bedtime must be before midnight";
            }
            if (result == Core.Babysitter.ERROR_START_AFTER_END) {
                this.outputLabel.Text = "Can't start after end";
            }
            if (result == -1) {
                this.outputLabel.Text = "Unknown error";
            }
            if (result >= 0) {
                this.outputLabel.Text = string.Format("${0}.00", result);
            }
        }
    }
}
