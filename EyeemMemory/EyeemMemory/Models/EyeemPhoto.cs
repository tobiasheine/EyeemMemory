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

namespace RESTTest.Models
{
    public class EyeemPhoto
    {
        public int id {get; set;}
        public string thumbUrl {get; set;}
        public string photoUrl {get; set;}
        public int width {get; set;}
        public int height {get; set;}
        public string updated {get; set;}
    }
       
}
