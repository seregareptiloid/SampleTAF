using System.Diagnostics;
using Serilog;

namespace CoreUi.Utils {
    public class Wait {
        private static readonly ILogger Logger = Log.ForContext(typeof(Wait));

        public static void For(Func<bool> waitFunc, int timeout = 5000, string timeoutMessage = "Timed out",
            int tick = 250) {
            For(waitFunc, () => timeoutMessage, timeout, tick);
        }

        private static void For(Func<bool> waitFunc, Func<string> errorMessageFunc, int timeout = 5000, int tick = 250) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try {
                while (stopwatch.ElapsedMilliseconds < timeout) {
                    try {
                        if (waitFunc()) {
                            return;
                        }
                    }
                    catch {
                        return;
                    }

                    Thread.Sleep(tick);
                }

                Log.Logger.Error(errorMessageFunc());
            }
            finally {
                stopwatch.Stop();
            }
        }

        public static void UntilEquals<T>(T expected, Func<T> actual, int timeout) {
            if (actual != null) {
                For(() => expected.Equals(actual()), timeout,
                    $"Value never changed to \"{expected}\". Actual: {actual()}");
            }
        }
    }
}
