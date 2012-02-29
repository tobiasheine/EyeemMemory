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
using EyeemMemory.Helper;
using EyeemMemory.Models;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Coding4Fun.Phone.Controls;
using Microsoft.Xna.Framework.Media;
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundAudio;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace EyeemMemory
{
    public partial class EyeemPanorama : PhoneApplicationPage
    {

        public List<EyeemAlbum> foundAlbums;

        private void CopyToIsolatedStorage()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] files = new string[] { "gamesoundtrack_final.mp3" };

                foreach (var _fileName in files)
                {
                    if (!storage.FileExists(_fileName))
                    {
                        string _filePath = "sounds/" + _fileName;
                        StreamResourceInfo resource = Application.GetResourceStream(new Uri(_filePath, UriKind.Relative));

                        using (IsolatedStorageFileStream file = storage.CreateFile(_fileName))
                        {
                            int chunkSize = 4096;
                            byte[] bytes = new byte[chunkSize];
                            int byteCount;

                            while ((byteCount = resource.Stream.Read(bytes, 0, chunkSize)) > 0)
                            {
                                file.Write(bytes, 0, byteCount);
                            }
                        }
                    }
                }
            }
        }

        public EyeemPanorama()
        {
            InitializeComponent();

            App app = (App)Application.Current;
            Highscore_List.ItemsSource = app.highscoreList;
                     
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Search_Text.Text.Length > 0)
            {
                WebClient client = new WebClient();
                string strUri = JSONHelper.Search_Url + this.Search_Text.Text.Replace(" ", "+") + Secrets.Client_Url;
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
                   this.foundAlbums = root.albums.items;
                   this.Album_List.ItemsSource = this.foundAlbums;
                   this.Album_List.SelectionChanged += new SelectionChangedEventHandler(Album_List_SelectionChanged);
                   
                }
                else
                {
                    MessageBox.Show("No Albums found");
                }

            }
        }

        void Album_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App app = (App)Application.Current;

            int index = (sender as ListBox).SelectedIndex;

            if (index > -1)
            {
                app.selectedAlbum = this.foundAlbums[index];
            
            var messagePrompt = new MessagePrompt
            {
                Title = "Are you sure?",
                Body = new TextBlock { Text = "Do you want to use the Album " + app.selectedAlbum.name, Foreground = new SolidColorBrush(Colors.Green), FontSize = 30.0, TextWrapping = TextWrapping.Wrap },
                IsAppBarVisible = true,
                IsCancelVisible = true
            };
            messagePrompt.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(messagePrompt_Completed);
            messagePrompt.Show();
            }

        }

        void messagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult.Equals(Coding4Fun.Phone.Controls.PopUpResult.Ok))
            {
               
                NavigationService.Navigate(new Uri("/EyeemGameField.xaml", UriKind.Relative));
            }
        }

        private void Album_List_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            if (app.popularAlbum == null || app.popularAlbum.photos.items.Capacity <= 0)
            {
                MessageBox.Show("Default Album not yet loaded");
            }
            else
            {
                app.selectedAlbum = app.popularAlbum;
                NavigationService.Navigate(new Uri("/EyeemGameField.xaml", UriKind.Relative));
            }
            
        }

       
    }
}