using System;
using System.IO;

namespace LoodonDAL
{
    public class ClsFileOperations
    {
        public void ReadFile(string filePath)
        {
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                var sr = new StreamReader(filePath);

                //Read the first line of text
                var line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public static void WriteFile(string filePath, string fileText)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(filePath);

                //Write a line of text
                sw.WriteLine(fileText);                

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public static void AppendFile(string filePath, string fileText)
        {
            try
            {
                // This text is always added, making the file longer over time 
                // if it is not deleted. 
                using (var sw = File.AppendText(filePath))
                {
                    sw.WriteLine(fileText);                   
                }	                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
