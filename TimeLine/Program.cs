using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLine.Business;



/* There are 4 basic commands (please make sure to put spaces as mention below :
 posting:[user name]->[message]  eg. posting:Laura->Hello World
 reading:[user name] e.g. reading:Laura
 following:[user name] follows [another user] e.g. following:Tom follows Laura
 wall:[user name]  e.g. wall:Laura
 */
namespace TimeLine
{


    class Program
    {
        private const string POSTING = "posting";
        private const string READING = "reading";
        private const string FOLLOWING = "following";
        private const string WALL = "wall";

        static void Main(string[] args)
        {
           
           
          //  Start :
            TimeLineBusiness _tline = new TimeLineBusiness();
            for(int i= 0;i<=100;i++)
            { 
                string str =  Console.ReadLine();
                int start = str.ToString().IndexOf(":");
                string ExecutionCase = str.ToString().Substring(0,start);
           
                string username = string.Empty;
          

           //Check the executed command
               switch(ExecutionCase.ToLower())
                {
                    case POSTING: 
                        int end = str.ToString().IndexOf("-");
                        int length =  (end - start) - 1;
                       username = str.ToString().Substring(start+1,length);
                        string message = str.ToString().Substring(end+2);
                        if (_tline.WriteOnMyTimeline(username, message))
                        {
                            Console.Write("{0}", _tline.ReadComment(username));
                        }
                        else
                            Console.Write("An Error has occured");
                        break; 
                    case READING :
                        username = str.ToString().Substring(start + 1);
                        Console.Write("{0}", _tline.ReadComment(username));
                        break;
                    case FOLLOWING:
                       int endstring = str.ToString().IndexOf("follows") ;
                       username = str.ToString().Substring(start + 1,endstring-(start+2));
                       int followend = endstring + "follows".Length + 1;                     
                        string SuscribeToUser =  str.ToString().Substring(followend);
                        if (_tline.SuscribeUserTimeline(username, SuscribeToUser))
                        {
                            Console.Write("{0}", _tline.ReadComment(username));
                        }
                        else
                        {
                            Console.Write("An Error has occured");
                        }
                        break;
                    case WALL :
                       // int endwall = str.ToString().IndexOf("-");
                        username = str.ToString().Substring(start + 1);
                        Console.Write("{0}", _tline.ReadComment(username,true));
                        break;
                    default:
                        Console.Write("Invalid Command");
                        break;
                }
           
                Console.WriteLine((10-i) + " Commands left...");

            }
        }
    }

   
}

