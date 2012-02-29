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
using System.ServiceModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;



namespace EyeemMemory.Models
{
    [DataContract]
    public class EyeemAlbum
    {
        [DataMember]
        public int id {get; set;}
        [DataMember]
        public string name {get; set;}
        [DataMember]
        public string thumbUrl {get; set;}
        [DataMember]
        public string updated {get; set;}
        [DataMember]
        public string type {get; set;}
        [DataMember]
        public int totalPhotos {get; set;}
        [DataMember]
        public int totalLikers {get; set;}
        [DataMember]
        public int totalContributors {get; set;}
        [DataMember]
        public EyeemPhotoItem photos {get; set;}


        public ImageSource thumbnail { 
            get
            {
                return new BitmapImage(new Uri(this.thumbUrl));
            }
            set { ; } 
        }
    }
}
