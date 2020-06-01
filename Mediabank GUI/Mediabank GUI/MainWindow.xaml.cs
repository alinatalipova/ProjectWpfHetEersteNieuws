using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mediabank_GUI.Models;
using Microsoft.Win32;

namespace Mediabank_GUI
{
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
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd_Cancel_New_Article.Visibility = Visibility.Hidden;
            btnAdd_New_Article.Visibility = Visibility.Visible;

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

        private void AddArticlClicked(object sender, RoutedEventArgs e)
        {
            Article newArticle = new Article(); 
            
            newArticle.Title = txtTitle.Text;
            newArticle.ID = txtId.Text;
            newArticle.Author = txtWriter.Text;
            newArticle.Content = txbTextArticle.Text;
            
            if (rbAutorisationYes.IsChecked == true)
            {
                newArticle.AuthToPublish = true;
            }
            else
            {
                newArticle.AuthToPublish = false;
            }

            // we voegen een geinitiaaliseerde object aan onze lijst articles
            articles.Add(newArticle);


            btnErase.Visibility = Visibility.Visible;
            btnPublish.Visibility = Visibility.Visible;
            btnmodify.Visibility = Visibility.Visible;
            btnPreview.Visibility = Visibility.Visible;
            btnAdd_New_Article.Visibility = Visibility.Visible;

            btnAdd_Cancel_New_Article.Visibility = Visibility.Hidden;
        }


        // titel verwijderen uit listbox 
        private void BtnErase_Click(object sender, RoutedEventArgs e)
        {
            Article selectie = (Article)lbxArticles.SelectedItem;
            if (selectie != null)
            {
                articles.Remove(selectie);
                UpdateDetailsView(new Article());
            }
            UpdateUI();
        }

        private void Btnmodify_Click(object sender, RoutedEventArgs e)
        {
            if (lbxArticles.SelectedItem != null)
            // casten om een access te hebben aan the propreties van het object
            {
                Article selectedArticel = (Article)lbxArticles.SelectedItem;

                selectedArticel.Title = txtTitle.Text;
                selectedArticel.ID = txtId.Text;
                selectedArticel.Author = txtWriter.Text;

                if (rbAutorisationYes.IsChecked == true)
                {
                    selectedArticel.AuthToPublish = true;
                }
                else
                {
                    selectedArticel.AuthToPublish = false;
                }


                lbxArticles.ItemsSource = null;
                lbxArticles.ItemsSource = articles;
            }
        }

        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (lbxArticles.SelectedItem != null)
            {
                Article selectedArticel = (Article)lbxArticles.SelectedItem;
                if (selectedArticel.AuthToPublish == true)
                {
                    selectedArticel.Published = true;
                    UpdateDetailsView(selectedArticel);
                }
            }
        }

        private void AddNewArticleClicked(object sender, RoutedEventArgs e)
        {
            ClearDetailView();
            btnAdd_New_Article.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Visible;
            btnAdd_Cancel_New_Article.Visibility = Visibility.Visible;

            btnErase.Visibility = Visibility.Hidden;
            btnPublish.Visibility = Visibility.Hidden;
            btnmodify.Visibility = Visibility.Hidden;
            btnPreview.Visibility = Visibility.Hidden;
        }

        private void CancelNewArticleClicked(object sender, RoutedEventArgs e)
        {
            btnAdd_New_Article.Visibility = Visibility.Visible;

            btnErase.Visibility = Visibility.Visible;
            btnPublish.Visibility = Visibility.Visible;
            btnmodify.Visibility = Visibility.Visible;
            btnPreview.Visibility = Visibility.Visible;

            btnAdd.Visibility = Visibility.Hidden;
            btnAdd_Cancel_New_Article.Visibility = Visibility.Hidden;
        }


        // file opzoeken en openen 
        private void BtnOpenfile_Click(object sender, RoutedEventArgs e)
        {
            // openfilediaolo
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
        }

        private void txbtext_KeyDown(object sender, KeyEventArgs e)
        {
            //string delen dor middel van split functie. de array dat je krijgt tellen ( met count? of lenghth)
            //en dan heb je aantal woorden
            // IF ALS ER MEER DAN 500 woorden dan errormessage 
        }
        private void LbxArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxArticles.SelectedItem != null)
            {
                UpdateUI();
                // titel is zichtbaar in textbox , ID en auteur bij selectie van een listboxItem
                Article selectedArticle = (Article)lbxArticles.SelectedItem;
                UpdateDetailsView(selectedArticle);
            }
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
        private void ShowErrorifTextboxEmpty()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtWriter.Text) ||
                cbCategory.SelectedIndex == -1 ||
                (rbAutorisationNo.IsChecked == false && rbAutorisationYes.IsChecked == false))
            {
                lblError.Content = " Alle velden moeten ingevuld worden!";
            }

            // titel mag max 50 karakters bevatten 
            // max lengte van artikel is 500 woorden 
            // als publicatie ja is dan mag "mag gepubliceerd" niet nee zijn
        }

        private void UpdateDetailsView(Article article)
        {
            txtId.Text = article.ID;
            txtTitle.Text = article.Title;
            txtWriter.Text = article.Author;
            txbTextArticle.Text = article.Content;

            if (article.AuthToPublish == true)
            {
                rbAutorisationYes.IsChecked = true;
                rbAutorisationNo.IsChecked = false;

            }
            else
            {
                rbAutorisationYes.IsChecked = false;
                rbAutorisationNo.IsChecked = true;
            }
            if (article.Published)
            {
                cbxPublished.IsChecked = true;
            }
            else
            {
                cbxPublished.IsChecked = false;
            }
        }

        private void ClearDetailView()
        {
            txtId.Text = "";
            txtTitle.Text = "";
            txtWriter.Text = "";
            txbTextArticle.Text = "";
            rbAutorisationYes.IsChecked = null;
            rbAutorisationNo.IsChecked = null;
            cbxPublished.IsChecked = null;
        }
    }
}
