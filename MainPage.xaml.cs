using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExAgenda10DataboundMultiwindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool projecting = false;

        public MainPage()
        {
            this.InitializeComponent();

            Week w = Week.Instance;
            foreach (var i in w)
            {
                AgendaItemControl ctrl = new AgendaItemControl { DataContext = i };
                ctrl.Tapped += AgendaItem_Tapped;
                ctrl.RightTapped += AgendaItem_RightTapped;
                ctrl.Holding += AgendaItem_Holding;
                this.WeekControl.Children.Add(ctrl);
            }
                
        }

        private void AgendaItem_Holding(object sender, HoldingRoutedEventArgs e)
        {
            ShowRightclickFlyout((UIElement)sender, e.GetPosition((UIElement)sender));
        }

        public void AgendaItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigateToDetailPage((AgendaItem)((AgendaItemControl)sender).DataContext);
        }

        public void AgendaItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ShowRightclickFlyout((UIElement)sender, e.GetPosition((UIElement)sender));
        }

        private AgendaItem flyoutMenuContext;

        private void ShowRightclickFlyout(UIElement sender, Point offset)
        {
            MenuFlyout flyout = this.Resources["RightClickFlyout"] as MenuFlyout;
            flyoutMenuContext = (AgendaItem)((AgendaItemControl)sender).DataContext;

            flyout.ShowAt(sender, offset);
        }

        private void MenuFlyout_Closed(object sender, object e)
        {
            flyoutMenuContext = null;
        }

        private void MenuFlyoutItemDetails_Click(object sender, RoutedEventArgs e)
        {
            NavigateToDetailPage(flyoutMenuContext);
            flyoutMenuContext = null;
        }

        private void MenuFlyoutItemNewWindow_Click(object sender, RoutedEventArgs e)
        {
            ShowDetailPageInNewWindow(flyoutMenuContext);
            flyoutMenuContext = null;
        }

        private void Project_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavigateToDetailPage(AgendaItem itemContext)
        {
            this.Frame.Navigate(typeof(AgendaItemDetailPage), itemContext);
        }

        // Docu: https://msdn.microsoft.com/en-us/windows/uwp/layout/show-multiple-views
        private async void ShowDetailPageInNewWindow(AgendaItem itemContext)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new NavigationAwareFrame();
                frame.Navigate(typeof(AgendaItemDetailPage), itemContext);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }
    }
}
