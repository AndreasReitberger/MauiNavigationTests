using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellNavTests.Models
{
    public static class DispatchManager
    {
        //Help: https://stackoverflow.com/questions/62154462/xamarin-difference-between-device-begininvokeonmainthread-and-mainthread-begini
        #region Properties
        public static void UpdateUIThreadSave(Action action, bool forceUiThread = false)
        {
            if (!MainThread.IsMainThread || forceUiThread)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static async Task UpdateUIThreadSaveAsync(Action action, bool forceUiThread = false)
        {
            if (!MainThread.IsMainThread || forceUiThread)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static async Task UpdateInNewTaskAsync(Action action)
        {
            await Task.Run(() =>
            {
                try
                {
                    action?.Invoke();
                }
                catch (Exception exc)
                {

                }
            });
        }

        public static void UpdateUIThreadSave(Func<Task> action, bool forceUiThread = false)
        {
            if (!MainThread.IsMainThread || forceUiThread)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static async Task UpdateUIThreadSaveAsync(Func<Task> action, bool forceUiThread = false)
        {
            if (!MainThread.IsMainThread || forceUiThread)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        await action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                await action?.Invoke();
            }
        }
        public static async Task UpdateInNewTaskAsync(Func<Task> action)
        {
            await Task.Run(async () =>
            {
                try
                {
                    await action?.Invoke();
                }
                catch (Exception exc)
                {

                }
            });
        }

        public static void Dispatch(IDispatcher dispatcher, Action action, bool forceUiThread = false)
        {
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static void Dispatch(IDispatcher dispatcher, Func<Task> action, bool forceUiThread = false)
        {
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                dispatcher.Dispatch(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static async Task DispatchAsync(IDispatcher dispatcher, Action action, bool forceUiThread = false)
        {
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                action?.Invoke();
            }
        }
        public static async Task DispatchAsync(IDispatcher dispatcher, Func<Task> action, bool forceUiThread = false)
        {
            if (dispatcher.IsDispatchRequired || forceUiThread)
            {
                await dispatcher.DispatchAsync(async () =>
                {
                    try
                    {
                        await action?.Invoke();
                    }
                    catch (Exception exc)
                    {

                    }
                });
            }
            else
            {
                await action?.Invoke();
            }
        }
        #endregion
    }
}
