using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Barotrauma;

namespace MyModName
{
    public partial class Plugin : IAssemblyPlugin
    {
        public void Initialize()
        {
            // When your plugin is loading, use this instead of the constructor
            // Put any code here that does not rely on other plugins.
            throw new NotImplementedException();
        }

        public void OnLoadCompleted()
        {
            // After all plugins have loaded
            // Put code that interacts with other plugins here.
        }

        public void Dispose()
        {
            // Cleanup your plugin!
            throw new NotImplementedException();
        }
    }
}
