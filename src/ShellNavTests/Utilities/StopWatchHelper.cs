﻿using AndreasReitberger.Shared.Core.EventLogger;
using AndreasReitberger.Shared.Core.Interfaces;
using System.Diagnostics;

namespace ShellNavTests.Utilities
{
    public static class StopWatchHelperOld
    {
        #region Methods
#nullable enable
        public static void Start(IDispatcher dispatcher, ref Stopwatch stopwatch, string methodName, IEventManager? eventManager = null, bool log = false)
        {
            stopwatch?.Start();
            string msg = $"Performance: Start => {methodName}: {DateTime.Now})";
            dispatcher?.Dispatch(() =>
            {
                if (log) eventManager?.LogInfo(new AppInfoEvent() { Message = msg, SourceName = $"{nameof(StopWatchHelperOld)}.{nameof(Start)}" });
                Debug.WriteLine(msg);
            });
        }
        public static async Task StartAsync(IDispatcher dispatcher, Stopwatch stopwatch, string methodName, IEventManager? eventManager = null, bool log = false)
        {
            stopwatch?.Start();
            string msg = $"Performance: Start => {methodName}: {DateTime.Now})";
            await dispatcher.DispatchAsync(() =>
            {
                if (log) eventManager?.LogInfo(new AppInfoEvent() { Message = msg, SourceName = $"{nameof(StopWatchHelperOld)}.{nameof(StartAsync)}" });
                Debug.WriteLine(msg);
            });
        }

        public static void Stop(IDispatcher dispatcher, ref Stopwatch stopwatch, string methodName, IEventManager? eventManager = null, bool log = false)
        {
            stopwatch?.Start();
            string msg = $"Performance: Done => {methodName}: {DateTime.Now} (Duration: {stopwatch?.Elapsed})";
            dispatcher?.Dispatch(() =>
            {
                if (log) eventManager?.LogInfo(new AppInfoEvent() { Message = msg, SourceName = $"{nameof(StopWatchHelperOld)}.{nameof(Stop)}" });
                Debug.WriteLine(msg);
            });
        }

        public static async Task StopAsync(IDispatcher dispatcher, Stopwatch stopwatch, string methodName, IEventManager? eventManager = null, bool log = false)
        {
            stopwatch?.Start();
            string msg = $"Performance: Done => {methodName}: {DateTime.Now} (Duration: {stopwatch?.Elapsed})";
            await dispatcher.DispatchAsync(() =>
            {
                if (log) eventManager?.LogInfo(new AppInfoEvent() { Message = msg, SourceName = $"{nameof(StopWatchHelperOld)}.{nameof(StopAsync)}" });
                Debug.WriteLine(msg);
            });
        }

        public static async Task DoTaskAndStopTimeAsync(IDispatcher dispatcher, string methodName, Func<Task> func, IEventManager? eventManager = null, bool logStart = false)
        {
            Stopwatch? watch = new();
            await StartAsync(dispatcher, watch, methodName, eventManager, logStart);
            try { await func.Invoke(); } catch (Exception ex) { eventManager?.LogError(ex); }
            await StopAsync(dispatcher, watch, methodName, eventManager);
        }

#nullable disable
        #endregion
    }
}
