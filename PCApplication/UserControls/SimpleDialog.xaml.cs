using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;


namespace PCApplication.UserControls {
    /// <summary>
    /// A custom content dialog (modal window) with streamlined formatting. Can be used to display messages (such as errors) to the user.
    /// </summary>
    public partial class SimpleDialog : ContentDialog {
        public SimpleDialog() {
            InitializeComponent();
        }

        public string Message {
            get {
                return MessageLabel.ToString();
            }
            set {
                MessageLabel.Text = value;
            }
        }
    }

}
