using System.Collections.Generic;

namespace POEPART2
{
    internal class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}