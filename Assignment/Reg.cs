using System;    
using System.Collections.Generic;    
using System.Linq;    
using System.Text;    
using System.Text.RegularExpressions;   

namespace Assignment
{
    public static class Reg
    {   
        public static void Main(string[] args)
        {
        

        }

        public static IEnumerable<string> SplitLine(IEnumerable<string> lines){
    
            foreach(string s in lines){
                 foreach(Match m in Regex.Matches(s, @"[a-zA-Z0-9]+")){
                     yield return m.ToString();
                 }

            }

        }

        public static IEnumerable<(int width, int height)> Resolutions(string resolutions){
                
                foreach(Match m in Regex.Matches(resolutions, @"(?<width>\d*)x(?<height>\d*)")){
                int width = Int32.Parse(m.Groups["width"].ToString());
                int height = Int32.Parse(m.Groups["height"].ToString());
                yield return (width, height);
          }
        }

        public static IEnumerable<string> InnerText(string html, string tag){
         
          
            foreach (Match m in Regex.Matches (html, @"<" + tag + @"[^>]*>" +
                                                     @"(?<innertext>.*?)" +  
                                                     @"</" + tag + @">")) 
            {
                yield return Regex.Replace(m.Groups["innertext"].ToString(), @"</?[^>]*>?", "");
            }

     }  

    }
}
