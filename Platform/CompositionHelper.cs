using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using ComponentContract;

namespace Platform
{
    public class CompositionHelper
    {
        //property holding loaded components
        [ImportMany]
        public IEnumerable<Lazy<TransformationContract>> AvailableTransformations { get; protected set; }

        public void AssembleComponents()
        {
            //get components path
            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var pathFromUri = Uri.UnescapeDataString((new UriBuilder(codeBase)).Path);
            var path = Path.GetDirectoryName(pathFromUri);
            var componentsPath = path + @"\" + "Components";
            if (!Directory.Exists(componentsPath))
            {
                Directory.CreateDirectory(componentsPath);
            }

            //put all components into catalog
            var catalog = new DirectoryCatalog(componentsPath, "*.component.dll");

            //create container
            var container = new CompositionContainer(catalog);

            //compose parts
            container.ComposeParts(this);
        }
    }
}