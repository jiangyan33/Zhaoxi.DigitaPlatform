namespace Zhaoxi.DigitaPlatform.Components
{
    /// <summary>
    /// AirCompressor.xaml 的交互逻辑
    /// </summary>
    public partial class AirCompressor : ComponentBase
    {
        public AirCompressor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteCommand?.Execute(DeleteParameter);
        }

        
    }
}
