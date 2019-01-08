using Quiz.BLL.DTO;
using Quiz.BLL.DTO.UserResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.BLL.Interfaces
{
  public interface ITestService
  {
    #region Test

    Task<TestDTO> Create(TestDTO test);

    Task<TestDTO> Get(int id);

    Task<IEnumerable<TestDTO>> GetAll();

    Task<PagedResultDTO<TestDTO>> GetPaged(int page = 1, int pageSize = 10);

    Task<TestDTO> Update(int id, TestDTO test);

    Task<TestDTO> Delete(int id);

    Task<TestResultDTO> SaveResult(int id, TestResultDTO testResultDTO);

    void Dispose();

    #endregion

    //TestDTO AddQuestion(int testId, QuestionDTO questionDTO);

    //TestDTO DeleteQuestion(int testId, int questionId);

    //QuestionDTO GetQuestion(int testId, int questionId);

    //IEnumerable<QuestionDTO> GetAllQuestions(int testId);

    //TestDTO UpdateQuestion(int testId, int questionId, QuestionDTO questionDTO);

    //#region Answer

    //TestDTO AddAnswerToQuestion(int testId, int questionId, AnswerDTO answerDTO);

    //TestDTO DeleteAnswerFromQuestion(int testId, int questionId, int answerId);

    //AnswerDTO GetAnswerFromQuestion(int testId, int questionId, int answerId);

    //IEnumerable<AnswerDTO> GetAllAnswersFromQuestion(int testId, int questionId);

    //TestDTO UpdateAnswerFromQuestion(int testId, int questionId, int answerId, AnswerDTO answerDTO);

    //#endregion
  }
}
