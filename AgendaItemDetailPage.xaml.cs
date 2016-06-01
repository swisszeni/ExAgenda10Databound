using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ExAgenda10DataboundMultiwindow
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class AgendaItemDetailPage : Page
    {
        public AgendaItemDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = e.Parameter;
            ToggleCloseButtonVisibility();
        }

        private void ToggleCloseButtonVisibility()
        {
            CloseViewButton.Visibility = CoreApplication.GetCurrentView().IsMain ? Visibility.Collapsed : Visibility.Visible;
            SwitchCloseViewButton.Visibility = CoreApplication.GetCurrentView().IsMain ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Time_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AgendaItemTimeDetailPage), DataContext);
        }

        private void CloseViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CoreApplication.GetCurrentView().IsMain)
            {
                Window.Current.Close();
            }
        }

        private async void SwitchCloseViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CoreApplication.GetCurrentView().IsMain)
            {
                await ApplicationViewSwitcher.SwitchAsync(((App)Application.Current).MainViewId, ApplicationView.GetForCurrentView().Id, ApplicationViewSwitchingOptions.ConsolidateViews);
            }
        }
    }
}
