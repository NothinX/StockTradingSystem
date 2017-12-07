using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using StockTradingSystem.Client.View.Control;
using StockTradingSystem.Client.ViewModel;

namespace StockTradingSystem.Client.Model.UI.Dialog
{
    public class DialogService : IDialogService
    {
        private readonly Dictionary<Guid, bool?> _dialogResult = new Dictionary<Guid, bool?>();

        private Grid _dialogsGrid;
        private Grid DialogsGrid => _dialogsGrid ?? (_dialogsGrid =
                                        Application.Current.MainWindow.GetDescendantFromName("DialogsGrid") as Grid);

        private (Guid, UIElement) ShowDialog(Func<Guid, UIElement> getGrid)
        {
            Messenger.Default.Send(new GenericMessage<bool>(true), MainViewModel.ShowDialog);
            var guid = Guid.NewGuid();
            var grid = getGrid(guid);
            lock (_dialogResult)
            {
                _dialogResult.Add(guid, null);
            }
            lock (DialogsGrid)
            {
                DialogsGrid.Children.Add(grid);
            }
            return (guid, grid);
        }

        private void CloseDialog(Guid guid, UIElement grid)
        {
            int gridCount;
            lock (DialogsGrid)
            {
                DialogsGrid.Children.Remove(grid);
                gridCount = DialogsGrid.Children.Count;
            }
            lock (_dialogResult)
            {
                _dialogResult.Remove(guid);
            }
            if (gridCount <= 0) Messenger.Default.Send(new GenericMessage<bool>(false), MainViewModel.ShowDialog);
        }

        private void ChangeDialogResult(Guid guid, bool result)
        {
            lock (_dialogResult)
            {
                _dialogResult[guid] = result;
            }
        }

        private bool? GetDialogResult(Guid guid)
        {
            lock (_dialogResult)
            {
                return _dialogResult[guid];
            }
        }

        private async Task WaitDialog(string message, string title, string buttonText = "确定", Action afterHideCallback = null)
        {
            (Guid g, UIElement mg) = ShowDialog(guid => MessageGrid.Show(ChangeDialogResult, guid, message, title, buttonText));
            while (true)
            {
                lock (_dialogResult)
                {
                    if (_dialogResult[g] != null) break;
                }
                await Task.Delay(1);
            }
            CloseDialog(g, mg);
            afterHideCallback?.Invoke();
        }

        private async Task<bool> WaitDialog(string message, string title, string buttonConfirmText = "确定", string buttonCancelText = "取消", Action<bool> afterHideCallback = null)
        {
            (Guid g, UIElement mg) = ShowDialog(guid => MessageGrid.Show(ChangeDialogResult, guid, message, title, buttonConfirmText, buttonCancelText));
            while (true)
            {
                lock (_dialogResult)
                {
                    if (_dialogResult[g] != null) break;
                }
                await Task.Delay(1);
            }
            var result = GetDialogResult(g) ?? false;
            CloseDialog(g, mg);
            afterHideCallback?.Invoke(result);
            return result;
        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return WaitDialog(message, title, buttonText, afterHideCallback);
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return WaitDialog(error.Message, title, buttonText, afterHideCallback);
        }

        public Task ShowMessage(string message, string title)
        {
            return WaitDialog(message, title, "确定", null);
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return WaitDialog(message, title, buttonText, afterHideCallback);
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return WaitDialog(message, title, buttonConfirmText, buttonCancelText, afterHideCallback);
        }

        public Task ShowMessageBox(string message, string title)
        {
            return ShowMessage(message, title);
        }
    }
}