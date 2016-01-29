using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaDownloadeur
{
    public partial class FenPrincipale : Form
    {
        private Dictionary<string, PluginInterface> plugins;

        public FenPrincipale()
        {
            InitializeComponent();

            plugins = new Dictionary<string, PluginInterface>();
            PluginLoader pl = new PluginLoader();
            ICollection<PluginInterface> items = pl.loadPlugins("Plugins");

            

            foreach (PluginInterface item in items)
            {
                plugins.Add(item.Site, item);
            }

            foreach (KeyValuePair<string, PluginInterface> item in plugins)
            {
                comboBox1.Items.Add(item.Key);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PluginInterface pl = null;
            plugins.TryGetValue(comboBox1.SelectedItem.ToString(), out pl);

            List<string> links = null;
            List<string> chaptersList = null;
            List<string> selectedChapters = null;

            if(pl != null)
            {
                chaptersList = pl.getChapterList(textBox1.Text);
                ChapterList fen = new ChapterList(chaptersList);  
            }

            if (!System.IO.Directory.Exists("Manga"))
            {
                System.IO.Directory.CreateDirectory("Manga");
            }

            foreach (string c in selectedChapters)
            {
                links = pl.getChapterLink(c);

                int i = 1;

                foreach (string link in links)
                {
                    WebClient w = new WebClient();
                    w.DownloadFile(link, ("Manga/"+c + i + ".jpg"));
                    i++;
                }
            }
        }
    }
}
