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

namespace V5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DatabaseEntities db = new DatabaseEntities();
            var tab = from d in db.Tables
                      select d;

            this.DataGrid.ItemsSource = tab.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DatabaseEntities db = new DatabaseEntities();

            Table TableObject = new Table()
            {
                Imie = TextImie.Text,
                Nazwisko = TextNazwisko.Text,
                WykształcenieID = int.Parse(TextWyksztalcenie.Text),
                SpecyfikacjaID = int.Parse(TextSpec.Text),
                PłećID = int.Parse(TextPlec.Text)
            };
            db.Tables.Add(TableObject);
            db.SaveChanges();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            DatabaseEntities db = new DatabaseEntities();
            this.DataGrid.ItemsSource = db.Tables.ToList();
        }


        private int updateTableID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataGrid.SelectedIndex >= 0)
            {
                if (this.DataGrid.SelectedItems.Count >= 0)
                {
                    if (this.DataGrid.SelectedItems[0].GetType() == typeof(Table))
                    {
                        Table t = (Table)this.DataGrid.SelectedItems[0];
                        this.TextImieUP.Text = t.Imie;
                        this.TextNazwiskoUP.Text = t.Nazwisko;
                        this.TextPlecUP.Text = t.PłećID.ToString();
                        this.TextSpecUP.Text = t.SpecyfikacjaID.ToString();
                        this.TextWyksztalcenieUP.Text = t.WykształcenieID.ToString();
                        this.updateTableID = t.Id;
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DatabaseEntities db = new DatabaseEntities();

            var r = from d in db.Tables
                    where d.Id == this.updateTableID
                    select d;

            Table obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Imie = this.TextImieUP.Text;
                obj.Nazwisko = this.TextNazwiskoUP.Text;
                obj.SpecyfikacjaID = int.Parse(this.TextSpecUP.Text);
                obj.WykształcenieID = int.Parse(this.TextWyksztalcenieUP.Text);
                obj.PłećID = int.Parse(this.TextPlecUP.Text);
            }

            db.SaveChanges();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to Delete?",
                "Delete Weapon",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                DatabaseEntities db = new DatabaseEntities();
                var r = from w in db.Tables
                        where w.Id == this.updateTableID
                        select w;

                Table obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Tables.Remove(obj);
                    db.SaveChanges();
                }
            }
        }
    }
}
