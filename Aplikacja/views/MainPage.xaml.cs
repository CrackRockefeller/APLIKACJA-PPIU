using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Microsoft.Data.Sqlite;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLibrary;
using System.Diagnostics;
using System.ComponentModel;
using Aplikacja.modele;
using Aplikacja.Validators;
using FluentValidation.Results;
using Aplikacja.views;

namespace Aplikacja
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            string first = DataAccess.checkUser(userName, password)[0];

            BindingList<string> errList = new BindingList<string>();

            Uzytkownicy uzytkownik = new Uzytkownicy();
            uzytkownik.email = userName;
            uzytkownik.haslo = password;

            //Validate my data
            UzytkownikLoginValidator validator = new UzytkownikLoginValidator();
            ValidationResult results = validator.Validate(uzytkownik);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure faliure in results.Errors)
                {
                    errList.Add($" {faliure.ErrorMessage}");
                }
            }

            Output.ItemsSource = errList;

            if (errList.Count == 0)
            {
                Debug.WriteLine("userCheck: " + first);
                if (first == "1")
                {
                    localSettings.Values["loggedUser"] = userName;
                    Debug.WriteLine("localSetting loggedUser:" + localSettings.Values["loggedUser"]);
                    Frame.Navigate(typeof(PanelUzytkownika));
                }
                else
                {
                    errList.Add("Email lub hasło niepoprawne!");
                }
            }
            //Frame.Navigate(typeof(AfterLogin));
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Rejestracja));
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                string userName = UsernameTextBox.Text;
                string password = PasswordTextBox.Password;

                string first = DataAccess.checkUser(userName, password)[0];

                BindingList<string> errList = new BindingList<string>();

                Uzytkownicy uzytkownik = new Uzytkownicy();
                uzytkownik.email = userName;
                uzytkownik.haslo = password;

                //Validate my data
                UzytkownikLoginValidator validator = new UzytkownikLoginValidator();
                ValidationResult results = validator.Validate(uzytkownik);

                if (results.IsValid == false)
                {
                    foreach (ValidationFailure faliure in results.Errors)
                    {
                        errList.Add($" {faliure.ErrorMessage}");
                    }
                }

                Output.ItemsSource = errList;

                if (errList.Count == 0)
                {
                    Debug.WriteLine("userCheck: " + first);
                    if (first == "1")
                    {
                        localSettings.Values["loggedUser"] = userName;
                        Debug.WriteLine("localSetting loggedUser:" + localSettings.Values["loggedUser"]);
                        Frame.Navigate(typeof(PanelUzytkownika));
                    }
                    else
                    {
                        errList.Add("Email lub hasło niepoprawne!");
                    }
                }
            }
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                PasswordTextBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordTextBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }
    }
}
