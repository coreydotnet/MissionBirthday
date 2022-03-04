using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionBirthday.Contracts.Ocr
{
    public class OcrResults
    {
        public OcrResults(IEnumerable<string> textLines)
        {
            TextLines = textLines.ToArray();
        }

        public ICollection<string> TextLines { get; }
    }
}
