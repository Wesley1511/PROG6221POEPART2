using System.Collections.Generic;

namespace POEPART2
{
    internal class QuizQuestion //my new quiz question class, which will be used to store the quiz questions and answers in memory
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}