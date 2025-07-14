using CommunityToolkit.Mvvm.ComponentModel;

namespace GlobalTikectAdminMobile.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private Guid _id;
        [ObservableProperty]
        private string _name = default!;

        public bool Equals(CategoryViewModel? other)
        {
            if (other is null) return false;
            return Id.Equals(other.Id);
        }
    }
}
