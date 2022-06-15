using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SmartER_Garden_API;
using SmartER_Garden_Library.SmartER_Garden_Models;

namespace SmartER_Garden_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Eintrag> eintraege = new ObservableCollection<Eintrag>();
        private readonly RestHelper restHelper;

        public MainWindow()
        {
            InitializeComponent();

            eintraege.Clear();
            dg.ItemsSource = null;
            GetEintraege(); 
        }

        private async void GetEintraege()
        {
            eintraege.Clear();
            dg.ItemsSource = null;

            IEnumerable<Eintrag> getResult = null;
            try
            {
                getResult = (IEnumerable<Eintrag>)await restHelper.GetEintraege();
            }
            catch (Exception ex)
            {
                return;
                //wird nicht ausgegeben? 
            }

            eintraege.Clear();

            foreach (var item in getResult)
            {
                eintraege.Add(item);
            }
        }

        private void btn_aendern_Click(object sender, RoutedEventArgs e)
        {
            var eintrag = new EintragWindow();
            eintrag.Show(); 
        }

        private async void btn_loeschen_Click(object sender, RoutedEventArgs e)
        {
            Eintrag eintrag = GetSelectedEintrag();
            if (eintrag == null)
                return;

            try
            {
                await restHelper.DeleteEintrag((int)eintrag.Id);
            }
            catch (Exception rce)
            {
                return;
                //lieber keine fehlermeldungen ausgeben :) 
            }

            eintraege.Remove(eintrag);
            dg.ItemsSource = null;

        }

        private Eintrag GetSelectedEintrag()
        {
            int ix = dg.SelectedIndex;
            if (ix < 0)
            {
                return null;
            }

            return eintraege[ix];
        }
    }
}
