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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using DataAccessLibrary;
using System.ComponentModel.DataAnnotations;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Rejestracja : Page
    {
        public Rejestracja()
        {
            this.InitializeComponent();
        }

        private void btnRejestracja_Click(object sender, RoutedEventArgs e)
        {
            EmailAddressAttribute emailCheck = new EmailAddressAttribute();
            if (emailCheck.IsValid(email.Text))
            {
                {
                    if (DataAccess.sprawdzUzytkownika(email.Text))
                    {
                        if (haslo.Password == haslo2.Password)
                        {
                            DataAccess.dodajUzytkownika(email.Text, imie.Text, haslo.Password);
                            ErrorMessage.Text = "Zarejestrowano!";
                        }
                        else
                        {
                            ErrorMessage.Text = "Hasla nie sa sobie rowne!";
                        }
                    }
                    else
                    {
                        ErrorMessage.Text = "taki adres juz istnieje!";
                    }
                }
            }
            else
            {
                ErrorMessage.Text = "bledny adres email!";
            }

            //TODO  zrobic walidację textboxow
        }

        private void btnWroc_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
