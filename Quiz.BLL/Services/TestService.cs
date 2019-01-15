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

		public TestDTO Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect test id.");

			Test test = _database.Tests.Get(id);

			if (test == null)
				throw new EntityNotFoundException($"Test with id = {id} not found.");

			TestDTO returnedTest = _mapper.Map<Test, TestDTO>(test);

			return returnedTest;
		}

		public IEnumerable<TestDTO> GetAll()
		{
			IEnumerable<Test> tests = _database.Tests.GetAll().ToList();

			List<TestDTO> returnedTests = _mapper.Map<IEnumerable<Test>, List<TestDTO>>(tests);

			return returnedTests;
		}

		public PagedResultDTO<TestDTO> GetPaged(
															string query,
															int page = 1,
															int pageSize = 10)
		{
			if (query == null)
				throw new ArgumentNullException("Name must not be null.");
			if (page <= 0)
				throw new ArgumentException("Page must be greater than zero.");
			if (pageSize <= 0)
				throw new ArgumentException("Page size must be greater than zero.");

			IQueryable<Test> tests;

			if (string.IsNullOrEmpty(query))
			{
				tests = _database.Tests.GetAll();
			}
			else
			{
				tests = _database.Tests.GetAll().Where(t => t.Name.ToLower().Contains(query.ToLower()));
			}
			
			int skip = pageSize * (page - 1);
			int total = tests.Count();

			var result = tests
				.OrderBy(t => t.Id)
				.Skip(skip)
				.Take(pageSize)
				.ToList();

			List<TestDTO> testsDto = _mapper.Map<List<Test>, List<TestDTO>>(result.ToList());

			return new PagedResultDTO<TestDTO>(testsDto.ToList(), page, pageSize, total);
		}

		public async Task<TestDTO> Update(TestDTO testDTO)
		{
			if (testDTO == null)
				throw new ArgumentNullException("TestDTO must not be null.");
			
			Test test = _mapper.Map<TestDTO, Test>(testDTO);

			Test updatedTest = _database.Tests.Update(test.Id, test);

			if (updatedTest == null)
				throw new EntityNotFoundException($"Test with id = {test.Id} not found.");

			await _database.SaveAsync();

			TestDTO returnedTest = _mapper.Map<Test, TestDTO>(updatedTest);

			return returnedTest;
		}
		
		#region Question 

		public async Task<QuestionDTO> AddQuestion(QuestionDTO questionDTO)
		{
			if (questionDTO == null)
				throw new ArgumentNullException("QuestionDTO must not be null.");
			if (questionDTO.TestId <= 0)
				throw new ArgumentException("Incorrect test id.");

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

		public QuestionDTO GetQuestion(int testId, int questionId)
		{
			if (testId <= 0)
				throw new ArgumentException("Incorrect test id.");
			if (questionId <= 0)
				throw new ArgumentException("Incorrect question id.");

			Question question = _database.Questions.Find(q => q.Id == questionId && q.TestId == testId).FirstOrDefault();

			if (question == null)
				throw new EntityNotFoundException($"Test with id = {questionId} not found.");

			QuestionDTO questionDTO = _mapper.Map<Question, QuestionDTO>(question);

			return questionDTO;
		}

		public IEnumerable<QuestionDTO> GetAllQuestions(int testId)
		{
			if (testId <= 0)
				throw new ArgumentException("Incorrect test id.");

			TestDTO testDTO = Get(testId);

			return testDTO.Questions;
		}

		public async Task<QuestionDTO> UpdateQuestion(QuestionDTO questionDTO)
		{
			if (questionDTO == null)
				throw new ArgumentNullException("QuestionDTO must not be null.");
			if (questionDTO.TestId <= 0)
				throw new ArgumentException("Incorrect test id.");
			if (questionDTO.Id <= 0)
				throw new ArgumentException("Incorrect question id.");
			
			Question question = _mapper.Map<QuestionDTO, Question>(questionDTO);

			Question updateQuestion = _database.Questions.Update(question.Id, question);

			if (updateQuestion == null)
				throw new EntityNotFoundException($"Question with id = {question.Id} not found.");
			
			await _database.SaveAsync();

			QuestionDTO returnedQuestionDTO = _mapper.Map<Question, QuestionDTO>(updateQuestion);

			return returnedQuestionDTO;
		}

		#endregion

		#region Answer

		public async Task<AnswerDTO> AddAnswerToQuestion(AnswerDTO answerDTO)
		{
			if (answerDTO == null)
				throw new ArgumentNullException("AnswerDTO must not be null.");
			if (answerDTO.QuestionId <= 0)
				throw new ArgumentException("Incorrect question id.");
			
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
			
			Answer returnedAnswer = _database.Answers.Delete(answerId);

			await _database.SaveAsync();

			AnswerDTO returnedAnswerDTO = _mapper.Map<Answer, AnswerDTO>(returnedAnswer);

			return returnedAnswerDTO;
		}

		public AnswerDTO GetAnswerFromQuestion(int testId, int questionId, int answerId)
		{
			if (testId <= 0)
				throw new ArgumentException("Incorrect test id.");
			if (questionId <= 0)
				throw new ArgumentException("Incorrect question id.");
			if (answerId <= 0)
				throw new ArgumentException("Incorrect answer id.");
			
			Answer answer =  _database.Answers.Get(answerId);

			if (answer == null)
				throw new EntityNotFoundException($"Answer with id = {answerId} not found.");

			AnswerDTO answerDTO = _mapper.Map<Answer, AnswerDTO>(answer);

			return answerDTO;
		}

		public IEnumerable<AnswerDTO> GetAllAnswersFromQuestion(int testId, int questionId)
		{
			if (testId <= 0)
				throw new ArgumentException("Incorrect test id.");
			if (questionId <= 0)
				throw new ArgumentException("Incorrect question id.");
			
			QuestionDTO question = GetQuestion(testId, questionId);

			return question.Answers;
		}

		public async Task<AnswerDTO> UpdateAnswerFromQuestion(AnswerDTO answerDTO)
		{
			if (answerDTO == null)
				throw new ArgumentNullException("AnswerDTO must not be null.");
			if (answerDTO.QuestionId <= 0)
				throw new ArgumentException("Incorrect question id.");
			if (answerDTO.Id <= 0)
				throw new ArgumentException("Incorrect answer id.");
			
			Answer answer = _mapper.Map<AnswerDTO, Answer>(answerDTO);

			Answer updateAnswer = _database.Answers.Update(answer.Id, answer);

			if (updateAnswer == null)
				throw new EntityNotFoundException($"Answer with id = {answer.Id} not found.");

			await _database.SaveAsync();

			AnswerDTO returnedAnswerDTO = _mapper.Map<Answer, AnswerDTO>(updateAnswer);

			return returnedAnswerDTO;
		}

		#endregion
		
	}
}
