using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace IMS.MainMenu
{
    public partial class OrderWindow : Window
    {
        // Define your SQL connection string here
        private string connectionString = "Your_Connection_String_Here";

        public OrderWindow()
        {
            InitializeComponent();
        }

        // Store the current order details
        private string currentOrderDetails;

        // Add Order button click handler
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string orderId = OrderIDTextBox.Text;
            string customerName = CustomerNameTextBox.Text;
            string productId = ProductIDTextBox.Text;
            string quantity = QuantityTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(customerName) ||
                string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Save the order details
            currentOrderDetails = $"Order ID: {orderId}\nCustomer Name: {customerName}\nProduct ID: {productId}\nQuantity: {quantity}";
            SavedOrderDetailsTextBlock.Text = currentOrderDetails;

            // Save the order to the database
            SaveOrderToDatabase(orderId, customerName, productId, quantity);

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Save Order to SQL Database
        private void SaveOrderToDatabase(string orderId, string customerName, string productId, string quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Orders (OrderID, CustomerName, ProductID, Quantity) VALUES (@OrderID, @CustomerName, @ProductID, @Quantity)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Update Order button click handler
        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentOrderDetails))
            {
                MessageBox.Show("No order has been saved yet to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string orderId = OrderIDTextBox.Text;
            string customerName = CustomerNameTextBox.Text;
            string productId = ProductIDTextBox.Text;
            string quantity = QuantityTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(customerName) ||
                string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the order in the database
            UpdateOrderInDatabase(orderId, customerName, productId, quantity);

            // Update the displayed details
            currentOrderDetails = $"Order ID: {orderId}\nCustomer Name: {customerName}\nProduct ID: {productId}\nQuantity: {quantity}";
            SavedOrderDetailsTextBlock.Text = currentOrderDetails;

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Update Order in SQL Database
        private void UpdateOrderInDatabase(string orderId, string customerName, string productId, string quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Orders SET CustomerName = @CustomerName, ProductID = @ProductID, Quantity = @Quantity WHERE OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", orderId);
                    command.Parameters.AddWithValue("@CustomerName", customerName);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Delete Order button click handler
        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentOrderDetails))
            {
                MessageBox.Show("No order has been saved yet to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string orderId = OrderIDTextBox.Text;

            // Delete the order from the database
            DeleteOrderFromDatabase(orderId);

            // Clear the displayed order details
            currentOrderDetails = string.Empty;
            SavedOrderDetailsTextBlock.Text = string.Empty;

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Delete Order from SQL Database
        private void DeleteOrderFromDatabase(string orderId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", orderId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Save Order button click handler
        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderButton_Click(sender, e); // Reuse AddOrder logic
        }

        // Clear input fields after saving
        private void ClearInputFields()
        {
            OrderIDTextBox.Clear();
            CustomerNameTextBox.Clear();
            ProductIDTextBox.Clear();
            QuantityTextBox.Clear();
        }
    }
}
