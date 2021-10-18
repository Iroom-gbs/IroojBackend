using System;
using System.Xml;

namespace IroojBackend.Models
{
    public static class DBModel
    {
        public static (long timeLimit, long memoryLimit, long testCaseCount) GetQuestionInfo(long questionNumber)
        {
            var d = GetProblemData(questionNumber);
            return (d.TimeLimit, d.MemoryLimit, d.TestCaseCount);
        }
        public static void ApplyData(long judgeNumber, string result)
        {
            // TODO
        }
        public static string GetGradData(long judgeNumber)
        {
            // TODO
            return "";
        }
        public static string GetCurrentGradInfo()
        {
            // TODO
            return "";
        }
        public static long GetJudgeNumber()
        {
            // TODO
            return 0;
        }
        public static ProblemData GetProblemData(long questionNumber)
        {
            // TODO
            return new ProblemData()
            {
                TimeLimit = 1000,
                MemoryLimit = 512 * 1024,
                HtmlData = "<html></html>",
                TestCaseCount = 2
            };
        }
        public static bool CheckAuth(string id, string encryptedPassword)
        {
            // TODO
            return true;
        }
        public static bool CreateAuth(string UserData)
        {
            try
            {
                // TODO
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}