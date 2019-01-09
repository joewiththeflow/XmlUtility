using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlUtility
{
    public class SwitchAttributesForKey
    {
        private static string key;
        private static string firstAttribute;
        private static string secondAttribute;

        //My first task is to create a method which will rearrange attributes for a particular key
        //  e.g. a file which has key/value attributes for various <add> nodes, but you want a way to easily call the 
        //SwitchAttributesForKey method, which will switch attributes from key/value to value/key in the xml
        // <add key="key1" value="value1" /> becomes <add value="value1" key="key2" />

        //we should have a folder called OldFiles and an another called NewFiles

        private static void CreateFolder(string path)
        {
            //create the directory if it doesn't exist
            System.IO.Directory.CreateDirectory(path);
        }

        public static void SwitchAttributes(string key, string firstAttribute, string secondAttribute)
        {

            SwitchAttributesForKey.key = key;
            SwitchAttributesForKey.firstAttribute = firstAttribute;
            SwitchAttributesForKey.secondAttribute = secondAttribute;
            string workingDirectory = "../../";
            string oldFilesDirectory = workingDirectory + "OldFiles/";
            string newFilesDirectory = workingDirectory + "NewFiles/";

            //create the OldFiles directory if it doesn't exist
            CreateFolder(oldFilesDirectory);
            //create the NewFiles directory if it doesn't exist
            CreateFolder(newFilesDirectory);

            //Get a list of the fles in the OldFiles directory
            FileInfo[] oldFiles = new DirectoryInfo(oldFilesDirectory).GetFiles();

            //loop through each file
            foreach(FileInfo fileInfo in oldFiles)
            {
                //load the original xml file into memory
                XmlDocument oldFile = new XmlDocument();
                oldFile.Load(fileInfo.FullName);

                //I think you want to load the new file here as well
                XmlDocument newFile = new XmlDocument();
                newFile.Load(fileInfo.FullName);

                //access the document element of the file
                XmlNode currentNode = oldFile.DocumentElement;
                //get a list of all the nodes in the xml file
                XmlNodeList listOfNodes = oldFile.SelectNodes("*");

                
                string completeXmlText = "<? xml version =\"1.0\"?>";
                completeXmlText += ExtractOutXml(listOfNodes[0]);

                //write this out to the new file
                using (var writer = System.IO.File.CreateText(newFilesDirectory + fileInfo.Name))
                {
                    writer.WriteLine(completeXmlText);
                    /*writer.WriteLine();
                    foreach (XmlNode node in listOfNodes)
                    {
                        writer.WriteLine(node.OuterXml);   // InnerXml to get only the content
                    }
                    */
                }

            }
        }
        //I'm going to attempt to make this a recursive method
        private static string ExtractOutXml(XmlNode node)
        {
            bool doesCurrentNodeMatchKey = (node.Name == key);
            string temp = "";
            //base case
            if ((node.NextSibling == null) && !(node.HasChildNodes))
            {
                //temp += node.Name;

                //if node = key passed in to the program then need to get the attributes switched
                if (doesCurrentNodeMatchKey)
                {
                    return temp + "\r\n" + "<" + node.Name + " " + secondAttribute + "=" + '"' +
                        node.Attributes[secondAttribute].Value + '"' + " " + firstAttribute + "=" + '"' +
                        node.Attributes[firstAttribute].Value + '"' + " />";
                }
                else
                {
                    return temp + "\r\n" + "<" + node.Name + ">";
                }
            }
            if (!doesCurrentNodeMatchKey)
            {
                temp += "\r\n" + "<" + node.Name + ">";
            }
                
            if (node.HasChildNodes)
            {
                temp += ExtractOutXml(node.FirstChild) + "\r\n" + "</" + node.Name + ">";

            }

            if ((node.NextSibling) != null)
            {
                if (doesCurrentNodeMatchKey)
                {
                    temp += "\r\n" + "<" + node.Name + " " + secondAttribute + "=" + '"' +
                        node.Attributes[secondAttribute].Value + '"' + " " + firstAttribute + "=" +'"' +
                        node.Attributes[firstAttribute].Value + '"' + " />";
                }
               
                    temp += ExtractOutXml(node.NextSibling);
                
            }
            return temp; //+ "\r\n" + node.Name + "\r\n";
        }
    }
}
