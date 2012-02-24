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
using System.Linq;
using System.Collections.Generic;
using EyeemMemory.Models;

namespace EyeemMemory.Helper
{
    public class JSONHelper
    {

        public static List<EyeemAlbum> sampleData()
        {
            /*List<EyeemAlbum> result = new List<EyeemAlbum>();

            EyeemAlbum myAlbum = new EyeemAlbum();
            myAlbum.name = "Readmill HQ";
            myAlbum.type = "venue";
            myAlbum.totalPhotos = 26;
            myAlbum.thumbUrl = "http://www.eyeem.com/thumb/sq/200/75ed889165ccbcc7043f8a26254d7c6464045923-1324406089"; 

            EyeemPhoto myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/75ed889165ccbcc7043f8a26254d7c6464045923-1324406089";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/75ed889165ccbcc7043f8a26254d7c6464045923-1324406089";

            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/80245e82107ec08f7deef62db78ce39d85a3cf51-1324401178";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/80245e82107ec08f7deef62db78ce39d85a3cf51-1324401178";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/1e17bbda9f9b56625148f511bd99d240228c26dd-1312573141";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/1e17bbda9f9b56625148f511bd99d240228c26dd-1312573141";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/78a3c34328482398469fe2220d2b00d326e7660c-1312454292";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/78a3c34328482398469fe2220d2b00d326e7660c-1312454292";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/1945d1a94773d775284d4e58cb25a2bcdd8f7777-1311598829";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/1945d1a94773d775284d4e58cb25a2bcdd8f7777-1311598829";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/cf25e8fb907281f2de9da16bc52e3a9b5332617d-1309639025";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/cf25e8fb907281f2de9da16bc52e3a9b5332617d-1309639025";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/a514fad8edc32a272c6196a4366d02659da480ab-1309282199";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/a514fad8edc32a272c6196a4366d02659da480ab-1309282199";
            myAlbum.photos.Add(myPhoto);

            result.Add(myAlbum);

            myAlbum = new EyeemAlbum();
            myAlbum.name = "faces";
            myAlbum.type = "venue";
            myAlbum.thumbUrl = "http://www.eyeem.com/thumb/sq/200/686a53d04e065f5808ec29f28c8ed9b3a507d83d-1320752178";
            myAlbum.totalPhotos = 34;

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/b51f1c9749e147fd33bf4b163efb5c9bbb157921-1329733402";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/b51f1c9749e147fd33bf4b163efb5c9bbb157921-1329733402";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/db709b2e7dd2b556273752d64f6f00497d869930-1325273886";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/db709b2e7dd2b556273752d64f6f00497d869930-1325273886";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/686a53d04e065f5808ec29f28c8ed9b3a507d83d-1320752178";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/686a53d04e065f5808ec29f28c8ed9b3a507d83d-1320752178";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/f0044779136508de23049d5e77179b58f22dab87-1318840852";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/f0044779136508de23049d5e77179b58f22dab87-1318840852";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/2af33ba3d3677f4880cf636480b4e079e5cb3fad-1317748047";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/2af33ba3d3677f4880cf636480b4e079e5cb3fad-1317748047";
            myAlbum.photos.Add(myPhoto);

            result.Add(myAlbum);

            myAlbum = new EyeemAlbum();
            myAlbum.name = "sunset";
            myAlbum.type = "venue";
            myAlbum.thumbUrl = "http://www.eyeem.com/thumb/sq/200/4be68660ff1594bdb76e79b26318233a5982d9c2-1329987417";
            myAlbum.totalPhotos = 392;

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/5479be982f9d3906921ecc81387bf0b039816dc6-1330036835";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/5479be982f9d3906921ecc81387bf0b039816dc6-1330036835";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/4be68660ff1594bdb76e79b26318233a5982d9c2-1329987417";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/4be68660ff1594bdb76e79b26318233a5982d9c2-1329987417";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/3b8ef19960493ea8aa0da351aef2dcbe00b90e6e-1329968315";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/3b8ef19960493ea8aa0da351aef2dcbe00b90e6e-1329968315";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/963c55ba776081611217e4919eaaf84c77116822-1329968200";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/963c55ba776081611217e4919eaaf84c77116822-1329968200";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/54c6f88d08141532f86dd7fc9c62d28e73e2083d-1329960883";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/54c6f88d08141532f86dd7fc9c62d28e73e2083d-1329960883";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/bb613b56f32e212f5c5f479f9946d373f37cff06-1329960671";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/bb613b56f32e212f5c5f479f9946d373f37cff06-1329960671";
            myAlbum.photos.Add(myPhoto);

            myPhoto = new EyeemPhoto();
            myPhoto.thumbUrl = "http://www.eyeem.com/thumb/h/100/fac2f945b960c9e673cc8bcd5eee23004cea85f3-1329862688";
            myPhoto.photoUrl = "http://www.eyeem.com/thumb/640/480/fac2f945b960c9e673cc8bcd5eee23004cea85f3-1329862688";
            myAlbum.photos.Add(myPhoto);

            result.Add(myAlbum);*/

            //...


            return null;

        }

        public static List<EyeemAlbum> parseOutput(string json)
        {
            bool flagPhoto = false;
            bool flagAlbum = false;

            List<EyeemAlbum> albums = new List<EyeemAlbum>();
            EyeemAlbum album = null;
            EyeemPhoto photo = null;

            string[] parts = json.Split('{');

            foreach (string elements in parts)
            {
                string[] attributes = elements.Split('"');
                if (containsString(attributes,"name"))
                {
                    //Photo Element
                    album = new EyeemAlbum();
                    flagAlbum = true;
                    flagPhoto = false;
                }
                else if (containsString(attributes, "width"))
                {
                    //Photo Element
                    photo = new EyeemPhoto();
                    flagPhoto = true;
                    flagAlbum = false;
                }

                for (int i = 0; i < attributes.Length; i++)
                {
                    if (flagAlbum)
                    {
                        //Parse Album
                    }
                    else if (flagPhoto)
                    {
                        //Parse Photo
                    }
                    else
                    {
                        //Nothing
                    }

                }
    
            }
            return albums;
        }

        public static bool containsString(string[] val, string stringToCheck)
        {
            String sep = "";
            bool result = String.Join(sep, val).Contains(stringToCheck);
            return result;
        }


    }
}
