using p5r.random.openings.Configuration;
using p5r.random.openings.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using System.Linq.Expressions;


#if DEBUG
using System.Diagnostics;
#endif

namespace p5r.random.openings
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {
        private IModLoader _modLoader =null!;

        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;

        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private ILogger _logger=null!;

        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;

        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;

        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;

        public Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;



#if DEBUG
            // Attaches debugger in debug mode; ignored in release.
            Debugger.Launch();
#endif

            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic
            string sourceFile = Path.Combine(_modLoader.GetDirectoryForModId(_modConfig.ModId), "Movies");
            string movFile = Path.Combine(_modLoader.GetDirectoryForModId(_modConfig.ModId), "CRIFsV2Hook", "CPK", "test", "Movie", "MOV050.USM");
            try{
                string[] movies = Directory.GetFiles(sourceFile);
                int moviesCount = movies.Length;
                _logger.WriteLine($"Number of openings found: [{moviesCount}]");
                if (moviesCount > 0)
                {

                    _logger.WriteLine("Openings: ");
                    foreach (string file in movies)
                    {
                        _logger.WriteLine(file);
                    }
                    Random rnd= new Random();
                    int r = rnd.Next(0, moviesCount);
                    _logger.WriteLine($"Random Opening Number: [{r}]");
                    _logger.WriteLine(movies[r]);
                    File.Copy(movies[r], movFile, true);
                }
                else
                {
                    _logger.WriteLine("Not files found in the folder");
                }
            }
            catch (DirectoryNotFoundException)
            {
                _logger.WriteLine("Folder not found", System.Drawing.Color.Red);
            }
            
            


            
            _logger.WriteLine("YUPI", System.Drawing.Color.Purple);

        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
        }
        #endregion
        /*public void Start(IModLoader loader)
        {
            _modLoader = (IModLoader)loader;
            _logger = (ILogger)_modLoader.GetLogger();

            

        }*/



        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}