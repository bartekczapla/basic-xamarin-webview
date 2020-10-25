
namespace XamarinBasic.Core
{
    public interface IPlatformSettingsProvider
    {
        string ConnectionString { get; }

        string Platform { get;  }
    }
}
