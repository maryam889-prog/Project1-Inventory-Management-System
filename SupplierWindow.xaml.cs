using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace IMS.MainMenu
{
    public partial class SupplierWindow : Window
    {
        // Define your SQL connection string
        private string connectionString = "Your_Connection_String_Here";

        public SupplierWindow()
        {
            InitializeComponent();
        }

        // Event handler for Add Supplier button click
        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            // Get input values from textboxes
            string supplierName = SupplierNameTextBox.Text;
            string contactName = ContactNameTextBox.Text;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;

            // Validate input fields
            if (string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(contactName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Save the supplier to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Suppliers (SupplierName, ContactName, Email, Address) VALUES (@SupplierName, @ContactName, @Email, @Address)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierName", supplierName);
                    command.Parameters.AddWithValue("@ContactName", contactName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Optionally display the saved details
                string savedDetails = $"Supplier Name: {supplierName}\nContact Name: {contactName}\nEmail: {email}\nAddress: {address}";
                DisplaySupplierDetails(savedDetails);

                // Clear the input fields
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Update Supplier button click
        private void UpdateSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string supplierName = SupplierNameTextBox.Text;
            string contactName = ContactNameTextBox.Text;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;

            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Supplier Name must be provided to update the supplier.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Update the supplier in the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Suppliers SET ContactName = @ContactName, Email = @Email, Address = @Address WHERE SupplierName = @SupplierName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierName", supplierName);
                    command.Parameters.AddWithValue("@ContactName", contactName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Address", address);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Optionally display the updated details
                        string updatedDetails = $"Supplier Name: {supplierName}\nContact Name: {contactName}\nEmail: {email}\nAddress: {address}";
                        DisplaySupplierDetails(updatedDetails);

                        // Clear the input fields
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("No supplier found with the specified name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for Delete Supplier button click
        private void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string supplierName = SupplierNameTextBox.Text;

            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Supplier Name must be provided to delete the supplier.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Delete the supplier from the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Suppliers WHERE SupplierName = @SupplierName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierName", supplierName);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Clear the displayed details and input fields
                        ClearInputFields();
                        SavedSupplierDetailsStackPanel.Children.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No supplier found with the specified name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to display supplier details in the saved details area
        private void DisplaySupplierDetails(string details)
        {
            // Ensure that the SavedSupplierDetailsStackPanel is cleared before adding new details
            SavedSupplierDetailsStackPanel.Children.Clear();

            // Create a new TextBlock to display the saved details
            TextBlock savedDetailsTextBlock = new TextBlock
            {
                Text = details,
                Foreground = (System.Windows.Media.Brush)FindResource("TextColor"), // Ensure TextColor exists in your resources
                FontSize = 16
            };

            // Add the TextBlock to the SavedSupplierDetailsStackPanel
            SavedSupplierDetailsStackPanel.Children.Add(savedDetailsTextBlock);
        }

        // Clear input fields after saving
        private void ClearInputFields()
        {
            SupplierNameTextBox.Clear();
            ContactNameTextBox.Clear();
            EmailTextBox.Clear();
            AddressTextBox.Clear();
        }
    }
}
