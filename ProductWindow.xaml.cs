using System;
using System.Data.SqlClient;
using System.Windows;

namespace IMS.MainMenu
{
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to add the product to the database
            string productId = ProductIDTextBox.Text;
            string name = NameTextBox.Text;
            string sku = SKUTextBox.Text;
            string category = CategoryTextBox.Text;
            string quantity = QuantityTextBox.Text;
            string unitPrice = UnitPriceTextBox.Text;

            // Insert into database logic
            using (SqlConnection connection = new SqlConnection("your_connection_string"))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Products (ProductID, Name, SKU, Category, Quantity, UnitPrice) VALUES (@ProductID, @Name, @SKU, @Category, @Quantity, @UnitPrice)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@SKU", sku);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@UnitPrice", unitPrice);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Product added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void SaveProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to save all products
            MessageBox.Show("All Products Saved.");
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to update the product in the database
            MessageBox.Show("Product Updated.");
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to delete the product from the database
            MessageBox.Show("Product Deleted.");
        }

        // You can add additional methods for fetching products, or other database-related operations as needed
    }
}
