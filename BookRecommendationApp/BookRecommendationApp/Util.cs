using BookRecommendationApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    class Util
    {
        private static int loadingInstanceAmount = 0;
        public static void StartLoadingForCursor()
        {
            loadingInstanceAmount++;
            Cursor.Current = Cursors.WaitCursor;
        }
        public static void StopLoadingForCursor()
        {
            if (loadingInstanceAmount > 0)
                loadingInstanceAmount--;
            else
                Database.PostError("Unknown semaphore error at loading cursor mechanic.");// semaphore error, should report
            if (loadingInstanceAmount <= 0)
                Cursor.Current = Cursors.Default;
        }

        private static HashSet<Task> taskList = new HashSet<Task>();
        private const int GCRate = 20000; // 20 sec
        private static Task taskGarbageCollector = new Task(async()=>
        {
            await Task.Delay(GCRate);
            HashSet<Task> toRemove = new HashSet<Task>();
            foreach (Task task in taskList)
                if (task.IsCompleted)
                    toRemove.Add(task);
            foreach (Task task in toRemove)
                taskList.Remove(task);

            // Error correction if all else fails
            if (taskList.Count <= 0)
                loadingInstanceAmount = 0;
        });
        public static void MarkLongRunningTask(Task task)
        {
            StartLoadingForCursor();

            taskList.Add(task.ContinueWith((t) => { StopLoadingForCursor(); }));
        }
    }
}
