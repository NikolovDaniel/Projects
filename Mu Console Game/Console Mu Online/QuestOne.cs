using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Mu_Online
{
    class QuestOne
    {
        public string status { get; set; }
        public int countRats { get; set; }
        public int countBears { get; set; }

        public void Setup()
        {
            status = "Not active";
            countRats = 0;
            countBears = 0;
        }
    }
}
