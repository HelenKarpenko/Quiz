using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.DTO.UserResult;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.DAL.Entities;
using Quiz.DAL.Entities.UserResults;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.BLL.Services
{
  public class TestService : ITestService
  {
    private readonly IUnitOfWork _database;

    private IMapper _mapper;

    public TestService(IUnitOfWork uow)
    {
      _database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

      _mapper = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<QuestionDTO, Question>();
        cfg.CreateMap<Question, QuestionDTO>()
          .ForMember(qDto => qDto.Answers, opt => opt.MapFrom(q => q.Answers));
        cfg.CreateMap<AnswerDTO, Answer>();
        cfg.CreateMap<Answer, AnswerDTO>();
        cfg.CreateMap<TestDTO, Test>();
        cfg.CreateMap<Test, TestDTO>()
          .ForMember(tDto => tDto.Questions, opt => opt.MapFrom(t => t.Questions));
        cfg.CreateMap<TestResult, TestResultDTO>();
        cfg.CreateMap<TestResultDTO, TestResult>();
        cfg.CreateMap<ResultDetails, ResultDetailsDTO>();
        cfg.CreateMap<ResultDetailsDTO, ResultDetails>();
      })
      .CreateMapper();
    }

    public async Task<TestDTO> Create(TestDTO testDTO)
    {
      if (testDTO == null)
        throw new ArgumentNullException("TestDTO must not be null.");

      Test test = _mapper.Map<TestDTO, Test>(testDTO);

      Test createdTest = _database.Tests.Create(test);

      await _database.SaveAsync();

      TestDTO returnedTest = _mapper.Map<Test, TestDTO>(createdTest);

      return returnedTest;
    }

    public async Task<TestDTO> Delete(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test id.");

      Test deletedTest = _database.Tests.Delete(id);

      if (deletedTest == null)
        throw new EntityNotFoundException($"Test with id = {id} not found.");

      await _database.SaveAsync();

      TestDTO returnedTest = _mapper.Map<Test, TestDTO>(deletedTest);

      return returnedTest;
    }

    public void Dispose()
    {
      _database.Dispose();
    }

    public async Task<TestDTO> Get(int id)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test id.");

      Test test = await _database.Tests.GetAsync(id);

      if (test == null)
        throw new EntityNotFoundException($"Test with id = {id} not found.");

      TestDTO returnedTest = _mapper.Map<Test, TestDTO>(test);

      return returnedTest;
    }
    
    public async Task<IEnumerable<TestDTO>> GetAll()
    {
      IEnumerable<Test> tests = await _database.Tests.GetAllAsync();

      List<TestDTO> returnedTests = _mapper.Map<IEnumerable<Test>, List<TestDTO>>(tests);

      return returnedTests;
    }

    public async Task<PagedResultDTO<TestDTO>> GetPaged(
      int page = 1,
      int pageSize = 10)
    {
      if (page <= 0)
        throw new ArgumentException("Page must be greater than zero.");
      if (pageSize <= 0)
        throw new ArgumentException("Page size must be greater than zero.");

      IEnumerable<Test> tests = await _database.Tests.GetAllAsync();

      int skip = pageSize * (page - 1);
      int total = tests.Count();

      var result = tests
        .OrderBy(t => t.Id)
        .Skip(skip)
        .Take(pageSize);
      
      List<TestDTO> testsDto = _mapper.Map<IEnumerable<Test>, List<TestDTO>>(result);

      return new PagedResultDTO<TestDTO>(testsDto, page,pageSize, total);
    }

    public async Task<TestDTO> Update(int id, TestDTO testDTO)
    {
      if (testDTO == null)
        throw new ArgumentNullException("TestDTO must not be null.");

      if (id <= 0)
        throw new ArgumentException("Incorrect test id.");

      testDTO.Id = id;

      Test test = _mapper.Map<TestDTO, Test>(testDTO);

      Test updatedTest = _database.Tests.Update(id, test);

      if (updatedTest == null)
        throw new EntityNotFoundException($"Test with id = {id} not found.");

      await _database.SaveAsync();

      TestDTO returnedTest = _mapper.Map<Test, TestDTO>(updatedTest);

      return returnedTest;
    }

    public async Task<TestResultDTO> SaveResult(int id, TestResultDTO testResultDTO)
    {
      if (id <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (testResultDTO == null)
        throw new ArgumentNullException("Test result nust not be null.");

      TestResult testResult = _mapper.Map<TestResultDTO, TestResult>(testResultDTO);

      testResult.Details = new List<ResultDetails>();
      foreach (KeyValuePair<int, int> answer in testResultDTO.Answers)
      {
        testResult.Details.Add(new ResultDetails { QuestionId = answer.Key, AnswerId = answer.Value });
      }

      TestResult savedTestResult = _database.TestResults.Create(testResult);
      await _database.SaveAsync();

      TestResultDTO returnedTestResult = _mapper.Map<TestResult, TestResultDTO>(savedTestResult);

      foreach (var answer in savedTestResult.Details)
      {
        returnedTestResult.Answers[answer.QuestionId] = answer.AnswerId;
      }

      return returnedTestResult;
    }

    #region Question 

    public async Task<QuestionDTO> AddQuestion(int testId, QuestionDTO questionDTO)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionDTO == null)
        throw new ArgumentNullException("QuestionDTO must not be null.");

      Question question = _mapper.Map<QuestionDTO, Question>(questionDTO);
      Question returnedQuestion = _database.Questions.Create(question);
      await _database.SaveAsync();

      QuestionDTO returnedQuestionDTO = _mapper.Map<Question, QuestionDTO>(returnedQuestion);
      return returnedQuestionDTO;
    }

    public async Task<QuestionDTO> DeleteQuestion(int testId, int questionId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");

      Question returnedQuestion = _database.Questions.Delete(questionId);
      await _database.SaveAsync();

      QuestionDTO returnedQuestionDTO = _mapper.Map<Question, QuestionDTO>(returnedQuestion);
      return returnedQuestionDTO;
    }

    public async Task<QuestionDTO> GetQuestion(int testId, int questionId)
    {
      return null;
      //if (testId <= 0)
      //  throw new ArgumentException("Incorrect test id.");
      //if (questionId <= 0)
      //  throw new ArgumentException("Incorrect question id.");

      //Question question = await _database.Questions.FindAsync(q => q.Id == questionId && q.TestId == testId).Id;
      //if (question == null)
      //  throw new EntityNotFoundException($"Test with id = {questionId} not found.");

      //QuestionDTO questionDTO = _mapper.Map<Question, QuestionDTO>(question);
      //return questionDTO;
    }

    public async Task<IEnumerable<QuestionDTO>> GetAllQuestions(int testId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");

      TestDTO testDTO = await Get(testId);

      return testDTO.Questions;
    }

    public async Task<QuestionDTO> UpdateQuestion(int testId, int questionId, QuestionDTO questionDTO)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");
      if (questionDTO == null)
        throw new ArgumentNullException("QuestionDTO must not be null.");

      questionDTO.Id = questionId;
      Question question = _mapper.Map<QuestionDTO, Question>(questionDTO);
      Question updateQuestion = _database.Questions.Update(questionId, question);
      if (updateQuestion == null)
        throw new EntityNotFoundException($"Question with id = {questionId} not found.");

      if (updateQuestion.TestId != testId)
        throw new EntityNotFoundException($"Test doesnt have question with id = {questionId}.");

      await _database.SaveAsync();

      QuestionDTO returnedQuestionDTO = _mapper.Map<Question, QuestionDTO>(updateQuestion);
      return returnedQuestionDTO;
    }

    #endregion

    #region Answer

    public async Task<AnswerDTO> AddAnswerToQuestion(int testId, int questionId, AnswerDTO answerDTO)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");
      if (answerDTO == null)
        throw new ArgumentNullException("AnswerDTO must not be null.");

      if (!TestСontainsQuestion(testId, questionId))
        throw new DoesNotContainException($"Test with id{testId} doesnt have question with id{questionId}");

      answerDTO.QuestionId = questionId;
      Answer answer = _mapper.Map<AnswerDTO, Answer>(answerDTO);
      Answer returnedAnswer = _database.Answers.Create(answer);

      await _database.SaveAsync();

      AnswerDTO returnedAnswerDTO = _mapper.Map<Answer, AnswerDTO>(returnedAnswer);
      return returnedAnswerDTO;
    }

    public async Task<AnswerDTO> DeleteAnswerFromQuestion(int testId, int questionId, int answerId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");
      if (answerId <= 0)
        throw new ArgumentException("Incorrect answer id.");

      if (!TestСontainsQuestion(testId, questionId))
        throw new DoesNotContainException($"Test with id{testId} doesnt have question with id{questionId}");

      if (!QuestionСontainsAnswer(questionId, answerId))
        throw new DoesNotContainException($"Question with id:{questionId} doesnt have answer with id:{answerId}");

      Answer returnedAnswer = _database.Answers.Delete(answerId);

      await _database.SaveAsync();

      AnswerDTO returnedAnswerDTO = _mapper.Map<Answer, AnswerDTO>(returnedAnswer);
      return returnedAnswerDTO;
    }

    public async Task<AnswerDTO> GetAnswerFromQuestion(int testId, int questionId, int answerId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");
      if (answerId <= 0)
        throw new ArgumentException("Incorrect answer id.");

      if (!TestСontainsQuestion(testId, questionId))
        throw new DoesNotContainException($"Test with id:{testId} doesnt have question with id:{questionId}");
      if (!QuestionСontainsAnswer(questionId, answerId))
        throw new DoesNotContainException($"Question with id:{questionId} doesnt have answer with id:{answerId}");

      Answer answer = await _database.Answers.GetAsync(answerId);
      if (answer == null)
        throw new EntityNotFoundException($"Answer with id = {answerId} not found.");

      AnswerDTO answerDTO = _mapper.Map<Answer, AnswerDTO>(answer);

      return answerDTO;
    }

    public async Task<IEnumerable<AnswerDTO>> GetAllAnswersFromQuestion(int testId, int questionId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");

      if (!TestСontainsQuestion(testId, questionId))
        throw new DoesNotContainException($"Test with id:{testId} doesnt have question with id:{questionId}");

      QuestionDTO question = await GetQuestion(testId, questionId);

      return question.Answers;
    }

    public async Task<AnswerDTO> UpdateAnswerFromQuestion(int testId, int questionId, int answerId, AnswerDTO answerDTO)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");
      if (answerId <= 0)
        throw new ArgumentException("Incorrect answer id.");
      if (answerDTO == null)
        throw new ArgumentNullException("AnswerDTO must not be null.");

      if (!TestСontainsQuestion(testId, questionId))
        throw new DoesNotContainException($"Test with id:{testId} doesnt have question with id:{questionId}");
      if (!QuestionСontainsAnswer(questionId, answerId))
        throw new DoesNotContainException($"Question with id:{questionId} doesnt have answer with id:{answerId}");

      answerDTO.Id = questionId;

      Answer answer = _mapper.Map<AnswerDTO, Answer>(answerDTO);

      Answer updateAnswer = _database.Answers.Update(answerId, answer);
      if (updateAnswer == null)
        throw new EntityNotFoundException($"Answer with id = {questionId} not found.");

      await _database.SaveAsync();

      AnswerDTO returnedAnswerDTO = _mapper.Map<Answer, AnswerDTO>(updateAnswer);
      return returnedAnswerDTO;
    }

    #endregion

    private bool TestСontainsQuestion(int testId, int questionId)
    {
      if (testId <= 0)
        throw new ArgumentException("Incorrect test id.");

      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");

      Test test = _database.Tests.Get(testId);
      return test.Questions.Where(q => q.Id == questionId).First() != null;
    }

    private bool QuestionСontainsAnswer(int questionId, int answerId)
    {
      if (questionId <= 0)
        throw new ArgumentException("Incorrect question id.");

      if (answerId <= 0)
        throw new ArgumentException("Incorrect answer id.");

      Question question = _database.Questions.Get(questionId);
      return question.Answers.Where(a => a.Id == answerId).First() != null;
    }
  }
}
