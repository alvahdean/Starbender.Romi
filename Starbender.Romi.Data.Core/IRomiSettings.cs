namespace Starbender.Romi.Data
{
    public interface IRomiSettings
    {
        /// <summary>
        /// The root path to the REST API
        /// </summary>
        string ApiRoot { get; set; }

        /// <summary>
        /// The API version number (appended to the ApiRoot path)
        /// </summary>
        string ApiVersion { get; set; }

        /// <summary>
        /// The local directory where the application will run from
        /// </summary>
        string ApplicationPath { get; set; }

        /// <summary>
        /// The directory where application data is stored
        /// </summary>
        string DataPath { get; set; }

        /// <summary>
        /// Primary Key
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The directory to which logs will be written
        /// </summary>
        string LogPath { get; set; }

        /// <summary>
        /// Host part of the service Url
        /// </summary>
        string ServiceHost { get; set; }

        /// <summary>
        /// The port that the service will listen
        /// </summary>
        int ServicePort { get; set; }
    }
}