using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton.Special_Editors.Level_Editor
{
    public partial class Loading : Form
    {
        public float currentprogress = 0;
        public float totalstepcount = 0;
        public Action OnStepDone;

        public Loading()
        {
            InitializeComponent();

            Size = new Size(343, 166);
        }

        public void StepProgressBar()
        {
            currentprogress += 99f / totalstepcount;
            progressBar.Value = (int)currentprogress;
            OnStepDone();
        }
    }
}
