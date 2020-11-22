using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.IoC;
using SI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.UWP
{
    public class Setup : MvxFormsWindowsSetup<SI.Core.App, SI.App>
    {
        protected override IMvxIoCProvider InitializeIoC()
        {
            var provider = base.InitializeIoC();
            return SetupIoC.RegisterDependencies(provider);
        }
    }
}
