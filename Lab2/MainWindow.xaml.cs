using System.Windows;
using Microsoft.Win32;

namespace SpringLab2
{
    public partial class MainWindow : Window
    {
        ViewData viewData = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewData;
        }

        private void rdfc_btn_Click(object sender, RoutedEventArgs e)
        {
            viewData.ExecuteSplinesFC();
            rd_lb.ItemsSource = viewData.NodeValue;
            sd_lb.ItemsSource = viewData.SplineData!.spline;
            integ_tb.Text = viewData.SplineData.integValue.ToString("f3");
        }

        private void rdff_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.Filter = "JSON Files | *.json";
            fileDialog.Title = "Please pick a JSON file for RawData initialization";
            bool? success = fileDialog.ShowDialog();
            if (success == true) {
                string path = fileDialog.FileName;
                viewData.Load(path);
                viewData.ExecuteSplinesFF();
                rd_lb.ItemsSource = viewData.NodeValue;
                sd_lb.ItemsSource = viewData.SplineData!.spline;
                integ_tb.Text = viewData.SplineData.integValue.ToString();
            } else {
                MessageBox.Show("You didn't select anything...");
            }
        }
        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new();
            fileDialog.Filter = "JSON Files | *.json";
            fileDialog.Title = "Please pick a JSON file for saving your data";
            bool? success = fileDialog.ShowDialog();
            if (success == true) {
                string path = fileDialog.FileName;
                viewData.Save(path);
            } else {
                MessageBox.Show("You didn't select anything...");
            }  
        }
    }
}
