using System.Collections.Generic;

namespace Manisero.YouShallNotPass.SampleApp.Model
{
    public class TaskConfiguration
    {
        /// <summary>Corresponding Algorithm*Configuration cannot be null. Other configurations should be null.</summary>
        public Algorithm Algorithm { get; set; }

        /// <summary>
        /// If Algorithm == Algorithm2:
        ///   - should not be null,
        ///   - should be greater than or equal to 0.
        /// Else, should be null;
        /// </summary>
        public double? Algorithm2Parameter { get; set; }

        /// <summary>If Algorithm == Algorithm3, should not be null. Else, should be null.</summary>
        public Algorithm3Configuration Algorithm3Configuration { get; set; }

        /// <summary>If Algorithm == Algorithm4, should not be null. Else, should be null./// </summary>
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
        /// <summary>Each item should be greater than 0.</summary>
        public int[] Vector { get; set; }

        /// <summary>Should be greater than 0.</summary>
        public int PhasesNumber { get; set; }

        /// <summary>
        /// Phase number -> phase configuration.
        /// If not null, key should be between 1 and <see cref="PhasesNumber"/>.
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
