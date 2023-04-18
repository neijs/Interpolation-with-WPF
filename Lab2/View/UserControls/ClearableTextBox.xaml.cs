using System.Windows;
using System.Windows.Controls;

namespace SpringLab2.View.UserControls
{
    public partial class ClearableTextBox : UserControl
    {
        public ClearableTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(ClearableTextBox));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private string placeholder;
        public string Placeholder
        {
            get { return placeholder; }
            set { 
                placeholder = value; 
                tbPlaceholder.Text = placeholder;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
                tbPlaceholder.Visibility = Visibility.Visible;
            else
                tbPlaceholder.Visibility = Visibility.Hidden;
        }
    }
}
