using System.ComponentModel;

namespace RubiksCube
{
    public sealed class LocalizationResourceManager : INotifyPropertyChanged
    {
        private LocalizationResourceManager() {
            CubeLang.Culture = CultureInfo.CurrentCulture;
        }

        public static LocalizationResourceManager Instance { get; } = new();

        public object this[string resourceKey]
            => CubeLang.ResourceManager.GetObject(resourceKey, CubeLang.Culture) ?? Array.Empty<byte>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetCulture(CultureInfo culture) {
            CubeLang.Culture = culture;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
