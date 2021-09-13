using System;
using Xunit;
using Assignment;
using System.Collections.Generic;

namespace Assignment.Tests
{
    public class RegTests
    {
        [Fact]
        public void SplitLine_Splitting_String_On_Special_Characters_And_Removing_Special_Characters()
        {  
             
            var input = new[]{"Hello------World", "Hello_World", "Hello,World", "Hello^World"};
            
            var output = Reg.SplitLine(input);

            Assert.Equal(new[]{"Hello", "World", "Hello", "World", "Hello", "World", "Hello", "World"}, output);
        }

       [Fact]
        public void SplitLine_Splitting_String_On_Whitespace_And_Removing_Whitespace()
        {  
             
            var input = new[]{"Hello World", "HelloWor ld", "He lloWorld", "HelloWorl d"};
            
            var output = Reg.SplitLine(input);

            Assert.Equal(new[]{"Hello", "World", "HelloWor", "ld", "He", "lloWorld", "HelloWorl", "d"}, output);
        }

        [Fact]
        public void Resolutions_Given_Resolution_Stream_Returns_Resolutions_As_Tuples(){
        
            var res1 = "1920x1080";
            var res2 = "1024x768, 800x600, 640x480";
            var res3 = "320x200, 320x240, 800x600";
            var res4 = "1280x960";
            var expected1 = new List<(int width, int height)>() { (1920, 1080) };
            var expected2 = new List<(int width, int height)>() { (1024, 768), (800, 600), (640, 480) };
            var expected3 = new List<(int width, int height)>() { (320, 200), (320, 240), (800, 600) };
            var expected4 = new List<(int width, int height)>() { (1280, 960) };



            var actual1 = Reg.Resolutions(res1);
            var actual2 = Reg.Resolutions(res2);
            var actual3 = Reg.Resolutions(res3);
            var actual4 = Reg.Resolutions(res4);


            
            Assert.Equal(expected1, actual1);
            Assert.Equal(expected2, actual2);
            Assert.Equal(expected3, actual3);
            Assert.Equal(expected4, actual4);

        }
        
        [Fact]
        public void InnerText_Given_HTML_And_Tag_A_Returns_Correct_InnerText_From_Tag_A_As_Strings() {
         
            var html = "<div>\n<p>A <b>regular expression</b>, <b>regex</b> or <b>regexp</b> (sometimes called a <b>rational expression</b>) is, in <a href=\"/wiki/Theoretical_computer_science\" title=\"Theoretical computer science\">theoretical computer science</a> and <a href=\"/wiki/\" title=\"Formal language\">formal language</a> theory, a sequence of <a href=\"/wiki/Character_(computing)\" title=\"Character (computing)\">characters</a> that define a <i>search <a href=\"/wiki/Pattern_matching\" title=\"Pattern matching\">pattern</a></i>. Usually this pattern is then used by <a href=\"/wiki/String_searching_algorithm\" title=\"String searching algorithm\">string searching algorithms</a> for \"find\" or \"find and replace\" operations on <a href=\"/wiki/String_(computer_science)\" title=\"String (computer science)\">strings</a>.</p>\n</div>";
            var tag = "a";
            var expected = new List<string>() {"theoretical computer science", "formal language", "characters", "pattern", "string searching algorithms","strings"};

          
            var actual = Reg.InnerText(html, tag);

          
            Assert.Equal(expected, actual);
        }
        
    }
}
