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
using System.Collections.Generic;

namespace RESTTest.Models
{
    public class EyeemAlbumContainer
    {
         public int offset {get; set;}
         public int limit {get; set;}
         public int total {get; set;}
         public List<EyeemAlbum> items {get; set;}
    }
}
