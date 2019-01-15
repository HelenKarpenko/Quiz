using Quiz.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.BLL.Interfaces
{
	public interface ITestService
	{
		#region Test

		Task<TestDTO> Create(TestDTO test);

		TestDTO Get(int id);

		IEnumerable<TestDTO> GetAll();

		PagedResultDTO<TestDTO> GetPaged(string query, int page = 1, int pageSize = 10);

		Task<TestDTO> Update(TestDTO test);

		Task<TestDTO> Delete(int id);

		void Dispose();

		#endregion

		#region Question

		Task<QuestionDTO> AddQuestion(QuestionDTO questionDTO);

		Task<QuestionDTO> DeleteQuestion(int testId, int questionId);

		QuestionDTO GetQuestion(int testId, int questionId);

		IEnumerable<QuestionDTO> GetAllQuestions(int testId);

		Task<QuestionDTO> UpdateQuestion(QuestionDTO questionDTO);

		#endregion

		#region Answer

		Task<AnswerDTO> AddAnswerToQuestion(AnswerDTO answerDTO);

		Task<AnswerDTO> DeleteAnswerFromQuestion(int testId, int questionId, int answerId);

		AnswerDTO GetAnswerFromQuestion(int testId, int questionId, int answerId);

		IEnumerable<AnswerDTO> GetAllAnswersFromQuestion(int testId, int questionId);

		Task<AnswerDTO> UpdateAnswerFromQuestion(AnswerDTO answerDTO);

		#endregion
	}
}
