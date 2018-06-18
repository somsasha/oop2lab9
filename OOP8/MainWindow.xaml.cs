using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Entity.Infrastructure;


//1)Для чего используют многоуровневые архитектуры?
//Многоуровневая архитектура - одна из архитектурных парадигм разработки ПО,
//при которой разбиение приложения на самостоятельные состовные части
//происходит по реализуемой ими функциональности.

//2)Опишите назначение слоев многоуровневой архитектуре. Business layer 
//(уровень бизнес-логики), Data Access layer(уровень доступа к данным). 
//1.Business layer - содержит набор компонентов, 
//которые отвечают за обработку полученных от уровня представлений данных, 
//реализует всю необходимую логику приложения, все вычисления, взаимодействует с базой данных и 
//передает уровню представления результат обработки.
//2.Data Access layer - хранит модели, описывающие используемые сущности, 
//также здесь размещаются специфичные классы для работы с разными технологиями доступа к данным, 
//например, класс контекста данных Entity Framework. 
//Здесь также хранятся репозитории, через которые уровень бизнес-логики взаимодействует с базой данных.
//3.Presentation layer - это тот уровень, с которым непосредственно взаимодействует пользователь. 
//Этот уровень включает компоненты пользовательского интерфейса, механизм получения ввода от пользователя.

//3)Паттерн Repository
//Паттерн Repository посредничает между слоем области определения и слоем распределения данных, работая, как обычная колекция объектов области определения. 
//Объекты-клиенты создают описание запроса декларативно и направляют их к объекту-репозиторию (Repository) для обработки. 
//Объекты могут быть добавлены или удалены из репозитория, как будто они формируют простую коллекцию объектов. 
//А код распределения данных, скрытый в объекте Repository, позаботится о соответсвующих операциях в незаметно для разработчика.

//4)Паттерн Unit of Work
//Обслуживает набор объектов, изменяемых в бизнес-действии и управляет записью изменений и разрешением проблем конкуренции данных.
//Реализация паттерна Unit of Work следит за всеми действиями приложения, которые могут изменить БД в рамках одного бизнес-действия. 
//Когда бизнес-действие завершается, Unit of Work выявляет все изменения и вносит их в БД.

//5)Опишите основные особенности Entity Framework  
//Entity Framework представляет специальную объектно-ориентированную технологию на базе фреймворка .NET для работы с данными. 
//Если традиционные средства ADO.NET позволяют создавать подключения, команды и прочие объекты для взаимодействия с базами данных, 
//то Entity Framework представляет собой более высокий уровень абстракции, который позволяет абстрагироваться от самой базы данных и работать с данными независимо от типа хранилища.
//Центральной концепцией Entity Framework является понятие сущности или entity. Сущность представляет набор данных, ассоциированных с определенным объектом. 
//Поэтому данная технология предполагает работу не с таблицами, а с объектами и их наборами.
//У каждой сущности может быть одно или несколько свойств, 
//которые будут отличать эту сущность от других и будут уникально определять эту сущность. Подобные свойства называют ключами.
//Отличительной чертой Entity Framework является использование запросов LINQ для выборки данных из БД
//Entity Framework предполагает три возможных способа взаимодействия с базой данных: Database first, Model first и Code first

//6)Какие  преимущества  обеспечивает  использование  слабосвязанного кода
//Повторное использование, расширяемость, более лёгкая замена компонентов и уменьшение побочных эффектов.


