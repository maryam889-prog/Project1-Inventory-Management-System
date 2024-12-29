using System.Windows;

namespace IMS.MainMenu
{
    public partial class ExitWindow : Window
    {
        public ExitWindow()
        {
            InitializeComponent();
        }

      
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }

       
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
