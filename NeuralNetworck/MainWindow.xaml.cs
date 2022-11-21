using System;
using System.Windows;
using System.Windows.Controls;

namespace NeuralNetworck
{



    public partial class MainWindow : Window
    {
        private NeiroWeb nw;
        private int[,] arr;
        private bool IsTraining;
        public static bool IsOk;

        public MainWindow()
        {
            InitializeComponent();
            IsTraining = true;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NeiroGraphUtils.ClearImage(InkCanvas);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            nw.SaveState();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NeiroGraphUtils.ClearImage(InkCanvas);
            nw = new NeiroWeb();
            Canvas.Width = Canvas.ActualWidth;
            Canvas.Height = Canvas.ActualHeight;
            InkCanvas.Width = Canvas.Width;
            InkCanvas.Height = Canvas.Height;
        }

        private void bIsTraining_Click(object sender, RoutedEventArgs e)
        {
            IsTraining = !IsTraining;
            bIsTraining.Content = IsTraining ? "Не обучать" : "Обучать";
        }

        private void recognize_Click(object sender, RoutedEventArgs e)
        {
            int[,] clipArr = NeiroGraphUtils.CutImageToArray(InkCanvas, Convert.ToInt32(InkCanvas.Width), Convert.ToInt32(InkCanvas.Height));
            if (clipArr == null) return;
            arr = NeiroGraphUtils.LeadArray(clipArr, new int[NeiroWeb.neironInArrayWidth, NeiroWeb.neironInArrayHeight]);
            string s = nw.CheckLitera(arr);
            if (s == null)
            {
                s = "null";
            }
            resultTextBlock.Text = s;
            if (IsTraining)
            {
                MessageBoxResult askResult = MessageBox.Show("Это " + s + " ?", "", MessageBoxButton.YesNo);
                if (askResult == MessageBoxResult.Yes)
                { 
                    if(MessageBox.Show("Сохранить?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        nw.SetTraining(s, arr);
                    }
                }
                else
                {
                    IsSaveWindow save = new IsSaveWindow();
                    save.ShowDialog();
                    if (Convert.ToBoolean(save.DialogResult))
                    {
                        s = save.Symbol.SelectedItem.ToString();
                        MessageBox.Show(s);
                        nw.SetTraining(s, arr);
                        resultTextBlock.Text = s;
                    }
                }
            }
        }
    }
}
