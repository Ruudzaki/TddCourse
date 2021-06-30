using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.Test
{
    internal class FakeSettings : ISetting
    {
        private bool isEnabled;
        private int maxUses = 1000;

        public FakeSettings(bool isEnabled)
        {
            this.isEnabled = isEnabled;
        }

        public FakeSettings(bool isEnabled, int maxUses) : this(isEnabled)
        {
            this.maxUses = maxUses;
        }

        public bool IsEnabled()
        {
            return isEnabled;
        }

        public int MaxUses()
        {
            return maxUses;
        }
    }
}
