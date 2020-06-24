using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlWithHeader : StackLayout
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(ControlWithHeader));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty ControlContentProperty = BindableProperty.Create("ControlContent", typeof(View), typeof(ControlWithHeader));
        public View ControlContent
        {
            get { return (View)GetValue(ControlContentProperty); }
            set { SetValue(ControlContentProperty, value); }
        }
        public ControlWithHeader()
        {
            InitializeComponent();
        }
    }
}