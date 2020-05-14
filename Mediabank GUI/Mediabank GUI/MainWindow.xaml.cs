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
using Microsoft.Win32;




namespace Mediabank_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //KLASSE DIE ARTIKEL VOORSTELT 
        class Article
        {
            public string Title { get; set; }
            public string ID { get; set; }
            public string Author { get; set; }
          
        }

       //DIT IS GEWOON EEN TEST OM DE KLASSES EN OBJECTEN TE PROBEREN  GEBRUIKEN 
        private void ArticlesData () 
        {
            // object maken van deze classe
            Article art = new Article();
            //properties een waarde geven
            art.Title = "Vlaamse regering wil zomerscholen om achterstand weg te werken";
            art.ID = "00000001";
            art.Author = "CATHY GALLE";
        }

        //Toevoegen van titel in listbox
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //toevoegen van titel in listbox
            ListBoxItem titles = new ListBoxItem();
            titles.Content = txtTitle.Text;
            lbxArticles.Items.Add(titles);
            txtTitle.Text = "";
        }

      private void UpdateUI()
        {
            // als er een titel geselecteerd is in lbx dan staanuttons erase, publish, preview en modify enable
            if (lbxArticles.SelectedItem != null)
            {
                //Buttons enablen
                btnErase.IsEnabled = true;
                btnPublish.IsEnabled = true;
                btnPreview.IsEnabled = true;
                btnmodify.IsEnabled = true;
            }
            
         }

        private void LbxArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
            //titel is zichtbaar in textbox bij selectie
            ListBoxItem titleSelection = (ListBoxItem)lbxArticles.SelectedItem;
             txtTitle.Text = titleSelection.Content.ToString();
        }

        // foutmeldingen 
       private void Foutmelding()
        {
            //alle velden moeten ingevuld worden 
            // titel mag max 50 karakters bevatten 
            // max lengte van artikel is 500 woorden 
            // als publicatie ja is dan mag "mag gepubliceerd" niet nee zijn
        }

        //titel verwijderen uit listbox 
        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectie = (ListBoxItem)lbxArticles.SelectedItem;
            if (selectie != null)
            {
                lbxArticles.Items.Remove(selectie);
            }
            UpdateUI();
        }

        //File opzoeken en openen 
        private void btnOpenfile_Click(object sender, RoutedEventArgs e)
        {
            //openfilediaolo
           OpenFileDialog ofd = new OpenFileDialog();
           ofd.ShowDialog();
        }
    }
}
