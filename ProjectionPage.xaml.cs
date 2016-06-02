using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ExAgenda10DataboundMultiwindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectionPage : Page
    {
        private MainPage presentingPage;

        public ProjectionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            presentingPage = (MainPage)e.Parameter;
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            SwapProjection();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            presentingPage.EndProjection();
        }

        private async void SwapProjection()
        {
            await ProjectionManager.SwapDisplaysForViewsAsync(ApplicationView.GetForCurrentView().Id, ((App)Application.Current).MainViewId);
        }
    }
}
