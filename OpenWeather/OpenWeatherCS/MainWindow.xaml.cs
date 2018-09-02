using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;
using GMap.NET;
using OpenWeatherCS.ViewModels;
using System.Windows.Media;
using System;
using OpenWeatherCS.Utils;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using OpenWeatherCS.Models;
using System.Collections.Generic;
using System.Linq;

namespace OpenWeatherCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void eilat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LocationTextBox.Text = "Eilat";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);
        }

        private void negev_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Beer Sheva";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);

        }

        private void jerusalem_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Jerusalem";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);

        }

        private void Haifa_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Haifa";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);

        }

        private void deadsea_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Arad";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);

        }

        private void TelAviv_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Tel Aviv";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);
        }

        private void golan_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Katzrin";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);

        }

        private void galil_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "akko";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);
        }

        private void yosh_Click(object sender, RoutedEventArgs e)
        {
            LocationTextBox.Text = "Gaza";
            LocationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            LocationTextBox.Focus();

            KeyEventArgs ke = new KeyEventArgs(
                Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource,
                0,
                Key.Enter)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };

            InputManager.Current.ProcessInput(ke);
        }

    }
}
