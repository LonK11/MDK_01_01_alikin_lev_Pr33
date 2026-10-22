using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatStudents_Alikin.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public int UserFrom { get; set; }

        public int UserTo { get; set; }

        public string Message { get; set; }

        public Messages(int UserFrom, int UserTo, string Message)
        {
            this.UserFrom = UserFrom;
            this.UserTo = UserTo;
            this.Message = Message;
        }
    }
}
