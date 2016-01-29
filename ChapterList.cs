using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaDownloadeur
{
    public partial class ChapterList : Form
    {
        public ChapterList(List<string> chapterList)
        {
            InitializeComponent();

            foreach(string s in chapterList)
            {
                checkedListBox1.Items.Add(s);
            }
    }
}
