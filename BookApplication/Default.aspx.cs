using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookApplication
{
    public partial class _Default : Page
    {
        const string BooksSession = "Books";

        #region events

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialiseBookGridView();
            }
        }

        /// <summary>
        /// Processes the user deleting a row event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {            
            var bookId = int.Parse(BookGridView.Rows[e.RowIndex].Cells[0].Text);
            var books = (IList<Book>)Session[BooksSession];

            if (books.Any(x => x.ID == bookId))
            {
                books.Remove(books.First(x => x.ID == bookId));
                Session[BooksSession] = books;
            }

            BindBookGridView();
        }

        /// <summary>
        /// Processes when a user clicks to edit a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BookGridView.EditIndex = e.NewEditIndex;
            BindBookGridView();
        }

        /// <summary>
        /// Processes the row update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            UpdateBookInSession(e);
            BookGridView.EditIndex = -1;
            BindBookGridView();
        }              

        /// <summary>
        /// Processes the cancel edit event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BookGridView.EditIndex = -1;
            BindBookGridView();
        }

        /// <summary>
        /// Process the add new book click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewRowButton_OnClick(object sender, EventArgs e)
        {
            AddBookToSession();
            ClearInputTextBoxes();
            BindBookGridView();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Update the book
        /// </summary>
        /// <param name="books">the list of books</param>
        /// <param name="textId">ID of book to be updated</param>
        /// <param name="textTitle">updated title</param>
        /// <param name="textAuthor">updated author</param>
        /// <param name="textPrice">updated price</param>
        private void UpdateBook(IList<Book> books, string textId, string title, string author, string textPrice)
        {
            var bookId = int.Parse(textId);
            var book = books.First(x => x.ID == bookId);
            book.Title = title;
            book.Author = author;
            book.Price = decimal.Parse(textPrice);
        }

        /// <summary>
        /// Initialise the grid using the json data file
        /// </summary>
        private void InitialiseBookGridView()
        {
            IDataProvider dataProvider = new DataProvider();
            var books = dataProvider.GetBooks();
            Session[BooksSession] = books;
            BookGridView.DataSource = books;
            BookGridView.DataBind();
        }

        /// <summary>
        /// Bind the book grid to the session
        /// </summary>
        private void BindBookGridView()
        {
            BookGridView.DataSource = Session[BooksSession];
            BookGridView.DataBind();
        }

        /// <summary>
        /// Create Book
        /// </summary>
        /// <param name="id">book id</param>
        /// <param name="title">the title</param>
        /// <param name="author">the author</param>
        /// <param name="price">the price</param>
        /// <returns>new book</returns>
        private Book CreateBook(int id, string title, string author, decimal price)
        {
            return new Book
            {
                ID = id,
                Title = title,
                Author = author,
                Price = price
            };
        }

        /// <summary>
        /// Clear the input text boxes
        /// </summary>
        private void ClearInputTextBoxes()
        {
            AddTitleTextBox.Text = string.Empty;
            AddAuthorTextBox.Text = string.Empty;
            AddPriceTextBox.Text = string.Empty;
        }
        
        /// <summary>
        /// Use the input text boxes to add the new book to the session
        /// </summary>
        private void AddBookToSession()
        {
            var title = AddTitleTextBox.Text;
            var author = AddAuthorTextBox.Text;
            var price = decimal.Parse(AddPriceTextBox.Text);
            var books = (IList<Book>)Session[BooksSession];            
            var maxId = books.Any() ? books.Max(x => x.ID) : 1;
            var newBook = CreateBook(maxId + 1, title, author, price);
            books.Add(newBook);
            Session[BooksSession] = books;
        }

        /// <summary>        
        /// Update the book by using the edited row
        /// </summary>
        /// <param name="e"></param>
        private void UpdateBookInSession(GridViewUpdateEventArgs e)
        {
            var row = BookGridView.Rows[e.RowIndex];

            var updateId = row.Cells[0].Text;
            var updateTitle = ((TextBox)row.Cells[1].Controls[0]).Text;
            var updateAuthor = ((TextBox)row.Cells[2].Controls[0]).Text;
            var updatePrice = ((TextBox)row.Cells[3].Controls[0]).Text;

            var books = (IList<Book>)Session[BooksSession];
            UpdateBook(books, updateId, updateTitle, updateAuthor, updatePrice);
            Session[BooksSession] = books;
        }

        #endregion
    }
}