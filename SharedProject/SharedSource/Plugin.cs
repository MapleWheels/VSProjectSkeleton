namespace MyModName
{
    public partial class Plugin : IAssemblyPlugin
    {
        // These are automatically assigned by the plugin service after the Constructor is called
        public IConfigService ConfigService { get; set; }
        public IPluginManagementService PluginService { get; set; }
        public ILoggerService LoggerService { get; set; }
        
        public void Initialize()
        {
            // When your plugin is loading, use this instead of the constructor for code relying on
            // the services above.
            
            // Put any code here that does not rely on other plugins.
            LoggerService.Log($"MyModName Plugin Initialized. Welcome to modding!");
        }

        public void OnLoadCompleted()
        {
            // After all plugins have loaded
            // Put code that interacts with other plugins here.
        }

        public void PreInitPatching()
        {
            //Called right after the constructor
        }

        public void Dispose()
        {
            // Cleanup your plugin!
            throw new NotImplementedException();
        }
    }
}
