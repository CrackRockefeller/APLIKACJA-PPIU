using DataAccessLibrary;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aplikacja.views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class Ustawienia : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public Ustawienia()
        {
            this.InitializeComponent();
            aktEmail.Text = localSettings.Values["loggedUser"].ToString();
        }

        private void btnZmienEmail(object sender, RoutedEventArgs e)
        {
            int idUzytkownika = Convert.ToInt32(DataAccess.sprawdzUzytkownika(aktEmail.Text));
            DataAccess.updateEmailUzytkownika(txtZmienEmail.Text, idUzytkownika);

        }

        private void btnCofnij(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
