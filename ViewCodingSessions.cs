using CodingTrackerConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTrackerConsoleApp {
    public static class ReadCodingSessions {
        public static void Read() {
            CodingRepository.Read();
            Program.MainMenu();
        }
    }
}
