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
        //property holding data about loaded components
        [ImportMany]
        public IEnumerable<Lazy<TransformationContract>> AvailableTransformations { get; protected set; }

        public void AssembleComponents()
        {
            //get components path
            var componentsPath = Directory.GetCurrentDirectory() + @"\" + "Components";
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
