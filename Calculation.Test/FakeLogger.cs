using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.Test
{
    public class FakeLogger : ILogger
    {
        public string LastResult { get; set; }
        public void Write(string text)
        {
            LastResult = text;
        }
    }
}
