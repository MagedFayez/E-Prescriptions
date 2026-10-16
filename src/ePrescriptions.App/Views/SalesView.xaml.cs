using System.Windows.Controls;

namespace ePrescriptions.App.Views
{
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                if (e.NewValue is ViewModels.SalesViewModel vm)
                {
                    // لما الـ ViewModel يمسح كلمة الموافقة بعد المحاولة، بنفضّي الخانة كمان
                    vm.PropertyChanged += (_, args) =>
                    {
                        if (args.PropertyName == nameof(ViewModels.SalesViewModel.ApproverPassword) && vm.ApproverPassword.Length == 0)
                            ApproverPasswordBox.Clear();
                    };
                }
            };
        }

        // PasswordBox لا يدعم الربط المباشر: بننقل الكلمة للـ ViewModel أول بأول (ما بتتخزّن، وبتتمسح بعد كل محاولة)
        private void ApproverPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ViewModels.SalesViewModel vm && sender is PasswordBox box)
            {
                vm.ApproverPassword = box.Password;
            }
        }
    }
}
