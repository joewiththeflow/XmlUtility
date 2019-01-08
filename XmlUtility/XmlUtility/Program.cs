using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//The aim of this program is to help with XML files going forward.
//For example, my first task is to create a method which will rearrange attributes for a particular key
//  e.g. a file which has key/value attributes for various <add> nodes, but you want a way to easily call the 
//SwitchAttributesForKey method, which will switch attributes from key/value to value/key in the xml
// <add key="key1" value="value1" /> becomes <add value="value1" key="key2" />

namespace XmlUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!(args == null) && args.Length == 4)
            {
                switch (args[0])
                {
                    case "SwitchAttributesForKey":
                        SwitchAttributesForKey.SwitchAttributes(args[1], args[2], args[3]);
                        break;
                }
            }
        }
    }
}