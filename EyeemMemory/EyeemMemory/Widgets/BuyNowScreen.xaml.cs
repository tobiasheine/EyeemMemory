using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Tasks;
using EyeemMemory.Models;
using System.Windows.Media.Imaging;
using System;

namespace HowtoUsePopup
{
    public partial class PhotoPopup : UserControl
    {
        private EyeemPhoto myPhoto;

        public PhotoPopup(EyeemPhoto photoObj)
        {
            InitializeComponent();
            myPhoto = photoObj;
            photo.Source = new BitmapImage(new Uri(myPhoto.photoUrl));
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePopup();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri("http://www.eyeem.com/p/"+myPhoto.id); 
            webBrowserTask.Show(); 
        }

        private void ClosePopup()
        {
            Popup buyPop = this.Parent as Popup;
            buyPop.IsOpen = false;
        }
    }
}
