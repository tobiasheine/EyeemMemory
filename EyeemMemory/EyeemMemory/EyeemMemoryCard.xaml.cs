using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EyeemMemory
{
	public partial class EyeemMemoryCard : UserControl
	{

        public EyeemMemoryCard()
		{
			// Für das Initialisieren der Variablen erforderlich
			InitializeComponent();
            this.FrontImage.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(FrontImage_ManipulationCompleted);
            this.BackImage.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(FrontImage_ManipulationCompleted);
            this.BackImage.Visibility = Visibility.Collapsed;
            
		}

        void FrontImage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
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
            }
            
        }

        
    }
}