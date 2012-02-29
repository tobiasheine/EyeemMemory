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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EyeemMemory.Models;
using EyeemMemory.Control;
using System.IO.IsolatedStorage;
using EyeemMemory.Helper;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace EyeemMemory
{
    public partial class App : Application
    {

        public EyeemAlbum selectedAlbum { get; set; }
        public GameManager manager { get; set; }
        public List<EyeemHighscore> highscoreList { get; set; }

        public EyeemAlbum popularAlbum { get; set; }

        /// <summary>
        /// Bietet einen einfachen Zugriff auf den Stammframe der Phone-Anwendung.
        /// </summary>
        /// <returns>Der Stammframe der Phone-Anwendung.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        public bool canPlay { get;  set; }

        /// <summary>
        /// Konstruktor für das Application-Objekt.
        /// </summary>
        public App()
        {
            // Globaler Handler für nicht abgefangene Ausnahmen. 
            UnhandledException += Application_UnhandledException;

            // Silverlight-Standardinitialisierung
            InitializeComponent();

            // Phone-spezifische Initialisierung
            InitializePhoneApplication();

            // Während des Debuggens Profilerstellungsinformationen zur Grafikleistung anzeigen.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Zähler für die aktuelle Bildrate anzeigen.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Bereiche der Anwendung hervorheben, die mit jedem Bild neu gezeichnet werden.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Nicht produktiven Visualisierungsmodus für die Analyse aktivieren, 
                // in dem Bereiche einer Seite angezeigt werden, die mit einer Farbüberlagerung an die GPU übergeben wurden.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Deaktivieren Sie die Leerlauferkennung der Anwendung, indem Sie die UserIdleDetectionMode-Eigenschaft des
                // PhoneApplicationService-Objekts der Anwendung auf "Disabled" festlegen.
                // Vorsicht: Nur im Debugmodus verwenden. Eine Anwendung mit deaktivierter Benutzerleerlauferkennung wird weiterhin ausgeführt
                // und verbraucht auch dann Akkuenergie, wenn der Benutzer das Handy nicht verwendet.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            manager = new GameManager();
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("highscore"))
            {
                highscoreList = (List<EyeemHighscore>)settings["highscore"];
            }
            else
            {
                highscoreList = new List<EyeemHighscore>();
            }

            WebClient client = new WebClient();
            string strUri = JSONHelper.Search_Url + "Nature" + Secrets.Client_Url;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new System.Uri(strUri));

            

        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            EyeemRootObject root = new EyeemRootObject();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(root.GetType());
            root = ser.ReadObject(ms) as EyeemRootObject;
            ms.Close();

            if (root.albums.items.Capacity > 0)
            {
                popularAlbum = root.albums.items[0];
                var currentPage = RootFrame.Content as EyeemPanorama;

                currentPage.default_image_1.Source = new BitmapImage(new Uri(popularAlbum.photos.items[0].thumbUrl));
                currentPage.default_image_2.Source = new BitmapImage(new Uri(popularAlbum.photos.items[1].thumbUrl));
                currentPage.default_image_3.Source = new BitmapImage(new Uri(popularAlbum.photos.items[2].thumbUrl));
            }
            
        }

        // Code, der beim Starten der Anwendung ausgeführt werden soll (z. B. über "Start")
        // Dieser Code wird beim Reaktivieren der Anwendung nicht ausgeführt
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code, der ausgeführt werden soll, wenn die Anwendung aktiviert wird (in den Vordergrund gebracht wird)
        // Dieser Code wird beim ersten Starten der Anwendung nicht ausgeführt
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code, der ausgeführt werden soll, wenn die Anwendung deaktiviert wird (in den Hintergrund gebracht wird)
        // Dieser Code wird beim Schließen der Anwendung nicht ausgeführt
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code, der beim Schließen der Anwendung ausgeführt wird (z. B. wenn der Benutzer auf "Zurück" klickt)
        // Dieser Code wird beim Deaktivieren der Anwendung nicht ausgeführt
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code, der bei einem Navigationsfehler ausgeführt wird
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Navigationsfehler. Unterbrechen und Debugger öffnen
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code, der bei nicht behandelten Ausnahmen ausgeführt wird
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Eine nicht behandelte Ausnahme ist aufgetreten. Unterbrechen und Debugger öffnen
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Initialisierung der Phone-Anwendung

        // Doppelte Initialisierung vermeiden
        private bool phoneApplicationInitialized = false;

        // Fügen Sie keinen zusätzlichen Code zu dieser Methode hinzu
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Frame erstellen, aber noch nicht als RootVisual festlegen. Dadurch kann der Begrüßungsbildschirm
            // aktiv bleiben, bis die Anwendung bereit für das Rendern ist.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Navigationsfehler behandeln
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Sicherstellen, dass keine erneute Initialisierung erfolgt
            phoneApplicationInitialized = true;
        }

        // Fügen Sie keinen zusätzlichen Code zu dieser Methode hinzu
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Visuelle Stammkomponente festlegen, sodass die Anwendung gerendert werden kann
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Dieser Handler wird nicht mehr benötigt und kann entfernt werden
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}