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
    class MenuItemViewModel
    {
        DataBase _dataBase;
        FileImput _fIO;

        private IView _consoleUI;
        public IView consoleUI { 
            set { _consoleUI = value; } 
        }

        public MenuItemViewModel(DataBase dataBase, FileImput fileImput) {
            _dataBase = dataBase;
            _fIO = fileImput;
        }

        public List<MenuItem> getMenuItems() {
            return _dataBase.menuItems;     // Not so good since UI gets acess to products
        }
        public void addMenuItem(string name, List<int> products) {
            _dataBase.addMenuItem(new MenuItem(name, products));
        }
        public MenuItem GetMenuItem(int id) {
            return _dataBase.GetMenuItem(id);
        }
        public void UpdateMenuItem(int id, string name, List<int> products) {
            _dataBase.removeMenuItem(id);
            _dataBase.addMenuItem(new MenuItem(id, name, products));
        }
        public void RemoveMenuItem(int id) {
            _dataBase.removeMenuItem(id);
        }

        public void WriteMenuItemsToCsv() {
            _fIO.WriteMenuItemsToCsv(_dataBase.menuItems);
        }
        public void LoadMenuItemsFromCsv() {
            _dataBase.loadMenuItems(_fIO.ReadMenuItemsFromCsv());
        }

    }
}
