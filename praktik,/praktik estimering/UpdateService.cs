using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktik_estimering
{
    class UpdateService
    {
        private UpdateService instance;
        private UpdateService(){}
        public UpdateService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UpdateService();
                }
                return instance;
            }
        }







    }
}
