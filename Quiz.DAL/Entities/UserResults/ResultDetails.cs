namespace Quiz.DAL.Entities.UserResults
{
	public class ResultDetails
	{
		public int Id { get; set; }

		public int QuestionId { get; set; }
		public Question Question { get; set; }

		public int? AnswerId { get; set; }
		public Answer Answer { get; set; }

		public int ResultId { get; set; }
		public TestResult Result { get; set; }
	}
}
