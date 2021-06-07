using Aplikacja.models;
using DataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PanelUzytkownika : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


        public PanelUzytkownika()
        {
            this.InitializeComponent();
            test.Text = localSettings.Values["loggedUser"].ToString();

            Output.ItemsSource = DataAccess.GetData(test.Text);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }

        private void LoadChartContents()
        {
            List<Wydatki> elementyDiagramu = new List<Wydatki>();
            List<string> listaKwota = new List<string>();
            List<string> listaTyp = new List<string>();
            foreach (var item in DataAccess.GetDataForDiagramsKwota(test.Text))
            {
                listaKwota.Add(item);
            }
            foreach (var item in DataAccess.GetDataForDiagramsTyp(test.Text))
            {
                listaTyp.Add(item);
            }

            for (int i = 0; i < listaKwota.Count; i++)
            {
                elementyDiagramu.Add(new Wydatki() { typ_wydatku = listaTyp[i], kwota = listaKwota[i] });
            } 
            (PieChart.Series[0] as PieSeries).ItemsSource = elementyDiagramu;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DodawanieWpisu));
        }

        private void btnUstawienia_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ustawienia));
        }

        private void btnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            string kliknietyEl = Output.SelectedItem.ToString();

            Regex rx = new Regex(@"^([\S]+)");
            Match match = rx.Match(kliknietyEl);
            String match2 = Convert.ToString(match);
            int idmatch = Convert.ToInt32(match2);
            DataAccess.usunWpisUzytkownika(idmatch);
            Output.ItemsSource = DataAccess.GetData(test.Text);
            LoadChartContents();
            btnUsun.Visibility = Visibility.Collapsed;
            btnModyfikuj.Visibility = Visibility.Collapsed;
            txtModKwota.Visibility = Visibility.Collapsed;
            txtUwaga.Visibility = Visibility.Collapsed;
        }

        private void Output_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnUsun.Visibility = Visibility.Visible;
            btnModyfikuj.Visibility = Visibility.Visible;
            txtModKwota.Visibility = Visibility.Visible;
        }

        private void btnModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            string kliknietyEl = Output.SelectedItem.ToString();

            Regex rx = new Regex(@"^([\S]+)");
            Match match = rx.Match(kliknietyEl);
            String match2 = Convert.ToString(match);
            int idmatch = Convert.ToInt32(match2);
            String kwotaWpisana = txtModKwota.Text.Replace(",", ".");
            if (kwotaWpisana != "")
            {
                double kwota = Convert.ToDouble(kwotaWpisana);
                DataAccess.updateWpisUzytkownika(idmatch, kwota);
                Output.ItemsSource = DataAccess.GetData(test.Text);
                LoadChartContents();
                btnModyfikuj.Visibility = Visibility.Collapsed;
                btnUsun.Visibility = Visibility.Collapsed;
                txtModKwota.Visibility = Visibility.Collapsed;
                txtUwaga.Text = "";
                txtModKwota.Text = "";
            }
            else
            {
                txtUwaga.Visibility = Visibility.Visible;
                txtUwaga.Text = "Wprowadz jakas kwote!";
            }


        }

        private void txtModKwota_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            foreach (char przec in txtModKwota.Text)
                if (przec == ',' || przec == '.')
                {
                    count++;
                    if (count > 1)
                    {
                        txtModKwota.Text = "";
                    }
                }


            double kwotaWpisana = 0;
            try
            {
                kwotaWpisana = Convert.ToDouble(txtModKwota.Text);
            }
            catch
            {
                txtModKwota.Text = "";
            }
        }
    }
}
