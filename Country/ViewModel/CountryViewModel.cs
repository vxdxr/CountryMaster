using Country.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Country.Model;


namespace Country.ViewModel
{


    internal class CountryViewModel : BindableObject
    {
        // Переменная для хранения состояния
        // выбранного элемента коллекции
        private Model.Countrym _selectedItem;
        // Объект с логикой по извлечению данных
        // из источника
        CountryService countryService = new();
        // Коллекция извлекаемых объектов
        public ObservableCollection<Model.Countrym> Countries { get; } = new();
        // Конструктор с вызовом метода
        // получения данных

        public CountryViewModel()
        {
            GetCountryAsync();
        }
        // Публичное свойство для представления
        // описания выбранного элемента из коллекции
        public string Desc { get; set; }
        // Свойство для представления и изменения
        // состояния выбранного объекта
        public Model.Countrym SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                Desc = $"Area: {value?.Area} \nPopulation: {value?.Population}";
                // Метод отвечает за обновление данных
                // в реальном времени
                OnPropertyChanged(nameof(Desc));
            }
        } 
        public ICommand AddItemCommand => new Command(() => AddNewItem());

        // Метод для создания нового элемента
        public void AddNewItem()
        {
            Countries.Add(new Countrym
            {
                Id = Countries.Count + 1,
                Name = "Title " + Countries.Count,
                Population = 1,
                Area = 2
            });
        }

        // Метод получения коллекции объектов
        async Task GetCountryAsync()
        {
            try
            {
                var country = await countryService.GetCountry();
                if (Countries.Count != 0)
                    Countries.Clear();
                foreach (var c in country)
                {
                    Countries.Add(c);
                }
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Ошибка!", $"Что - то пошло не так: {ex.Message}", "OK");
            }
        }

    }
}