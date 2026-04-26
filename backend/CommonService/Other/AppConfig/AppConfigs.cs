using Microsoft.Extensions.Configuration;

namespace CommonService.Other.AppConfig
{
    internal class AppConfigs : IAppConfigs
    {
        private readonly IConfiguration _configuration;
        private readonly FairBaseConfigs _fairBaseConfigs = new FairBaseConfigs();
        private readonly ConnectionStrings _connectionStrings = new ConnectionStrings();
        private readonly FilesUrls _filesUrls = new FilesUrls();
        private readonly JWTConfigs _jWTConfigs = new JWTConfigs();
        private readonly DeepLinksConfigs _deepLinksConfigs = new DeepLinksConfigs();
        private readonly AppUrlConfigs _appUrlConfigs = new AppUrlConfigs();
        public AppConfigs(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._configuration.GetSection(nameof(connectionStrings)).Bind(_connectionStrings);
            this._configuration.GetSection(nameof(fairBaseConfigs)).Bind(this._fairBaseConfigs);
            this._configuration.GetSection(nameof(filesUrls)).Bind(this._filesUrls);
            this._configuration.GetSection(nameof(jWTConfigs)).Bind(this._jWTConfigs);
            this._configuration.GetSection(nameof(deepLinksConfigs)).Bind(this._deepLinksConfigs);
            this._configuration.GetSection(nameof(appUrlConfigs)).Bind(this._appUrlConfigs);
        }
        public FairBaseConfigs fairBaseConfigs { get => this._fairBaseConfigs; }
        public ConnectionStrings connectionStrings { get => this._connectionStrings; }
        public FilesUrls filesUrls { get => this._filesUrls; }
        public JWTConfigs jWTConfigs { get => this._jWTConfigs; }
        public DeepLinksConfigs deepLinksConfigs { get => this._deepLinksConfigs; }
        public AppUrlConfigs appUrlConfigs { get => this._appUrlConfigs; }

    }
}
