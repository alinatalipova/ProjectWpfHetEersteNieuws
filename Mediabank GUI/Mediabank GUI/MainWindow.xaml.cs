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

        // articles is een verzameling van objecten
        // you can use a BindingList<T>, instead of your List<T>, to automatically recognize new items added. Your ShowData() method must be called once at startup.
        BindingList<Article> articles = new BindingList<Article>();
        //class <Type / classe > naam = new class<type>();
        public MainWindow()
        {
            InitializeComponent();
            lbxArticles.ItemsSource = articles;
            Testdata();

        }



        //DIT IS GEWOON EEN TEST OM DE KLASSES EN OBJECTEN TE PROBEREN  GEBRUIKEN 
        private void Testdata()
        // fonction

        {

            // object maken van deze classe
            Article art = new Article();
            // properties een waarde geven
            art.Title = "Vlaamse regering wil zomerscholen om achterstand weg te werken";
            art.ID = "00000001";
            art.Author = "CATHY GALLE";
            art.Content = "";
            articles.Add(art);

        }

        // toevoegen van titel, ID , Auteur in listbox 
        private void addArticlClicked(object sender, RoutedEventArgs e)
        {
            // toevoegen van titel in listbox, aanmaken van een nieuwe artikel
            Article newArticle = new Article(); //=initialiseren van een nieuwe object van een type Article
            // we give values to the proporties
            newArticle.Title = txtTitle.Text;
            newArticle.ID = txtId.Text;
            newArticle.Author = txtWriter.Text;
            newArticle.Content = txbtext.Text;
            //string textbox = new TextRange(rtxbInhoud.Document.ContentStart, rtxbInhoud.Document.ContentEnd).Text ;
      

            // we voegen een geinitiaaliseerde object aan onze lijst articles
            articles.Add(newArticle);

        }

        private void UpdateUI()
        {
            // als er een titel geselecteerd is in lbx dan staanuttons erase, publish, preview en modify enable
            if (lbxArticles.SelectedItem != null)
            {
                // buttons enablen
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
                // titel is zichtbaar in textbox , ID en auteur bij selectie van een listboxItem
                Article selectedArticle = (Article)lbxArticles.SelectedItem;
                updateDetailsView(selectedArticle.ID, selectedArticle.Title, selectedArticle.Author, selectedArticle.Content);

            }

        }

       
        private void ShowErrorifTextboxEmpty()
        {
            // alle velden moeten ingevuld worden 
            if (string.IsNullOrWhiteSpace(txtId.Text)||
                string.IsNullOrWhiteSpace(txtTitle.Text)||
                string.IsNullOrWhiteSpace(txtWriter.Text)|| 
                cbCategory.SelectedIndex == -1
                )
            {
                lblError.Content = " Alle velden moeten ingevuld worden!";
            }
            
            // titel mag max 50 karakters bevatten 
            // max lengte van artikel is 500 woorden 
            // als publicatie ja is dan mag "mag gepubliceerd" niet nee zijn
        }

        private void updateDetailsView(String txtIdParam, string txtTitleParam, string txtWriterParam, string txtContent)
        {
            txtId.Text = txtIdParam;
            txtTitle.Text = txtTitleParam;
            txtWriter.Text = txtWriterParam;
            txbtext.Text = txtContent;

        }

        // titel verwijderen uit listbox 
        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            Article selectie = (Article)lbxArticles.SelectedItem;
            if (selectie != null)
            {
                articles.Remove(selectie);
                updateDetailsView("", "", "","");
            }
            UpdateUI();
        }

        // file opzoeken en openen 
        private void btnOpenfile_Click(object sender, RoutedEventArgs e)
        {
            // openfilediaolo
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
        }

        private void btnmodify_Click(object sender, RoutedEventArgs e)
        {
            if (lbxArticles.SelectedItem != null)
                // casten om een access te hebben aan the propreties van het object
            {
                Article selectedArticel = (Article)lbxArticles.SelectedItem;

                selectedArticel.Title = txtTitle.Text ;
                selectedArticel.ID = txtId.Text;
                selectedArticel.Author = txtWriter.Text;

                lbxArticles.ItemsSource = null;
                lbxArticles.ItemsSource = articles;

            }


        }

        private void txbtext_KeyDown(object sender, KeyEventArgs e)
        {
            //string delen dor middel van split functie. de array dat je krijgt tellen ( met count? of lenghth)
            //en dan heb je aantal woorden
            // IF ALS ER MEER DAN 500 woorden dan errormessage 
        }
    }
}
