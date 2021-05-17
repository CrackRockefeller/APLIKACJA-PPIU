﻿using System;
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

namespace Aplikacja
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO walidacja textboxow logowania
            Frame.Navigate(typeof(AfterLogin));
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Rejestracja));
        }
        
    }
}