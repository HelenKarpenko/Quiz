using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Quiz.Web.API.Models.UserResult;
using Quiz.BLL.DTO.UserResult;

namespace Quiz.Web.API.Controllers
{
	[Authorize(Roles = "admin")]
	public class TestsController : ApiController
	{
		private readonly ITestService _testService;

		private IMapper _mapper;

		public TestsController(ITestService service)
		{
			_testService = service ?? throw new ArgumentNullException("Service must not be null.");

			_mapper = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<TestModel, TestDTO>();
				cfg.CreateMap<TestDTO, TestModel>();
				cfg.CreateMap<PagedResultDTO<TestDTO>.PagingInfo, PagedResultModel<TestModel>.PagingInfo>();
				cfg.CreateMap<PagedResultDTO<TestDTO>, PagedResultModel<TestModel>>();
				cfg.CreateMap<ResultDetailsModel, ResultDetailsDTO>();
				cfg.CreateMap<ResultDetailsDTO, ResultDetailsModel>();
				cfg.CreateMap<TestResultModel, TestResultDTO>();
				cfg.CreateMap<TestResultDTO, TestResultModel>();
			})
			.CreateMapper();
		}

		[HttpPost]
		public async Task<IHttpActionResult> Create(TestModel test)
		{
			if (test == null)
				return BadRequest("Test must not be null.");

			try
			{
				TestDTO testDTO = _mapper.Map<TestModel, TestDTO>(test);

				TestDTO createdTest = await _testService.Create(testDTO);

				TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(createdTest);

				string createdAtUrl = "http://www.google.com";

				return Created(createdAtUrl, returnedTest);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "user")]
		[HttpGet]
		public async Task<IHttpActionResult> GetById(int id)
		{
			if (id <= 0)
				return BadRequest("Incorrect test id.");

			try
			{
				TestDTO testDTO = await _testService.Get(id);

				TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

				return Ok(returnedTest);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "user")]
		[HttpGet]
		public async Task<IHttpActionResult> Get(
												 string query = "",
												 int page = 1,
												 int pageSize = 10)
		{

			string searchQuery = String.IsNullOrEmpty(query) ? "" : query;

			page = page > 0 ? page : 1;
			pageSize = pageSize > 0 ? pageSize : 10;

			PagedResultDTO<TestDTO> pagedResultDTO = await _testService.GetPaged(searchQuery, page, pageSize);

			PagedResultModel<TestModel> pagedResult = _mapper.Map<PagedResultDTO<TestDTO>, PagedResultModel<TestModel>>(pagedResultDTO);

			return Ok(pagedResult);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteById(int id)
		{
			if (id <= 0)
				return BadRequest("Incorrect test id.");

			try
			{
				TestDTO testDTO = await _testService.Delete(id);

				TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

				return Ok(returnedTest);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IHttpActionResult> UpdateById(int id, TestModel test)
		{
			if (test == null)
				return BadRequest("Test must not be null.");

			try
			{
				TestDTO testDTO = _mapper.Map<TestModel, TestDTO>(test);

				TestDTO updatedTest = await _testService.Update(id, testDTO);

				TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(updatedTest);

				return Ok(returnedTest);
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[Authorize(Roles = "user")]
		//[HttpPost]
		//[Route("api/tests/{testId}/results")]
		//public async Task<IHttpActionResult> SaveResult(int testId, TestResultModel result)
		//{
		//	if (testId <= 0)
		//		return BadRequest("Incorrect test id.");
		//	if (result == null)
		//		return BadRequest("Test result must not be null.");

		//	try
		//	{
		//		result.TestId = testId;
		//		result.UserId = User.Identity.GetUserId();
		//		result.PassageDate = DateTime.Now;

		//		TestResultDTO resultDTO = _mapper.Map<TestResultModel, TestResultDTO>(result);
		//		TestResultDTO savedResult = await _testService.SaveResult(testId, resultDTO);
		//		TestResultModel returnedResult = _mapper.Map<TestResultDTO, TestResultModel>(savedResult);
		//		return Ok(returnedResult);
		//	}
		//	catch (EntityNotFoundException)
		//	{
		//		return NotFound();
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}

		protected override void Dispose(bool disposing)
		{
			_testService.Dispose();
			base.Dispose(disposing);
		}

		//#region Question

		//[HttpPost]
		//[Route("api/tests/{testId}/questions")]
		//public IHttpActionResult AddQuestion(int testId, QuestionModel questionModel)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionModel == null)
		//    return BadRequest("Question must not be null.");

		//  try
		//  {
		//    QuestionDTO questionDTO = _mapper.Map<QuestionModel, QuestionDTO>(questionModel);

		//    TestDTO testDTO = _testService.AddQuestion(testId, questionDTO);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpDelete]
		//[Route("api/tests/{testId}/questions/{questionId}")]
		//public IHttpActionResult DeleteQuestion(int testId, int questionId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  try
		//  {

		//    TestDTO testDTO = _testService.DeleteQuestion(testId, questionId);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpGet]
		//[Route("api/tests/{testId}/questions/{questionId}")]
		//public IHttpActionResult GetQuestion(int testId, int questionId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  try
		//  {
		//    QuestionDTO questionDTO = _testService.GetQuestion(testId, questionId);

		//    QuestionModel returnedQuestion = _mapper.Map<QuestionDTO, QuestionModel>(questionDTO);

		//    return Ok(returnedQuestion);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpGet]
		//[Route("api/tests/{testId}/questions")]
		//public IHttpActionResult GetAllQuestions(int testId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  try
		//  {
		//    List<QuestionDTO> questions = _testService.GetAllQuestions(testId).ToList();

		//    List<QuestionModel> retunedQuestions = _mapper.Map<IEnumerable<QuestionDTO>, List<QuestionModel>>(questions);

		//    return Ok(retunedQuestions);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpPut]
		//[Route("api/tests/{testId}/questions/{questionId}")]
		//public IHttpActionResult UpdateQuestion(int testId, int questionId, QuestionModel questionModel)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  if (questionModel == null)
		//    return BadRequest("Question must not be null.");

		//  try
		//  {
		//    QuestionDTO questionDTO = _mapper.Map<QuestionModel, QuestionDTO>(questionModel);

		//    TestDTO testDTO = _testService.UpdateQuestion(testId, questionId, questionDTO);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//#endregion

		//#region Answer

		//[HttpPost]
		//[Route("api/tests/{testId}/questions/{questionId}/answers")]
		//public IHttpActionResult AddAnswerToQuestion(int testId, int questionId, AnswerModel answerModel)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");
		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");
		//  if (answerModel == null)
		//    return BadRequest("Answer must not be null.");

		//  try
		//  {
		//    AnswerDTO answerDTO = _mapper.Map<AnswerModel, AnswerDTO>(answerModel);

		//    TestDTO testDTO = _testService.AddAnswerToQuestion(testId, questionId, answerDTO);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpDelete]
		//[Route("api/tests/{testId}/questions/{questionId}/answers/{answerId}")]
		//public IHttpActionResult DeleteAnswerFromQuestion(int testId, int questionId, int answerId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  if (answerId <= 0)
		//    return BadRequest("Incorrect answer id.");

		//  try
		//  {
		//    TestDTO testDTO = _testService.DeleteAnswerFromQuestion(testId, questionId, answerId);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpGet]
		//[Route("api/tests/{testId}/questions/{questionId}/answers/{answerId}")]
		//public IHttpActionResult GetAnswerFromQuestion(int testId, int questionId, int answerId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");

		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  if (answerId <= 0)
		//    return BadRequest("Incorrect answer id.");

		//  try
		//  {
		//    AnswerDTO answerDTO = _testService.GetAnswerFromQuestion(testId, questionId, answerId);

		//    AnswerModel returnedAnswer = _mapper.Map<AnswerDTO, AnswerModel>(answerDTO);

		//    return Ok(returnedAnswer);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpGet]
		//[Route("api/tests/{testId}/questions/{questionId}/{answers}")]
		//public IHttpActionResult GetAllAnswersFromQuestion(int testId, int questionId)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");
		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");

		//  try
		//  {
		//    List<AnswerDTO> answers = _testService.GetAllAnswersFromQuestion(testId, questionId).ToList();

		//    List<AnswerModel> retunedAnswers = _mapper.Map<IEnumerable<AnswerDTO>, List<AnswerModel>>(answers);

		//    return Ok(retunedAnswers);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//[HttpPut]
		//[Route("api/tests/{testId}/questions/{questionId}/answers/{answerId}")]
		//public IHttpActionResult UpdateAnswerFromQuestion(int testId, int questionId, int answerId, AnswerModel answerModel)
		//{
		//  if (testId <= 0)
		//    return BadRequest("Incorrect test id.");
		//  if (questionId <= 0)
		//    return BadRequest("Incorrect question id.");
		//  if (answerId <= 0)
		//    return BadRequest("Incorrect answer id.");

		//  if (answerModel == null)
		//    return BadRequest("Answer must not be null.");

		//  try
		//  {
		//    AnswerDTO answerDTO = _mapper.Map<AnswerModel, AnswerDTO>(answerModel);

		//    TestDTO testDTO = _testService.UpdateAnswerFromQuestion(testId, questionId, answerId, answerDTO);

		//    TestModel returnedTest = _mapper.Map<TestDTO, TestModel>(testDTO);

		//    return Ok(returnedTest);
		//  }
		//  catch (EntityNotFoundException)
		//  {
		//    return NotFound();
		//  }
		//  catch (Exception ex)
		//  {
		//    return BadRequest(ex.Message);
		//  }
		//}

		//#endregion

	}
}
