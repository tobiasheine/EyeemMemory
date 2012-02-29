using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using EyeemMemory.Models;
using EyeemMemory.Helper;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Media.Imaging;

namespace EyeemMemory
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Konstruktor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (this.searchBox.Text.Length > 0)
            {
                WebClient client = new WebClient();
                string strUri = JSONHelper.Search_Url + this.searchBox.Text + Secrets.Client_Url;
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(searchTask_DownloadStringCompleted);
                client.DownloadStringAsync(new System.Uri(strUri));
            }
            else
            {
                MessageBox.Show("Enter Album Name First!");
            }
        }

        // Event handler which runs after the feed is fully downloaded.
        private void searchTask_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Showing the exact error message is useful for debugging. In a finalized application, 
                    // output a friendly and applicable string to the user instead. 
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                EyeemRootObject root = new EyeemRootObject();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(root.GetType());
                root = ser.ReadObject(ms) as EyeemRootObject;
                ms.Close();

                if (root.albums.items.Capacity > 0)
                {
                    this.StartImage.Source = new BitmapImage(new Uri(root.albums.items[0].photos.items[2].photoUrl));

                    //this.myCard.FImage.Source = this.StartImage.Source;
                    this.myCard.FrontImage.Source = new BitmapImage(new Uri(root.albums.items[0].photos.items[2].photoUrl));
                    this.myCard.BackImage.Source = new BitmapImage(new Uri(root.albums.items[0].photos.items[1].photoUrl));

                    this.AlbumList.ItemsSource = root.albums.items;
                }
                else
                {
                    MessageBox.Show("No Albums found");
                }
                
                
                
            }
        }
        
        private void StartImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

       
    }
}