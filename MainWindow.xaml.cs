using System.Windows;

namespace IMS.MainMenu
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Button event handlers for navigation (to open different windows)
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();  // Close the current window
        }

        private void ShowCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
            this.Close();  // Close the current window
        }

        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            this.Close();  // Close the current window
        }

        private void ShowSuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            SupplierWindow supplierWindow = new SupplierWindow();
            supplierWindow.Show();
            this.Close();  // Close the current window
        }

        // Exit button event to show exit confirmation
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitWindow exitWindow = new ExitWindow();
            exitWindow.ShowDialog();  // Show exit confirmation window
        }
    }
}