namespace OOP8 {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        UnitOfWork unitOfWork = new UnitOfWork();
        PhoneContext p = new PhoneContext();
        public MainWindow() {
            InitializeComponent();
            //Company c1 = new Company { CompanyName = "Samsung" };
            //Company c2 = new Company { CompanyName = "Apple" };
            //Company c3 = new Company { CompanyName = "Huawei" };
            //unitOfWork.CompanyRepository.Insert(c1);
            //unitOfWork.CompanyRepository.Insert(c2);
            //unitOfWork.CompanyRepository.Insert(c3);
            //Phone p1 = new Phone { Name = "Galaxy S5", Price = 20000, Date = DateTime.Parse("Jan 15, 2015").Date, CompanyObj = c1 };
            //Phone p2 = new Phone { Name = "Galaxy S4", Price = 15000, Date = DateTime.Parse("Mar 26, 2014").Date, CompanyObj = c1 };
            //Phone p3 = new Phone { Name = "iPhone 5", Price = 28000, Date = DateTime.Parse("Sep 2, 2015").Date, CompanyObj = c2 };
            //Phone p4 = new Phone { Name = "iPhone 4S", Price = 23000, Date = DateTime.Parse("Jun 9, 2014").Date, CompanyObj = c2 };
            //Phone p5 = new Phone { Name = "U8230", Price = 8000, Date = DateTime.Parse("Nov 11, 2012").Date, CompanyObj = c3 };
            //unitOfWork.PhoneRepository.dbSet.AddRange(new List<Phone>() { p1, p2, p3, p4, p5 });
            //unitOfWork.CompanyRepository.dbSet.Load();
            //unitOfWork.PhoneRepository.dbSet.Load();
            //MyDataGrid.ItemsSource = unitOfWork.CompanyRepository.dbSet.Local.ToBindingList();
            //ShowRepository();
            //Closing += Window_Closing;
        }

        private void Update() {
            try {
                Update();
            }
            catch (Exception) {
            }
            MessageBox.Show("Отправлено");
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e) {
            try {
                var context = ((IObjectContextAdapter)unitOfWork.context).ObjectContext;
                var refreshableObjects = unitOfWork.context.ChangeTracker.Entries().Select(c => c.Entity).ToList();
                context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, refreshableObjects);
            }
            catch (Exception) {
            }
            MessageBox.Show("Обновлено");
        }
        private void Window_Closing(object sender, CancelEventArgs e) {
            unitOfWork.Dispose();
        }
        private void updateButton_Click(object sender, RoutedEventArgs e) {
            unitOfWork.Save();
        }
        private List<Phone> SearchOnPrice() {
            var min = int.Parse(priceMin.Text);
            var max = int.Parse(priceMax.Text);
            var list = unitOfWork.PhoneRepository.dbSet.Where(p => (p.Price >= min && p.Price <= max)).ToList();
            return list;
        }
        private List<Phone> SearchOnDate() {
            var date = DateTime.Parse(dateSearch.Text);
            var list = unitOfWork.PhoneRepository.dbSet.Where(p => (p.Date >= date)).ToList();
            return list;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window1 window1 = new Window1();
            if ((bool)priceCheck.IsChecked && (bool)dateCheck.IsChecked) {
                var resultList = SearchOnPrice().Intersect(SearchOnDate()).ToList();
                window1.temp.ItemsSource = new BindingList<Phone>(resultList);
            }
            else if ((bool)priceCheck.IsChecked) {
                window1.temp.ItemsSource = new BindingList<Phone>(SearchOnPrice());
            }
            else if ((bool)dateCheck.IsChecked) {
                window1.temp.ItemsSource = new BindingList<Phone>(SearchOnDate());
            }
            window1.Show();
        }

        private void ButtonTransaction_Click(object sender, RoutedEventArgs e) {
            ShowTransaction();
        }

        private void ShowRepository() {
            Window1 window1 = new Window1();
            var phones = unitOfWork.PhoneRepository.Get(p => p.Name.StartsWith("G")).ToList();
            phones.Add(unitOfWork.PhoneRepository.GetByID(5));
            window1.temp.ItemsSource = new BindingList<Phone>(phones); 
            window1.Show();
        }

        private void ShowTransaction() {
            using (PhoneContext db = new PhoneContext()) {
                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        db.Database.ExecuteSqlCommand(@"UPDATE Phone SET Price = Price + 1000");
                        transaction.Commit();
                    }
                    catch (Exception ex) {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}