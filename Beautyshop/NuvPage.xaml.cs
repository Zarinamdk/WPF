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
    /// Логика взаимодействия для NuvPage.xaml
    /// </summary>
    public partial class NuvPage : Page
    {
        public NuvPage()
        {
            InitializeComponent();
            DGridMaster.Items.Clear();
            //DGridMaster.ItemsSource = beautyshopEntities.GetContext().ID_Мастера.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
      
        }

        private void BthEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as ID_Мастера));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mastersForRemoving = DGridMaster.SelectedItems.Cast<ID_Мастера>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {mastersForRemoving.Count()} элемента?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    beautyshopEntities.GetContext().ID_Мастера.RemoveRange(mastersForRemoving);
                    beautyshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridMaster.ItemsSource = beautyshopEntities.GetContext().ID_Мастера.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                beautyshopEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridMaster.ItemsSource = beautyshopEntities.GetContext().ID_Мастера.ToList();
            }
        }
    }
}
