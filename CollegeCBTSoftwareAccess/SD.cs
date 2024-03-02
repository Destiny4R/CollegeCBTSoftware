using Medallion;

namespace CollegeCBTSoftwareAccess
{
    public static class SD
    {
        public static IList<int> ShuffleQuestions(List<int> questionsdbIds, int noofquestion)
        {
            return questionsdbIds.Shuffled().Take(noofquestion).ToArray();
        }
        public const string Role_Admin = "Admin";
        public const string Role_Student = "Student";
    }
}