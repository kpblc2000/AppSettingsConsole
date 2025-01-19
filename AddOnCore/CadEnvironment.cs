namespace AddOnCore
{
    public class CadEnvironment
    {
        public CadEnvironment(string Name, string Version, string Localization)
        {
            this.Name = Name;
            this.Version = Version;
            this.Localization = Localization;
        }

        public string Name { get; }
        public string Version { get; }
        public string Localization { get; }
    }
}
