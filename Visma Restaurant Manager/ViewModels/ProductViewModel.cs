using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma_Restaurant_Manager.DB;
using Visma_Restaurant_Manager.Models;
using Visma_Restaurant_Manager.Views;

namespace Visma_Restaurant_Manager.ViewModels
{

    class ProductViewModel
    {
        DataBase _dataBase;
        FileImput _fIO;

        private IView _consoleUI;
        public IView consoleUI {
            set { _consoleUI = value; }
        }

        public void Add( ) {
           
        }

        public ProductViewModel(DataBase dataBase, FileImput fileImput) {
            _dataBase = dataBase;
            _fIO = fileImput;
        }

        public List<Product>getProducts() {
            return _dataBase.products;     // Not so good since UI gets acess to products
        }
        public void addProduct(string name, int portionCount, string unit, double portionSize) {
            _dataBase.addProduct(new Product(name, portionCount, unit, portionSize));
        }
        public Product GetProduct(int id) {
            return _dataBase.getProduct(id);
        }
        public void UpdateProduct(int id, string name, int portionCount, string unit, double portionSize) {
            _dataBase.removeProduct(id);
            _dataBase.addProduct(new Product(id, name, portionCount, unit, portionSize));
        }
        public void RemoveProduct(int id) {
            _dataBase.removeProduct(id);
        }

        public void WriteProductsToCsv() {
            _fIO.WriteProductsToCsv(_dataBase.products);
        }
        public void LoadProductsFromCsv() {
            _dataBase.loadProducts(_fIO.ReadProductsFromCsv());
        }
    }
}
