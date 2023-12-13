using SoundBuddy.ViewModels;
using System.Windows.Controls;

namespace SoundBuddy.Views
{
    public partial class SelectedPlaylistPage : Page
    {
        private readonly PageController _pageController;

        public SelectedPlaylistPage(PageController pageController)
        {
            InitializeComponent();

            _pageController = pageController;
        }
    }
}
