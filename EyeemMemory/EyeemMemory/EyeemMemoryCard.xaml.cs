using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using EyeemMemory.Models;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace EyeemMemory
{
	public partial class EyeemMemoryCard : UserControl
	{
        public EyeemPhoto myPhoto { get; set; }

        public bool canBeChanged { get; set; }

        public EyeemMemoryCard(string frontImage, EyeemPhoto photo)
		{
			// Für das Initialisieren der Variablen erforderlich
			InitializeComponent();
            canBeChanged = true;
            this.FrontImage.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(FrontImage_ManipulationCompleted);
            //this.BackImage.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(FrontImage_ManipulationCompleted);
            this.BackImage.Visibility = Visibility.Collapsed;
            this.myPhoto = photo;
            BitmapImage image = new BitmapImage();
            
            if (frontImage.Equals("black"))
            {
                image.UriSource = new Uri("Images/card_black.png", UriKind.Relative);
                this.FrontImage.Source = image;
            }
            else
            {
                image.UriSource = new Uri("Images/card_grey.png", UriKind.Relative);
                this.FrontImage.Source = image;
            }

            this.BackImage.Source = photo.rawImage;
            this.BackImage.Visibility = Visibility.Visible;

		}

        void FrontImage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            App app = (App)Application.Current;

            if (canBeChanged && app.canPlay)
            {
                Stream stream = TitleContainer.OpenStream("sounds/flip/" + RandomNumber(1, 10) + ".wav");
                SoundEffect effect = SoundEffect.FromStream(stream);
                FrameworkDispatcher.Update();
                effect.Play();
                app.manager.setNextCard(this);
            }
            
            /*
            if (e.OriginalSource == this.FrontImage)
            {
                Console.Write("foo");
                this.FrontImage.Visibility = Visibility.Collapsed;
                this.BackImage.Visibility = Visibility.Visible;
            }
            else if (e.OriginalSource == this.BackImage)
            {
                Console.Write("foo");
                this.BackImage.Visibility = Visibility.Collapsed;
                this.FrontImage.Visibility = Visibility.Visible;
            }*/
            
        }

        private String RandomNumber(int min, int max)
        {
            Random random = new Random();
            int num = random.Next(min, max);
            if (num >= 10)
            {
                return "" + num;
            }
            else
            {
                return "0" + num;
            }
        }
        
    }
}