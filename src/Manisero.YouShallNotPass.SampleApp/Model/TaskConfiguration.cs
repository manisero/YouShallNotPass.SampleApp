using System.Collections.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Model
{
    public class TaskConfiguration
    {
        /// <summary>Corresponding Algorithm*Configuration cannot be null.</summary>
        public Algorithm Algorithm { get; set; }

        /// <summary>Should be greater than or equal to 0.</summary>
        public double? Algorithm2Parameter { get; set; }
        public Algorithm3Configuration Algorithm3Configuration { get; set; }
        public Algorithm4Configuration Algorithm4Configuration { get; set; }
    }

    public enum Algorithm
    {
        /// <summary>No parameters.</summary>
        Algorithm1 = 1,

        /// <summary>Single parameter.</summary>
        Algorithm2,

        /// <summary>Parameters: Algorithm3Configuration.</summary>
        Algorithm3,

        /// <summary>Parameters: Algorithm4Configuration.</summary>
        Algorithm4
    }

    public class Algorithm3Configuration
    {
        /// <summary>If specified, should be greater than or equal to 0.</summary>
        public int? Parameter { get; set; }

        /// <summary>If <see cref="Parameter"/> specified, should be greater than or equal to 0.</summary>
        public double? DependentParameter { get; set; }
    }

    public class Algorithm4Configuration
    {
        /// <summary>Should be greater than 0.</summary>
        public int PhasesNumber { get; set; }

        /// <summary>
        /// Phase number -> phase configuration.
        /// Key should be between 1 and <see cref="PhasesNumber"/>.
        /// (Not all phases have to be configured - unconfigured phases will get some default configuration.)
        /// </summary>
        public Dictionary<int, Algorithm4PhaseConfiguration> Phases { get; set; }
    }
    
    public class Algorithm4PhaseConfiguration
    {
        /// <summary>Should be greater than or equal to 0.</summary>
        public int Parameter { get; set; }
    }
}
