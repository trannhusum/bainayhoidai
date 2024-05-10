using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace bainayhoidai
{
    internal class Program
    {
        class Product
        {
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string Manufacturer { get; set; }
            public double Price { get; set; }
            public string OtherDescription { get; set; }
        }

        class Programs
        {
            static List<Product> products = new List<Product>();
            static string filePath = "products.dat";

            static void Main(string[] args)
            {
                LoadProducts();

                while (true)
                {
                    Console.WriteLine("1. Thêm sản phẩm");
                    Console.WriteLine("2. Hiển thị danh sách sản phẩm");
                    Console.WriteLine("3. Tìm kiếm sản phẩm");
                    Console.WriteLine("4. Lưu và thoát");

                    Console.Write("Chọn một chức năng: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            DisplayProducts();
                            break;
                        case "3":
                            SearchProduct();
                            break;
                        case "4":
                            SaveProducts();
                            return;
                        default:
                            Console.WriteLine("Chức năng không hợp lệ.");
                            break;
                    }
                }
                Console.ReadKey();
            }

            static void AddProduct()
            {
                Product product = new Product();

                Console.Write("Nhập mã sản phẩm: ");
                product.ProductCode = Console.ReadLine();

                Console.Write("Nhập tên sản phẩm: ");
                product.ProductName = Console.ReadLine();

                Console.Write("Nhập hãng sản xuất: ");
                product.Manufacturer = Console.ReadLine();

                Console.Write("Nhập giá sản phẩm: ");
                product.Price = double.Parse(Console.ReadLine());

                Console.Write("Nhập các mô tả khác: ");
                product.OtherDescription = Console.ReadLine();

                products.Add(product);

                Console.WriteLine("Sản phẩm đã được thêm thành công.");
            }

            static void DisplayProducts()
            {
                Console.WriteLine("Danh sách sản phẩm:");
                foreach (var product in products)
                {
                    Console.WriteLine($"Mã sản phẩm: {product.ProductCode}");
                    Console.WriteLine($"Tên sản phẩm: {product.ProductName}");
                    Console.WriteLine($"Hãng sản xuất: {product.Manufacturer}");
                    Console.WriteLine($"Giá sản phẩm: {product.Price}");
                    Console.WriteLine($"Mô tả khác: {product.OtherDescription}");
                    Console.WriteLine();
                }
            }

            static void SearchProduct()
            {
                Console.Write("Nhập mã sản phẩm cần tìm: ");
                string searchCode = Console.ReadLine();

                Product foundProduct = products.Find(p => p.ProductCode == searchCode);
                if (foundProduct != null)
                {
                    Console.WriteLine("Thông tin sản phẩm:");
                    Console.WriteLine($"Mã sản phẩm: {foundProduct.ProductCode}");
                    Console.WriteLine($"Tên sản phẩm: {foundProduct.ProductName}");
                    Console.WriteLine($"Hãng sản xuất: {foundProduct.Manufacturer}");
                    Console.WriteLine($"Giá sản phẩm: {foundProduct.Price}");
                    Console.WriteLine($"Mô tả khác: {foundProduct.OtherDescription}");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sản phẩm với mã đã nhập.");
                }
            }

            static void SaveProducts()
            {
                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, products);
                        Console.WriteLine("Danh sách sản phẩm đã được lưu.");
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Lỗi: Không thể lưu danh sách sản phẩm.");
                }
            }

            static void LoadProducts()
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        using (Stream stream = File.Open(filePath, FileMode.Open))
                        {
                            BinaryFormatter bin = new BinaryFormatter();
                            products = (List<Product>)bin.Deserialize(stream);
                        }
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Lỗi: Không thể đọc danh sách sản phẩm.");
                    }
                }
            }
        }
    }
}
