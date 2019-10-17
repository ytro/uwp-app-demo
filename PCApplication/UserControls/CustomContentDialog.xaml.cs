using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace PCApplication.UserControls {
    /// <summary>
    /// A custom content dialog (modal window) with streamlined formatting. Can be used to display messages (such as errors) to the user.
    /// </summary>
    public partial class CustomContentDialog : ContentDialog {
        public CustomContentDialog() {
            InitializeComponent();
        }

        public CustomContentDialog(string message, string title) {
            InitializeComponent();

            Message = message;
            Title = title;
            PrimaryButtonText = "OK";
        }

        public string Message {
            get {
                return MessageLabel.ToString();
            }
            set {
                MessageLabel.Text = value;
            }
        }

        // Spawns a content dialog (modal)
        public static async Task<bool> ShowAsync(string message, string title = null, string primary = null, string secondary = null) {
            var dialog = new CustomContentDialog();
            dialog.Title = title;
            dialog.Message = message;
            dialog.PrimaryButtonText = primary ?? string.Empty;
            dialog.SecondaryButtonText = secondary ?? string.Empty;

            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
    }
}
