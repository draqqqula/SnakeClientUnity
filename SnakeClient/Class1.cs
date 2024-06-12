using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    public class MenuRootUI : WindowUI
    {
        public override void Hide()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            SetActive();
        }
    }
}
