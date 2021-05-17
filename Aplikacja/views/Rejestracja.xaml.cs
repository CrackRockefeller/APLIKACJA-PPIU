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

using DataAccessLibrary;
using Aplikacja.modele;
using Aplikacja.Validators;
using FluentValidation.Results;
using System.ComponentModel;

namespace Aplikacja
{
    public sealed partial class Rejestracja : Page
    {
        public Rejestracja()
        {
            this.InitializeComponent();
        }

        private void btnRejestracja_Click(object sender, RoutedEventArgs e)
        {
            BindingList<string> errList = new BindingList<string>();

            Uzytkownicy uzytkownik = new Uzytkownicy();
            uzytkownik.email = email.Text;
            uzytkownik.imie = imie.Text;
            uzytkownik.haslo = haslo.Text;

            //Validate my data

            UzytkownikValidator validator = new UzytkownikValidator();

            ValidationResult results = validator.Validate(uzytkownik);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure faliure in results.Errors)
                {
                    errList.Add($" {faliure.ErrorMessage}");
                }
            } 

            Output.ItemsSource = errList;

            //tu dodaj do bazy
            //DataAccess.dodajUzytkownika(email.Text, imie.Text, haslo.Text);

        }
    }
}
