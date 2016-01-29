using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloadeur
{
    public interface PluginInterface
    {
        string Site { get; }
        List<string> getChapterList(string link);
        List<string> getChapterLink(string link);
    }
}
