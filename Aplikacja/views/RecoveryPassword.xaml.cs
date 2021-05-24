using Aplikacja.modele;
using Aplikacja.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja.views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class RecoveryPassword : Page
    {
        public RecoveryPassword()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter)) 
            {
                UsernameTextBox.Text = e.Parameter.ToString();
            }
        }
        private void ResetPassword(object sender, RoutedEventArgs e)
        {
            
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActiveButton();   
        }

        private void UsernameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            ActiveButton();
        }
        private void ActiveButton()
        {
            //TODO
            //ZROBIĆ NA SPRAWDZANIE WALIDACJI
            if (UsernameTextBox.Text.Length > 0)
            {
                PasswordResset.IsEnabled = true;
            }
            else
            {
                PasswordResset.IsEnabled = false;
            }
        }
    }
}
