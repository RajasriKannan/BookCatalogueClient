using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookCatalogueClient.BooksCatalogueService;

namespace BookCatalogueClient
{
    public partial class OnlineBookCatalogueHomePage : System.Web.UI.Page
    {
        BooksCatalogueServiceClient client;
        public OnlineBookCatalogueHomePage()
        {
            client = new BooksCatalogueServiceClient("WSHttpBinding_IBooksCatalogueService");   
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            AddNewBookButtonVisibility("block");
            AddNewBookFormVisibility("none");
            EditBookFormVisibility("none");
            if (!Page.IsPostBack)
            {
                BindGVAGetBooks(0, string.Empty);
                BindSearchCatalogue();
            }
            else
            {             
                hdnSearchCatalogue.Value = ddlKSearchCatalogue.SelectedValue;
                hdnSearchText.Value = txtSearchValue.Text;
            }
        }

        private void BindSearchCatalogue()
        {
            try
            {
                DataSet dataSet;
                dataSet = client.SearchCatalogue();

                if (!(dataSet.Tables[0].Rows.Count == 0))
                {
                    ddlKSearchCatalogue.DataSource = dataSet;
                    ddlKSearchCatalogue.DataTextField = "texts";
                    ddlKSearchCatalogue.DataValueField = "value";
                    ddlKSearchCatalogue.DataBind();
                    ddlKSearchCatalogue.Items.Insert(0, new ListItem("--Select Category--", "-1"));
                }
            }
            catch(FaultException<FaultExceptionHandler> faultException)
            {
                lblSelectCatalogue.Text = faultException.Detail.Error + "" + faultException.Detail.Details;
            }
        }

        private void BindGVAGetBooks(int ID, string SearchValue)
        {
            AddNewBookButtonVisibility("block");
            try
            {
                DataSet dataSet = client.GetAllBooks(ID, SearchValue);
                if (dataSet != null)
                {
                    if (!(dataSet.Tables[0].Rows.Count == 0) || dataSet != null)
                    {
                        gvOnlineCatalogue.DataSource = dataSet;
                        gvOnlineCatalogue.DataBind();
                        lblSelectCatalogue.Text = string.Empty;
                    }
                    else
                    {
                        lblSelectCatalogue.Text = Messages.NOBOOKSAVALIABLE;
                        lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
                        gvOnlineCatalogue.DataSource = null;
                        gvOnlineCatalogue.DataBind();
                    }
                }
                else
                {
                    lblSelectCatalogue.Text = Messages.NOBOOKSAVALIABLE;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
                    gvOnlineCatalogue.DataSource = null;
                    gvOnlineCatalogue.DataBind();
                }
            }
            catch (FaultException faultException)
            {
                lblSelectCatalogue.Text = faultException.Message;
            }
        }


        protected void btnEditBookForm_Click(object sender, EventArgs e)
        {
            EditBookFormVisibility("block");
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                row.BorderStyle = BorderStyle.Ridge;
                row.BorderColor = Color.DarkOliveGreen;

                txtEditName.Text = row.Cells[1].Text;
                txtEditAuthor.Text = row.Cells[2].Text;
                txtEditPrice.Text = row.Cells[3].Text;

                hdnBookID.Value = row.Cells[0].Text;
            }
        }

        protected void btnEditBook_Click(object sender, EventArgs e)
        {
            Books book = new Books()
            {
                ID = Convert.ToInt32(hdnBookID.Value),
                BookName = txtEditName.Text,
                Author = txtEditAuthor.Text,
                Price = Convert.ToDecimal(txtEditPrice.Text)
            };
            try
            {
                int result = client.SaveBook(book);

                BindGVAGetBooks(Convert.ToInt32(hdnSearchCatalogue.Value), hdnSearchText.Value);

                if (result > 0)
                {
                    lblSelectCatalogue.Text = Messages.EDITUPDATEBOOKSUCCESS;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.DarkOliveGreen;
                }
                else
                {
                    lblSelectCatalogue.Text = Messages.EDITUPDATEBOOKFAIL;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (FaultException faultException)
            {
                lblSelectCatalogue.Text = faultException.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int SearchCatalogueID = Convert.ToInt32(ddlKSearchCatalogue.SelectedValue);
            string SearchValue = txtSearchValue.Text;

            if (SearchCatalogueID == -1)
            {
                lblSelectCatalogue.Text = Messages.SEARCHCATALOGUE;
                lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                BindGVAGetBooks(SearchCatalogueID, SearchValue);
            }
           
        }

        protected void btnAddNewBook_Click(object sender, EventArgs e)
        {
            AddNewBookFormVisibility("block");

            txtBookName.Text = string.Empty;
            txtAuthorName.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }

        private void AddNewBookButtonVisibility(string display)
        {
            if (display == "block")
            {
                btnAddNewBook.Attributes.Add("style", "display:block");
            }
            else
            {
                btnAddNewBook.Attributes.Add("style", "display:none");
            }
        }

        private void AddNewBookFormVisibility(string display)
        {
            if (display == "block")
            {
                AddNewBookForm.Attributes.Add("style", "display:block");
            }
            else
            {
                AddNewBookForm.Attributes.Add("style", "display:none");
            }
        }

        private void EditBookFormVisibility(string display)
        {
            if (display == "block")
            {
                EditBookForm.Attributes.Add("style", "display:block");
            }
            else
            {
                EditBookForm.Attributes.Add("style", "display:none");
            }
        }

        protected void btnInsertBook_Click(object sender, EventArgs e)
        {
            Books books = new Books()
            {
                BookName = txtBookName.Text,
                Author = txtAuthorName.Text,
                Price = Convert.ToDecimal(txtPrice.Text)
            };
            try
            {
                int result = client.InsertNewBook(books);
                BindGVAGetBooks(Convert.ToInt32(hdnSearchCatalogue.Value), hdnSearchText.Value);
                if (result > 0)
                {
                    lblSelectCatalogue.Text = Messages.ADDNEWBOOKSUCCESSFUL;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.DarkOliveGreen;
                }
                else
                {
                    lblSelectCatalogue.Text = Messages.ADDNEWBOOKFAILED;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (FaultException faultException)
            {
                lblSelectCatalogue.Text = faultException.Message;
            }
        }

        protected void btnCancelNewBook_Click(object sender, EventArgs e)
        {
            BindGVAGetBooks(Convert.ToInt32( hdnSearchCatalogue.Value), hdnSearchText.Value);
            lblSelectCatalogue.Text = string.Empty;
        }


        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            BindGVAGetBooks(Convert.ToInt32(hdnSearchCatalogue.Value), hdnSearchText.Value);
            lblSelectCatalogue.Text = string.Empty;
        }


        protected void btnDeleteBook_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32( hdnBookID.Value);

            try
            {
                int result = client.DeleteBook(ID);

                BindGVAGetBooks(Convert.ToInt32(hdnSearchCatalogue.Value), hdnSearchText.Value);

                if (result > 0)
                {
                    lblSelectCatalogue.Text = Messages.DELETEBOOKSUCCESS;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.DarkOliveGreen;
                }
                else
                {
                    lblSelectCatalogue.Text = Messages.DELETEBOOKFAIL;
                    lblSelectCatalogue.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (FaultException faultException)
            {
                lblSelectCatalogue.Text = faultException.Message;
            }
        }
    }
}