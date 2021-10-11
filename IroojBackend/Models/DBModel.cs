namespace IroojBackend.Models
{
    public static class DBModel
    {
        public static (long timeLimit, long memoryLimit, long testCaseCount) GetQuestionInfo(long questionNumber)
        {
            return (1000, 512 * 1024, 2);
        }
    }
}