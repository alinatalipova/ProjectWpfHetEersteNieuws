using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Mediabank_GUI.Models;
using Microsoft.Win32;




namespace Mediabank_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //articles is een verzameling van objecten
        BindingList<Article> articles = new BindingList<Article>();

        public MainWindow()
        {
            InitializeComponent();
            lbxArticles.ItemsSource = articles;
            Fakedata();

        }



        //DIT IS GEWOON EEN TEST OM DE KLASSES EN OBJECTEN TE PROBEREN  GEBRUIKEN 
        private void Fakedata()

        {

            // object maken van deze classe

            Article art = new Article();
            //properties een waarde geven
            art.Title = "Vlaamse regering wil zomerscholen om achterstand weg te werken";
            art.ID = "00000001";
            art.Author = "CATHY GALLE";
            articles.Add(art);

        }

        //Toevoegen van titel in listbox
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //toevoegen van titel in listbox
            Article newArticle = new Article();
            newArticle.Title = txtTitle.Text;
            newArticle.ID = txtId.Text;
            newArticle.Author = txtWriter.Text;

            articles.Add(newArticle);

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
            if (lbxArticles.SelectedItem != null)
            {

                UpdateUI();
                //titel is zichtbaar in textbox , ID en auteur bij selectie van een listboxItem
                Article selectedArticle = (Article)lbxArticles.SelectedItem;
                updateDetailsView(selectedArticle.ID, selectedArticle.Title, selectedArticle.Author);
            }

        }

        // foutmeldingen 
        private void Foutmelding()
        {
            //alle velden moeten ingevuld worden 
            // titel mag max 50 karakters bevatten 
            // max lengte van artikel is 500 woorden 
            // als publicatie ja is dan mag "mag gepubliceerd" niet nee zijn
        }

        private void updateDetailsView(String txtIdParam, string txtTitleParam, string txtWriterParam)
        {
            txtId.Text = txtIdParam;
            txtTitle.Text = txtTitleParam;
            txtWriter.Text = txtWriterParam;
        }

        //titel verwijderen uit listbox 
        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            Article selectie = (Article)lbxArticles.SelectedItem;
            if (selectie != null)
            {
                articles.Remove(selectie);
                updateDetailsView("", "", "");
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
