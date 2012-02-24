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



namespace RESTTest.Models
{
    //[DataContract]
    public class EyeemAlbum
    {
        //[DataMember]
        public int id {get; set;}
        public string name {get; set;}
        public string thumbUrl {get; set;}
        public string updated {get; set;}
        public string type {get; set;}
        public int totalPhotos {get; set;}
        public int totalLikers {get; set;}
        public int totalContributors {get; set;}
        public List<EyeemPhoto> photos {get; set;}
    }
}
