using KabeGami.Desktop.ViewModels.Images;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace KabeGami.Desktop.Views.Images.Common
{
    /// <summary>
    /// Interaction logic for ImageViewerUserControl.xaml
    /// </summary>
    public partial class ImageViewerUserControl : UserControl
    {
        private readonly ImageViewerViewModel _viewModel;
        public Guid ImageViewerGuid
        {
            get => (Guid)GetValue(ImageViewerGuidProperty);
            set => SetValue(ImageViewerGuidProperty, value);
        }
        public static readonly DependencyProperty ImageViewerGuidProperty =
            DependencyProperty.Register(
                nameof(ImageViewerGuid),
                typeof(Guid),
                typeof(ImageViewerUserControl),
                new PropertyMetadata(Guid.Empty, OnTypeChanged));

        public bool EnableRightClick
        {
            get => (bool)GetValue(EnableRightClickProperty);
            set => SetValue(EnableRightClickProperty, value);
        }
        public static readonly DependencyProperty EnableRightClickProperty =
            DependencyProperty.Register(
                nameof(EnableRightClick),
                typeof(bool),
                typeof(ImageViewerUserControl),
                new PropertyMetadata(true));

        public ImageViewerUserControl()
        {
            InitializeComponent();
            _viewModel = App.Container.GetRequiredService<ImageViewerViewModel>();
            DataContext = _viewModel;
        }

        private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageViewerUserControl userControl)
            {
                userControl.InitializeImageViewerGuid();
            }
        }

        private void InitializeImageViewerGuid()
        {
            _viewModel.InitializeImageViewerGuid(ImageViewerGuid);
        }
    }
}
