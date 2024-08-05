using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using PlayGround.APIHandler;

namespace PlayGround
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand FocusUsernameCommand { get; }
        public ICommand FocusPasswordCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand CloseCommand { get; }


        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            DataContext = this;
            

        

            FocusUsernameCommand = new RelayCommand(FocusUsername);
            FocusPasswordCommand = new RelayCommand(FocusPassword);
            SubmitCommand = new RelayCommand(Submit);
            ClearCommand = new RelayCommand(Clear);
            CloseCommand = new RelayCommand(CloseWindow);
        }
        private void FocusUsername()
        {
            txtUser.Focus();
        }

        private void FocusPassword()
        {
            txtPassword.Focus();
        }

        private void Submit()
        {
            btnLogin_Click(null, null);
        }

        private void Clear()
        {
            txtUser.Clear();
            txtPassword.Clear();
        }

        private void CloseWindow()
        {
            Application.Current.Shutdown();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Password;

            var response = await RestHelper.PostLoginAuth(username,password);
            try
            {
                if (!string.IsNullOrEmpty(response) && response.Contains("token"))
                {
                    MessageBox.Show($"Successful Login: {response}");
                }
                else
                {
                    MessageBox.Show($"Login failed: {response}");
                    txtUser.Clear(); txtPassword.Clear();
                }
            }
            catch (Exception ex) {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Confirm Password reset.");

            // ResetPassword resetPassword = new ResetPassword();
            // resetPassword.Show();
            //this.Close();

            MessageBox.Show("Cool Way to reseting password...!");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

