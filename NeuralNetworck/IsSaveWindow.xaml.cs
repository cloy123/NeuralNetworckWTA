using System.Windows;

namespace NeuralNetworck
{
    public partial class IsSaveWindow : Window
    {

        bool IsOk;
        public IsSaveWindow()
        {
            InitializeComponent();
            Symbol.Items.Add("0");
            Symbol.Items.Add("1");
            Symbol.Items.Add("2");
            Symbol.Items.Add("3");
            Symbol.Items.Add("4");
            Symbol.Items.Add("5");
            Symbol.Items.Add("6");
            Symbol.Items.Add("7");
            Symbol.Items.Add("8");
            Symbol.Items.Add("9");
            Symbol.SelectedIndex = 0;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            IsOk = true;
            this.Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = IsOk;
        }
    }
}
