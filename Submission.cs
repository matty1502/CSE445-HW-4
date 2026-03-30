using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Submission
    {
        public static string xmlURL = "https://raw.githubusercontent.com/matty1502/CSE445-HW-4/refs/heads/main/NationalParks.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/matty1502/CSE445-HW-4/refs/heads/main/NationalParksErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/matty1502/CSE445-HW-4/refs/heads/main/NationalParks.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            string errors = ""; //track errors

            try
            {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdUrl); //xsd from git\
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas = schemas;
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (sender,e) =>
            {
                errors += $"Verification error: {e.Message}";
            };
            using(XmlReader reader = XmlReader.Create(xmlUrl, settings))
            {
                while(reader.Read())
                {
                    //read file
                }
            }
            return string.IsNullOrEmpty(errors) ? "No errors found" : errors;

            }
            catch(Exception ex)
            {
                return $"Verification error: {ex.Message}";
            }

        
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                //load xml from git
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                //convert to json
                string json = JsonConvert.SerializeXmlNode(doc,Newtonsoft.Json.Formatting.Indented, false);
                return json;
            }
            catch (Exception ex)
            {
                return $"Error with conversion: {ex.Message}";
            }
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
        }

        // Helper method to download content from URL
        private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

}