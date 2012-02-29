using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace EyeemMemory.Models
{
    [DataContract]
    public class EyeemPhoto
    {
        [DataMember]
        public int id {get; set;}
        [DataMember]
        public string thumbUrl {get; set;}
        [DataMember]
        public string photoUrl {get; set;}
        [DataMember]
        public int width {get; set;}
        [DataMember]
        public int height {get; set;}
        [DataMember]
        public string updated {get; set;}

        public BitmapImage rawImage { get; set; }
    }
       
}
