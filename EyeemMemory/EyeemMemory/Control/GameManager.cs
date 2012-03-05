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
using EyeemMemory;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using EyeemMemory.Models;
using System.Linq;
using Coding4Fun.Phone.Controls;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.IO;
using System.Windows.Media.Imaging;

namespace EyeemMemory.Control
{
    public class GameManager
    {
        EyeemMemoryCard firstCard;
        EyeemMemoryCard secondCard;

        List<EyeemMemoryCard> wonCards = new List<EyeemMemoryCard>();

        private DispatcherTimer validationTimer = new DispatcherTimer();
        
        public int moves;
        private int click = 0;
        private int winMoves;

        private DateTime startDt;
        public DispatcherTimer gameTimer = new DispatcherTimer();
        
        private int gametimeSeconds = 0;
        private int gametimeMinutes = 0;

        public void setNextCard(EyeemMemoryCard myCard)
        {

            click++;
            if (click % 2 == 0) 
            {
                moves++;
            }

            if (moves == 0)
            {
                gameTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                gameTimer.Tick += new EventHandler(dt_Tick);

                //startDt = DateTime.Now;
                
                var currentPage = ((App)Application.Current).RootFrame.Content as EyeemGameField;

                startDt = DateTime.Now;
                gameTimer.Start();
                
            }

            myCard.FrontImage.Visibility = Visibility.Collapsed;
            
            if (firstCard == null)
            {
                firstCard = myCard;
            }
            else if (secondCard == null)
            {
                               
                var currentPage = ((App)Application.Current).RootFrame.Content as EyeemGameField;
                currentPage.Moves.Text = "" + moves;

                secondCard = myCard;
                App app = (App)Application.Current;
                app.canPlay = false;
                
                validationTimer.Interval = new TimeSpan(0, 0, 1);
                validationTimer.Tick += new EventHandler(timer_Tick);
                validationTimer.Start();

            }
        }

         

        void dt_Tick(object sender, EventArgs e)
        {
            var currentPage = ((App)Application.Current).RootFrame.Content as EyeemGameField;

            if (currentPage != null)
            {
                gametimeSeconds++;
                if (gametimeSeconds >= 60)
                {
                    gametimeSeconds = 0;
                    gametimeMinutes++;
                }

                if (gametimeMinutes > 0)
                {
                    currentPage.txtMin.Text = "" + gametimeMinutes;
                }

                currentPage.txtSec.Text = "" + gametimeSeconds;

                if (currentPage.txtMin.Text.Length == 1) currentPage.txtMin.Text = "0" + gametimeMinutes;
                if (currentPage.txtSec.Text.Length == 1) currentPage.txtSec.Text = "0" + gametimeSeconds;
            }
        }

        public GameManager() {
            moves = 0;
            click = 0;
            wonCards = new List<EyeemMemoryCard>();
            winMoves = 0;
            validationTimer = new DispatcherTimer();
            gameTimer = new DispatcherTimer();
            gametimeSeconds = 0;
            gametimeMinutes = 0;
        }

        public void reset()
        {
            moves = 0;
            click = 0;
            wonCards = new List<EyeemMemoryCard>();
            winMoves = 0;
            validationTimer = new DispatcherTimer();
            gameTimer = new DispatcherTimer();
            gametimeSeconds = 0;
            gametimeMinutes = 0;
        }

        

        void timer_Tick(object sender, EventArgs e)
        {
            App app = (App)Application.Current;
            checkCards();
            validationTimer.Stop();
            app.canPlay = true;
        }

        private int compareMoves(EyeemHighscore first, EyeemHighscore second)
        {
            if (first == null)
            {
                return -1;
            }

            if (first.moves > second.moves)
                return 1;
            else
                return -1;
        }

        private void checkCards(){

            if (firstCard != null && secondCard != null)
            {
                bool result = firstCard.myPhoto.id == secondCard.myPhoto.id;

                if (result)
                {
                    winMoves++;

                    Stream stream = TitleContainer.OpenStream("sounds/yes/"+RandomNumber(1,10)+".wav");
                    SoundEffect effect = SoundEffect.FromStream(stream);
                    FrameworkDispatcher.Update();
                    effect.Play();

                    wonCards.Add(firstCard);
                    wonCards.Add(secondCard);
                    firstCard.canBeChanged = false;
                    secondCard.canBeChanged = false;
                    
                    var photoBox = new MessagePrompt
                    {
                        Title = "Great Move!",
                        Body = new Image { Stretch=Stretch.Uniform, Source = new BitmapImage(new Uri(firstCard.myPhoto.photoUrl)) },
                        IsAppBarVisible = true
                    };
                    gameTimer.Stop();

                    photoBox.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(photoBox_Completed);
                    photoBox.Show();


                    firstCard = null;
                    secondCard = null;

                    if (winMoves == 2)
                    {
                        gameTimer.Stop();
                        //Save albumName + moves in Highscore
                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        //List<EyeemHighscore> highscoreList;
                        App app = (App)Application.Current;

                        EyeemHighscore newHighscore = new EyeemHighscore();
                        newHighscore.albumName = app.selectedAlbum.name;
                        newHighscore.moves = moves;

                        app.highscoreList.Add(newHighscore);
                        //app.highscoreList.Sort((x, y) => (compareMoves(x, y)));

                        List<EyeemHighscore> SortedList = app.highscoreList.OrderBy(o => o.moves).ToList();
                        app.highscoreList = SortedList;

                        if (app.highscoreList.Capacity > 10)
                        {
                            app.highscoreList = app.highscoreList.GetRange(0, 9);
                            
                        }

                        settings["highscore"] = app.highscoreList;
                        settings.Save();

                        var messagePrompt = new MessagePrompt
                        {
                            Title = "Congratulations",
                            Body = new TextBlock { Text = "You won! " },
                            IsAppBarVisible = true,
                            IsCancelVisible = false
                        };
                        messagePrompt.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(messagePrompt_Completed);
                        messagePrompt.Show();

                    }
                }
                else
                {
                    Stream stream = TitleContainer.OpenStream("sounds/no/"+RandomNumber(1,10)+".wav");
                    SoundEffect effect = SoundEffect.FromStream(stream);
                    FrameworkDispatcher.Update();
                    effect.Play();

                    firstCard.FrontImage.Visibility = Visibility.Visible;
                    secondCard.FrontImage.Visibility = Visibility.Visible;
                    firstCard = null;
                    secondCard = null;

                }
            }
            
        }

        void photoBox_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            gameTimer.Start();
        }

        void messagePrompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult.Equals(Coding4Fun.Phone.Controls.PopUpResult.Ok))
            {
                App app = (App)Application.Current;
                var currentPage = app.RootFrame.Content as EyeemGameField;
                moves = 0;
                click = 0;
                gametimeMinutes = 0;
                gametimeMinutes = 0;
                wonCards = new List<EyeemMemoryCard>();
                winMoves = 0;
                currentPage.goBack();
            }
        }

        private string RandomNumber(int min, int max)
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
