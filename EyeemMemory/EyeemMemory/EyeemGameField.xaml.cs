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
using System.Windows.Media.Imaging;


namespace EyeemMemory
{
    public partial class EyeemGameField : PhoneApplicationPage
    {
        
        BitmapImage image = new BitmapImage();
        static Random generator = new Random();

        public static int[] availablePositions = {0,1,2,3,10,11,12,13,20,21,22,23,30,31,32,33,40,41,42,43};

        public static int GetRandomElement()
        {
            // If there are no elements in the collection, return the default value of T
            if (availablePositions.Count() == 0)
                return -1;  

            int index = generator.Next(availablePositions.Count());
            int element = availablePositions.ElementAt(generator.Next(availablePositions.Count()));
            availablePositions = availablePositions.Except(new int[] { element }).ToArray();

            return element;

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //There is no PopUp open, use the back button normally
            App app = (App)Application.Current;
            app.manager.dt.Stop();
            app.manager.moves = 0;
            base.OnBackKeyPress(e);

        }

        public EyeemGameField()
        {
            InitializeComponent();
            downloadImages();
            App app = (App)Application.Current;
            app.canPlay = true;

            txtMin.Text = "00";
            txtSec.Text = "00";

            
        }

        public void goBack()
        {
            NavigationService.GoBack();
        }

        public void downloadImages()
        {
            App app = (App)Application.Current;
            int i = 0;
            foreach (EyeemPhoto photo in app.selectedAlbum.photos.items)
            {
                photo.rawImage = new BitmapImage(new Uri(photo.photoUrl));
                i++;
                if (i > 9)
                {
                    break;
                }
            }

            buildGameField();
        }

        public void buildGameField()
        {

            availablePositions =  new int[]{0,1,2,3,10,11,12,13,20,21,22,23,30,31,32,33,40,41,42,43};

            App app = (App)Application.Current;
            EyeemMemoryCard myCard = null;
          
            int imageIndex = 0;

            for (int i = 0; i < 20; i++)
            {
                if (imageIndex > 9)
                {
                    imageIndex = 0;
                }

                myCard = new EyeemMemoryCard("grey", app.selectedAlbum.photos.items[imageIndex]);
                this.GameField.Children.Add(myCard);

                int position = GetRandomElement();

                int row = position / 10;
                int colum = position % 10;

                Grid.SetRow(myCard,row);
                Grid.SetColumn(myCard, colum);
                colum++;

                
                imageIndex++;
            }
         }
    }
}