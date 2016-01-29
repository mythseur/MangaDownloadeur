using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloadeur
{
    public class PluginLoader
    {
        public ICollection<PluginInterface> loadPlugins(string path)
        {
            string[] dllFilenames = null;
           
            if(System.IO.Directory.Exists(path))
            {
                dllFilenames = System.IO.Directory.GetFiles(path,"*.dll");

                ICollection<System.Reflection.Assembly> assemblies = new List<System.Reflection.Assembly>(dllFilenames.Length);

                foreach(string dllfilename in dllFilenames)
                {
                    System.Reflection.AssemblyName an = System.Reflection.AssemblyName.GetAssemblyName(dllfilename);
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(PluginInterface);
                ICollection<Type> pluginTypes = new List<Type>();

                foreach (System.Reflection.Assembly assembly in assemblies)
                {
                    if(assembly != null)
                    {
                        Type[] types = null;

                        try
                        {
                            types = assembly.GetTypes();
                        }
                        catch(System.Reflection.ReflectionTypeLoadException e)
                        {
                            types = e.Types;
                        }

                        foreach(Type type in types)
                        {
                            if(type != null)
                            {
                                if(type.IsInterface || type.IsAbstract)
                                {
                                    continue;
                                }
                                else
                                {
                                    if(type.GetInterface(pluginType.FullName) != null)
                                    {
                                        pluginTypes.Add(type);
                                    }
                                }
                            }
                        }
                    }
                }

                ICollection<PluginInterface> plugins = new List<PluginInterface>(pluginTypes.Count);

                foreach(Type type in pluginTypes)
                {
                    PluginInterface plugin = (PluginInterface)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            

            return null;
        }
    }
}
