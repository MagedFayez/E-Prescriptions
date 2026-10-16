using System;
using System.Windows;
using ePrescriptions.App.ViewModels;
using ePrescriptions.Core.Helpers;

namespace ePrescriptions.App.Views
{
    public partial class StandaloneWindow : Window
    {
        public ViewModelBase CurrentViewModel { get; }
        public string WindowTitle { get; }

        public StandaloneWindow(ViewModelBase viewModel, string title)
        {
            CurrentViewModel = viewModel;
            WindowTitle = title;
            Title = $"e_prescriptions - {title}";

            InitializeComponent();
            DataContext = this;

            Loaded += (s, e) =>
            {
                try
                {
                    CurrentViewModel.OnNavigatedTo();
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex, "StandaloneWindow.Loaded");
                }
            };

            Closed += (s, e) =>
            {
                try
                {
                    CurrentViewModel.OnNavigatedFrom();
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex, "StandaloneWindow.Closed");
                }
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
