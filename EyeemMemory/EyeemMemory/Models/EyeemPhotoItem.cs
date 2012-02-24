﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using EyeemMemory.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EyeemMemory.Models
{
    [DataContract]
    public class EyeemPhotoItem
    {
        [DataMember]
        public List<EyeemPhoto> items { get; set; }
    }
}
