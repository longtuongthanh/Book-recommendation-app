using BookRecommendationApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    public partial class frmSearch : Form
    {
        private string searchCriteria;
        public frmSearch()
        {
            InitializeComponent();
        }

        public string SearchCriteria
        {
            get => searchCriteria;
            set
            {
                searchCriteria = value;
                GetSearchResult();
            }
        }
        int MatchScore(Book book, string match)
        {
            string[] matchItems = match.Split(' ');

            string[] authorWords = book.Author.Split(' ');
            string[] descWords = book.Description.Split(' ');
            string[] nameWords = book.Name.Split(' ');
            List<string[]> tagWords = new List<string[]>();
            if (book.Tags != null)
                foreach (string tag in book.Tags)
                    tagWords.Add(tag.Split(' '));

            Int32 score = 0;

            foreach(string word in matchItems)
            {
                Int32 wordScore = 0;

                foreach (string authorWord in authorWords)
                    if (authorWord.ToLower() == word.ToLower())
                        wordScore = Math.Max(wordScore,
                            Database.Setting.SearchAuthorMatchCoefficient);
                foreach (string nameWord in nameWords)
                    if (nameWord.ToLower() == word.ToLower())
                        wordScore = Math.Max(wordScore,
                            Database.Setting.SearchNameMatchCoefficient);
                foreach (string descWord in descWords)
                    if (descWord.ToLower() == word.ToLower())
                        wordScore = Math.Max(wordScore,
                            Database.Setting.SearchDescriptionMatchCoefficient);

                string[] tagToRemove = null;
                foreach (string[] tag in tagWords)
                    foreach (string tagWord in tag)
                    {
                        if (tagWord.ToLower() == word.ToLower())
                            wordScore = Math.Max(wordScore,
                                Database.Setting.SearchTagMatchCoefficient);
                        tagToRemove = tag;
                    }
                tagWords.Remove(tagToRemove);

                score += wordScore;
            }

            return score;
        }
        void GetSearchResult()
        {
            List<Tuple<Book, Int32>> list = 
                new List<Tuple<Book, Int32>>(Database.Books.Select(
                    book => {
                        return new Tuple<Book, Int32>
                        (book, MatchScore(book, searchCriteria));
                    }));
            list.Sort((it, other) => { return other.Item2 - it.Item2; });
            int showAmount = Math.Min(list.Count, Database.Setting.SearchMaxResultAmount);
            for (int i = 0; i < showAmount; i++)
                if (list[i].Item2 > 0)
                {
                    Panel pal = new Panel()
                    {
                        Width = 350,
                        Height = 230
                    };

                    ApplyBookItem(pal, list[i].Item1);
                    flowLayoutPanel1.Controls.Add(pal);
                }
        }
        public void ClearSearchResult()
        {
            flowLayoutPanel1.Controls.Clear();
        }

        public void ApplyBookItem(Panel panel, Book book)
        {

            BookItem frmBI = new BookItem(
                book, SelectedBook, AddBook)
            { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(frmBI);
            frmBI.Show();
        }
        Action<Book> updateOnDBBookChange;
        private void frmSearch_Load(object sender, EventArgs e)
        {
            // No errors in case updateOnDBBookChange is null
            Firebase.Ins.onBookUpdate -= updateOnDBBookChange;
            updateOnDBBookChange = (book) =>
            {
                ClearSearchResult();
                GetSearchResult();
            };
            Firebase.Ins.onBookUpdate += updateOnDBBookChange;
        }
        private void SelectedBook(object sender, EventArgs e)
        {
            Panel panelLoad = (this.Parent as Panel);

            foreach (Control item in panelLoad.Controls)
                item.Dispose();

            panelLoad.Controls.Clear();

            BookInfo frmBI = new BookInfo(sender as Book) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }
        private void AddBook(object sender, EventArgs e)
        {
            Panel panelLoad = (this.Parent as Panel);

            foreach (Control item in panelLoad.Controls)
                item.Dispose();

            panelLoad.Controls.Clear();

            FormMyBooks frmBI = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            Book book = sender as Book;
            frmBI.FormBorderStyle = FormBorderStyle.None;
            panelLoad.Controls.Add(frmBI);
            frmBI.Show();
        }

        private void frmSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            Firebase.Ins.onBookUpdate -= updateOnDBBookChange;
        }
    }
}
