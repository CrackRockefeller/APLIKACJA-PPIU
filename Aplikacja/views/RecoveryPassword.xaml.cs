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
using DataAccessLibrary;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja.views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class RecoveryPassword : Page
    {
        private bool wyslano = false;
        public static Random rnd = new Random();
        int a = rnd.Next(0, 10000);
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
            string czyjest;
            czyjest = DataAccess.checkEmail(UsernameTextBox.Text.ToString())[0];
            if (!StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = true;
                grid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (czyjest == "1")
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("uwpApplication", "uwpapplication.uwpapplication@interia.pl"));
                message.To.Add(new MailboxAddress("", UsernameTextBox.Text.ToString()));
                message.Subject = "Kod odzyskiwania hasła konto UWP; Aplikacja finansowa";

                string txt = @"Odzyskiwanie hasła,<br>
                               <p>Wpisz ten kod do swojej aplikacji aby otrzymać nowe hasło:</p><p>" + a.ToString() + @"</p><br>
                               <p>-- UWPApplication</p>";

                message.Body = new TextPart("Html")
                {
                    Text = txt
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("poczta.interia.pl", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("uwpapplication.uwpapplication@interia.pl", "1234567Mm.");
                    Debug.WriteLine("The mail has been sent successfully !!");
                    client.Send(message);
                    client.Disconnect(true);

                    wyslano = true;
                }

            }
            

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
            
            if (UsernameTextBox.Text.Length > 0)
            {
                PasswordResset.IsEnabled = true;
            }
            else
            {
                PasswordResset.IsEnabled = false;
            }
        }

        private void potwierdz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cofnij_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
