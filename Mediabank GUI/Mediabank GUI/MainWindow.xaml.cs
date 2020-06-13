using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Mediabank_GUI.Models;
using Microsoft.Win32;

namespace Mediabank_GUI
{
    public partial class MainWindow : Window
    {
        Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
        // articles is een verzameling van objecten
        // you can use a BindingList<T>, instead of your List<T>, to automatically recognize new items added. Your ShowData() method must be called once at startup.
        BindingList<Article> articles = new BindingList<Article>();
        public List<Article> ToPublishArticles = new List<Article>();

        //class <Type / classe > naam = new class<type>();
        public MainWindow()
        {
            InitializeComponent();
            lbxArticles.ItemsSource = articles;
            btnAdd.Visibility = Visibility.Hidden;
            btnAdd_Cancel_New_Article.Visibility = Visibility.Hidden;
            btnAdd_New_Article.Visibility = Visibility.Visible;
            lbxArticles.SelectionMode = SelectionMode.Multiple; // activeren van meerdere selecties , selecties van meerde items

        }

        private void AddArticlClicked(object sender, RoutedEventArgs e)
        {
            if (GetErrorInfoAndSetCounter())
            {
                Article newArticle = new Article();

                newArticle.Title = txtTitle.Text;
                newArticle.ID = txtId.Text;
                newArticle.Author = txtWriter.Text;
                newArticle.Content = txbTextArticle.Text;

                if (rbAutorisationYes.IsChecked == true)
                {
                    ToPublishArticles.Add(newArticle);
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
            if (lbxArticles.SelectedItem != null && GetErrorInfoAndSetCounter())
            // casten om een access te hebben aan the propreties van het object
            {
                Article selectedArticel = (Article)lbxArticles.SelectedItem; // we casten



                if (rbAutorisationYes.IsChecked == true)
                {

                    ToPublishArticles.Add(selectedArticel);
                }
                else
                {
                    ToPublishArticles.Remove(selectedArticel);
                }

                selectedArticel.Title = txtTitle.Text;
                selectedArticel.ID = txtId.Text;
                selectedArticel.Author = txtWriter.Text;
                selectedArticel.Content = txbTextArticle.Text;

                lbxArticles.ItemsSource = null;
                lbxArticles.ItemsSource = articles;
            }
            //ErrorMessage wordt getoond
            else
            {
                GetErrorInfoAndSetCounter();
            }

        }

        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {

            using (StreamWriter writer = new StreamWriter("article1.txt"))

                foreach (Article selectedItem in ToPublishArticles)
                {

                    selectedItem.Published = true;

                    {
                        writer.Write(selectedItem.Title);
                        writer.WriteLine(selectedItem.Content);
                        writer.WriteLine(selectedItem.Author);

                    }
                    UpdateDetailsView(selectedItem);


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

            //ErrorMessage wordt getoond
            GetErrorInfoAndSetCounter();

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
            // OpenFileDialog ofd = new OpenFileDialog();
            // ofd.ShowDialog();


            // Create OpenFileDialog
            // Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                openFileDlg.Filter = "All files (*.*)|*.*";
                combobox1.Text = openFileDlg.FileName;
                txbTextArticle.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }

            GetErrorInfoAndSetCounter();

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
            if (rbAutorisationNo.IsChecked == true)
            {
                btnPublish.IsEnabled = false;
            }
        }

        

        private bool GetErrorInfoAndSetCounter()//toont de error tekst +kijken dat alle velden in orde zijn
        {
            int AmountOfCharInTitle = txtTitle.Text.Length;
            bool checkContentTitle = true;
            bool checkContentId = true;
            bool checkContentWriter = true;
            bool checkContentCategory = true;
            bool checkContentAutorisation = true;
            bool checkContentLenght = true;
            string errormessage = "";
            if (string.IsNullOrEmpty(txtTitle.Text) )
            {
                checkContentTitle = false;
                errormessage += "Vul een titel in!" + Environment.NewLine;
            }
            if (txtTitle.Text.Length > 50)
            {
                errormessage += " De titel mag niet meer dan 50 characters bevatten." + Environment.NewLine;
                checkContentTitle = false;
            }

            if (string.IsNullOrEmpty(txtId.Text))
            {
                
                checkContentId = false;
                errormessage += "Vul een ID nummer in!" + Environment.NewLine;
            }
            
            if (string.IsNullOrEmpty(txtWriter.Text))
            {
                
                checkContentWriter = false;
                errormessage += "Vul de autheur in!" + Environment.NewLine;
            }
            
           
            
            if (rbAutorisationNo.IsChecked==false &&rbAutorisationYes.IsChecked ==false)
            {
                checkContentAutorisation = false;
                errormessage += "Authorisatie tot publicatie moet ingevuld worden" + Environment.NewLine;
            }
            
            if (cbCategory.SelectedIndex== -1)
            {
                checkContentCategory = false;
                errormessage += "geef een catefgorie aan!" + Environment.NewLine;
            }
 

            List<string> articleText = new List<string>();
            // trimEnd : om de spatie niet mee te tellen , ToList () : om array van strings naar lijs te converteren ( van split ) 
            
            articleText = txbTextArticle.Text.TrimEnd(' ').Split(' ').ToList();
            if (articleText.Count >= 500)
            {
               errormessage += "Mag niet langer zijn dan 500 woorden " + Environment.NewLine;
               checkContentLenght = false;
            }
            
            lblLenght.Content = articleText.Count;

            lblError.Content = errormessage;

            // als publicatie ja is dan mag "mag gepubliceerd" niet nee zijn
            //mag alleen cijfers invullen in ID


            if (checkContentId &&
                checkContentTitle &&
                checkContentWriter &&
                checkContentCategory &&
                checkContentAutorisation &&
                checkContentLenght)
            {
                return true;
            }
            return false;
        }


        private void UpdateDetailsView(Article article)
        {
            txtId.Text = article.ID;
            txtTitle.Text = article.Title;
            txtWriter.Text = article.Author;
            txbTextArticle.Text = article.Content;


            if (article.Published)
            {
                cbxPublished.IsChecked = true;
            }
            else
            {
                cbxPublished.IsChecked = false;
            }
        }

        private void ClearDetailView()//bij het toevoegen van nieuwe artikel worden alle velden eerst leeg gemaakt
        {
            txtId.Text = "";
            txtTitle.Text = "";
            txtWriter.Text = "";
            txbTextArticle.Text = "";
            rbAutorisationYes.IsChecked = false;
            rbAutorisationNo.IsChecked = false;
            cbxPublished.IsChecked = false;
            cbCategory.SelectedIndex = -1;
            lblLenght.Content = "";
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            
            //ErrorMessage wordt getoond
            //lblError.Content = GetErrorInfoAndSetCounter().Message;
        }

 
        private void OnKeyUpTextArticle(object sender, KeyEventArgs e)//wanneer je key up gaat dan wodt lengte getelt
        {
            GetErrorInfoAndSetCounter();
        }

        private void cbxPublished_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
