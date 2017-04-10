using System;

namespace _8LMBackend
{
    public static class Logger
    {
        public static void SaveLog(string str){
            System.IO.File.WriteAllText(@"~\Projects\TestFolder\WriteText.txt", str);
        }
    }
}