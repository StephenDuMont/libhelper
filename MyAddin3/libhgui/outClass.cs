using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libhgui
{
    public class outClass
    {
        public List<string> incdir=new List<string>();
        public List<string> liblinks = new List<string>();
        public List<string> adddep = new List<string>();
        public string cond;
        public outClass()
        {
           
        }
        public override string ToString()
        {
            return cond;
        }

    }

}
