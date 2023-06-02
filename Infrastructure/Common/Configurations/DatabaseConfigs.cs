namespace Infrastructure.Common.Configurations
{
    public class DatabaseConfigs
    {
        public const string Key = "ConnectionStrings";
        public string HahnConnection { get; set; } = default!;
    }
}
