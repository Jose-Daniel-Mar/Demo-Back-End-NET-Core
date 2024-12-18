namespace MarCorp.DemoBack.Services.WebApi.Helpers
{
    /// <summary>
    /// Represents the application settings required for authentication and authorization.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the secret key used for token encryption.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience for the token.
        /// </summary>
        public string Audience { get; set; }
    }
}
