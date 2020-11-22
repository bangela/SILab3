using MvvmCross.Forms.Views;
using SI.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : MvxContentPage<FirstPageViewModel>
    {
        public FirstPage()
        {
            InitializeComponent();
        }
    }
}