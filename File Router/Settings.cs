using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Router {
    class Settings {
        public bool timedTransfers;
        public int timerTimeInMinutes;
        //Settings window
        public bool showNotificationOnMinimize;
        public bool startWithWindows;
        public bool startMinimized;

        public Settings() {
            timedTransfers = false;
            timerTimeInMinutes = 0;
            showNotificationOnMinimize = true;
            startWithWindows = false;
            startMinimized = false;
        }
    }
}
