using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace IMS.MainMenu
{
    public partial class CategoryWindow : Window
    {
        // Define your SQL connection string here
        private string connectionString = "Your_Connection_String_Here";

        public CategoryWindow()
        {
            InitializeComponent();
        }

        // Store the current category details
        private string currentCategoryDetails;

        // Text Changed event handler for Category textboxes
        private void CategoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle text changes if needed (e.g., enable/disable buttons)
        }

        // Add Category button click handler
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryId = CategoryIDTextBox.Text;
            string categoryName = CategoryNameTextBox.Text;
            string description = DescriptionTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(categoryId) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Save the category details
            currentCategoryDetails = $"Category ID: {categoryId}\nCategory Name: {categoryName}\nDescription: {description}";

            // Display saved category details
            SavedCategoryDetailsTextBlock.Text = currentCategoryDetails;

            // Save the category to the database
            SaveCategoryToDatabase(categoryId, categoryName, description);

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Save Category to SQL Database
        private void SaveCategoryToDatabase(string categoryId, string categoryName, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Categories (CategoryID, CategoryName, Description) VALUES (@CategoryID, @CategoryName, @Description)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CategoryID", categoryId);
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Update Category button click handler
        private void UpdateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentCategoryDetails))
            {
                MessageBox.Show("No category has been saved yet to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string categoryId = CategoryIDTextBox.Text;
            string categoryName = CategoryNameTextBox.Text;
            string description = DescriptionTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(categoryId) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the category in the database
            UpdateCategoryInDatabase(categoryId, categoryName, description);

            // Update the displayed details
            currentCategoryDetails = $"Category ID: {categoryId}\nCategory Name: {categoryName}\nDescription: {description}";
            SavedCategoryDetailsTextBlock.Text = currentCategoryDetails;

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Update Category in SQL Database
        private void UpdateCategoryInDatabase(string categoryId, string categoryName, string description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Categories SET CategoryName = @CategoryName, Description = @Description WHERE CategoryID = @CategoryID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CategoryID", categoryId);
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Delete Category button click handler
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentCategoryDetails))
            {
                MessageBox.Show("No category has been saved yet to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string categoryId = CategoryIDTextBox.Text;

            // Delete the category from the database
            DeleteCategoryFromDatabase(categoryId);

            // Clear the displayed category details
            currentCategoryDetails = string.Empty;
            SavedCategoryDetailsTextBlock.Text = string.Empty;

            // Optionally, clear the input fields
            ClearInputFields();
        }

        // Delete Category from SQL Database
        private void DeleteCategoryFromDatabase(string categoryId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CategoryID", categoryId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Save Category button click handler
        private void SaveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryButton_Click(sender, e); // Reuse AddCategory logic
        }

        // Clear input fields after saving
        private void ClearInputFields()
        {
            CategoryIDTextBox.Clear();
            CategoryNameTextBox.Clear();
            DescriptionTextBox.Clear();
        }
    }
}
