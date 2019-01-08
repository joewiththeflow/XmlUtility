using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlUtility
{
    public class SwitchAttributesForKey
    {
        //My first task is to create a method which will rearrange attributes for a particular key
        //  e.g. a file which has key/value attributes for various <add> nodes, but you want a way to easily call the 
        //SwitchAttributesForKey method, which will switch attributes from key/value to value/key in the xml
        // <add key="key1" value="value1" /> becomes <add value="value1" key="key2" />

        //we should have a folder called OldFiles and an another called NewFiles


        public static void SwitchAttributes(string key, string firstAttribute, string secondAttribute)
        {
            string oldFilesDirectory = "../../../OldFiles";
            string newFilesDirectory = "../../../NewFiles";

            //create the OldFiles directory if it doesn't exist
            System.IO.Directory.CreateDirectory(oldFilesDirectory);
            //create the NewFiles directory if it doesn't exist
            System.IO.Directory.CreateDirectory(newFilesDirectory);

        }
    }
}
