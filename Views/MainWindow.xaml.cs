// MIT License
// Copyright (c) 2025 Denny Sulyvan Peyerl
//This code is free to use, modify and distribute according to the MIT license.

using BuddyProductivityAssistant.Models;
using BuddyProductivityAssistant.Views.Helpers;
using System.Windows;
using System.Windows.Threading;

namespace BuddyProductivityAssistant
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timerForStatus;
        private DispatcherTimer _timerForCompliments;
        
        private int timerValue = 30;

        public MainWindow()
        {
            InitializeComponent();
            HomeMenu();
        }
        //Handles the tatus update every tick in the timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            Dog.Instance.UpdateStatus();

            if (Dog.Instance.Happinesss <= 40)
            {
                NotificationHelper.Notifications("Buddy", "Hey, preciso de um carinho.");
                timerValue -= 5;

                if (timerValue <= 5) timerValue = 1;

                //Decreases the timer interval when user not interacting with the buddy enougth
                _timerForStatus.Stop();
                _timerForStatus.Interval = TimeSpan.FromSeconds(timerValue);
                _timerForStatus.Start();

                BuddyLogic.Bark(TheBuddy);
            }
        }
 
       
        private async void PetBuddy(object sender, RoutedEventArgs e)
        {
            await BuddyLogic.Bark(TheBuddy);
            NotificationHelper.Notifications("Buddy", "Obrigado!");
            Dog.Instance.Pet();
        }
        
        //Handles the first screen when user launches the application
        private async void HomeMenu()
        {
            Title.Visibility = Visibility.Visible;
            SubTitle.Visibility = Visibility.Visible;
            HomeButton.Visibility = Visibility.Visible;

            await ViewHelpers.WaitForHomeButtonClicked(HomeButton);

            await ViewHelpers.FadeAsync(1, 0, new UIElement[] { Title, SubTitle, HomeButton }, 1.0);

            Title.Visibility = Visibility.Hidden;
            SubTitle.Visibility = Visibility.Hidden;
            HomeButton.Visibility = Visibility.Hidden;
            MainMenu();

        }
        //Handle the elements visibility and timers when the Main Window is shown
        private async void MainMenu()
        {
            TheBuddy.Visibility = Visibility.Visible;
            Desk.Visibility = Visibility.Visible;
            Background.Opacity = 1;
            await ViewHelpers.FadeAsync(0, 1, new UIElement[] { TheBuddy, Desk}, 1.0);
            HitBoxHead.Visibility = Visibility.Visible;

            _timerForStatus = new DispatcherTimer();
            _timerForStatus.Interval = TimeSpan.FromSeconds(timerValue);
            _timerForStatus.Tick += Timer_Tick;
            _timerForStatus.Start();

            _timerForCompliments = new DispatcherTimer();
            _timerForCompliments.Interval = TimeSpan.FromMinutes(1);
            _timerForCompliments.Tick += BuddyLogic.Compliments;
            _timerForCompliments.Start();
        }
    }
}