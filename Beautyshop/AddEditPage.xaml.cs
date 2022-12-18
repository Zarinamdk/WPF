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

namespace Beautyshop
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private ID_Мастера _currentMaster = new ID_Мастера();
        public AddEditPage(ID_Мастера selectedMaster)
        {
            if (selectedMaster != null)
                _currentMaster = selectedMaster;
            InitializeComponent();
            DataContext = _currentMaster;
            ComboKategories.ItemsSource = beautyshopEntities.GetContext().ID_Категории.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentMaster.Фамилия))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(_currentMaster.Имя))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(_currentMaster.Отчество))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentMaster.Возраст)))
                errors.AppendLine("Укажите возраст от 18 до 60");
            if (Convert.ToString(_currentMaster.Категория) == null)
                errors.AppendLine("Выберите категорию");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentMaster.ID_мастера1 == 0)
                beautyshopEntities.GetContext().ID_Мастера.Add(_currentMaster);
            try
            {
                beautyshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
