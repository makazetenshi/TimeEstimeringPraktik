using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktik_estimering
{
    class PeriodService
    {
        private static PeriodService instance;

        private PeriodService(){}

        public static PeriodService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PeriodService();
                }
                return instance;
            }
        }








    }
}
