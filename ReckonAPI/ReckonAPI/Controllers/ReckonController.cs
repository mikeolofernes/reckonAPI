using Microsoft.AspNetCore.Mvc;
using ReckonAPI.Models;
using System.Text;

namespace ReckonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReckonController : ControllerBase
    {
        string baseText = "Peter told me (actually he slurrred) that peter the pickle piper piped a pitted pickle before he petered out. Phew!";
        List<string> tmpList = new List<string>();

        [Route("textToSearch")]
        [HttpGet]
        public string textToSearch(string text)
        {
            baseText = text;
            return baseText;
        }

        [Route("subTexts")]        
        [HttpGet]
        public SubText subTexts(string? searches)
        {
            SubText tmpSubtext = new SubText();
           
            tmpSubtext.SubTexts = getSubText(searches);
            
            return tmpSubtext;
        }

        [Route("submitResults")]
        [HttpGet]
        public SubmitResult submitResults(string? searches)
        {           
            List<List<string>> resultList = new List<List<string>>();
            List<string> subtexts = new List<string>();
            List<int> result = new List<int>();
            List<SubTextResult> subTextResult = new List<SubTextResult>();

            if (tmpList.Count == 0)
            {
                getSubText(searches);
            }

            foreach (string tmp in tmpList)
            {
                SubTextResult tmpSTR = new SubTextResult();
                tmpSTR.subtext=tmp;                
                tmpSTR.result = getTextPosition(tmp, baseText);        
                subTextResult.Add(tmpSTR);
            }

            var results = new SubmitResult
            {
                candidate = "Michael Olofernes",
                text = baseText,
                results = subTextResult
            };
            return results;
        }

        private List<string> getSubText(string? searches)
        {           
            if (string.IsNullOrEmpty(searches))
                searches = "Peter,peter,Pick,Pi,Z";

            tmpList = searches.Split(",").ToList();
            return tmpList;
        }
        private List<int> getTextPosition(string txtSearch, string txtToSearchFrom)
        {
            List<int> result = new List<int>();
            string tmp = RemoveSpecialCharacters(txtToSearchFrom);
            string[] tmpArray = tmp.Split(" ");
            int txtLength = 1;

            foreach(string str in tmpArray)
            {
                if(str.ToLower().StartsWith(txtSearch.ToLower()))
                {
                    result.Add(txtLength);
                }

                txtLength = txtLength + str.Length + 1;
            }

            if(result.Count == 0)
                result.Add(0);

            return result;

        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

    }
}
