using PCApplication.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;

namespace PCApplication.Services {
    // A service to create Dialog modals
    // It can manage multiple simultaneous Dialog requests, needed due to UWP's single active dialog limitation
    public static class DialogService {

        static public async Task<bool> ShowAsync(string message, string title = null, string primary = null, string secondary = null) {
            var dialog = new SimpleDialog();
            dialog.Title = title;
            dialog.Message = message;
            dialog.PrimaryButtonText = primary ?? string.Empty;
            dialog.SecondaryButtonText = secondary ?? string.Empty;

            return await ShowAsync(dialog);
        }

        static public async Task<bool> ShowAsync(ContentDialog dialog) {
            TaskCompletionSource<object> currentDialogCompletion = new TaskCompletionSource<object>();
            TaskCompletionSource<object> previousDialogCompletion = null;

            // Get the currently active dialog, if exists, to await it
            previousDialogCompletion = DialogService.PreviousDialogCompletion;

            // Set this dialog to be the next awaitable dialog
            DialogService.PreviousDialogCompletion = currentDialogCompletion;

            // Await the currently active dialog
            if (previousDialogCompletion != null) {
                await previousDialogCompletion.Task;
            }

            var result = await dialog.ShowAsync();
            currentDialogCompletion.SetResult(null);
            return result == ContentDialogResult.Primary;
        }

        static private TaskCompletionSource<object> PreviousDialogCompletion = null;

    }
}
