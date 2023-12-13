using SoundBuddy.ViewModels;
using System.Windows.Controls;

namespace SoundBuddy.Views
{
    public partial class SettingsPage : Page
    {
        private readonly PageController _pageController;

        public SettingsPage(PageController pageController)
        {
            InitializeComponent();

            _pageController = pageController;
        }
    }
}
