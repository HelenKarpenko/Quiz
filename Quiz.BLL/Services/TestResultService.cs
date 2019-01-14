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
	public class TestResultService : ITestResultService
	{
		private readonly IUnitOfWork _database;

		private IMapper _mapper;

		public TestResultService(IUnitOfWork uow)
		{
			_database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

			_mapper = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<TestResult, TestResultDTO>()
					.ForMember(rDTO => rDTO.MaxResult, map => map.MapFrom(r => r.Test.Questions.Count));
				cfg.CreateMap<TestResultDTO, TestResult>();
				cfg.CreateMap<ResultDetails, ResultDetailsDTO>();
				cfg.CreateMap<ResultDetailsDTO, ResultDetails>();
			})
			.CreateMapper();
		}

		public async Task<TestResultDTO> Create(TestResultDTO testResultDTO)
		{
			if (testResultDTO == null)
				throw new ArgumentNullException("TestDTO must not be null.");

			TestResult result = _mapper.Map<TestResultDTO, TestResult>(testResultDTO);

			result.Result = CalculateResult(_database.Tests.Get(result.TestId), testResultDTO.Details);

			TestResult createdTestResult = _database.TestResults.Create(result);

			await _database.SaveAsync();

			TestResultDTO returnedTestResult = _mapper.Map<TestResult, TestResultDTO>(createdTestResult);

			return returnedTestResult;
		}

		private int CalculateResult(Test test, IEnumerable<ResultDetailsDTO> answers)
		{
			int result = 0;
			
			foreach (var answer in answers)
			{
				if (test.Questions
						.Where(q => q.Id == answer.QuestionId)
						.First()
						.Answers
						.Where(a => a.IsCorrect)
						.First()
						.Id == answer.AnswerId)
				{
					result++;
				}
			}

			return result;
		}

		public async Task<TestResultDTO> Delete(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect result id.");

			TestResult deletedTestResult = _database.TestResults.Delete(id);

			if (deletedTestResult == null)
				throw new EntityNotFoundException($"Test result with id = {id} not found.");

			await _database.SaveAsync();

			TestResultDTO returnedTestresult = _mapper.Map<TestResult, TestResultDTO>(deletedTestResult);

			return returnedTestresult;
		}

		public void Dispose()
		{
			_database.Dispose();
		}

		public async Task<TestResultDTO> Get(int id)
		{
			if (id <= 0)
				throw new ArgumentException("Incorrect result id.");

			TestResult testResult = await _database.TestResults.GetAsync(id);

			if (testResult == null)
				throw new EntityNotFoundException($"Test result with id = {id} not found.");

			TestResultDTO returnedTestResult = _mapper.Map<TestResult, TestResultDTO>(testResult);

			return returnedTestResult;
		}

		public async Task<IEnumerable<TestResultDTO>> GetAll()
		{
			IEnumerable<TestResult> testResults = await _database.TestResults.GetAllAsync();

			List<TestResultDTO> returnedTestResults = _mapper.Map<IEnumerable<TestResult>, List<TestResultDTO>>(testResults);

			return returnedTestResults;
		}
	}
}
