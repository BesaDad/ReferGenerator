using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForSupport.Models
{
    [Serializable]
    public class Refer
    {
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ReferText { get; set; }
    }
}
