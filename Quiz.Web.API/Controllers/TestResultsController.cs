using AutoMapper;
using Microsoft.AspNet.Identity;
using Quiz.BLL.DTO.UserResult;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.Web.API.Models.UserResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Quiz.Web.API.Controllers
{
    public class TestResultsController : ApiController
    {
		private readonly ITestResultService _resultService;

		private IMapper _mapper;

		public TestResultsController(ITestResultService service)
		{
			_resultService = service ?? throw new ArgumentNullException("Service must not be null.");

			_mapper = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ResultDetailsModel, ResultDetailsDTO>();
				cfg.CreateMap<ResultDetailsDTO, ResultDetailsModel>();
				cfg.CreateMap<TestResultModel, TestResultDTO>();
				cfg.CreateMap<TestResultDTO, TestResultModel>();
			})
			.CreateMapper();
		}

		[HttpPost]
		public async Task<IHttpActionResult> Create(TestResultModel result)
		{
			if (result == null)
				return BadRequest("Test must not be null.");

			try
			{
				result.UserId = User.Identity.GetUserId();
				result.PassageDate = DateTime.Now;

				TestResultDTO resultDTO = _mapper.Map<TestResultModel, TestResultDTO>(result);

				TestResultDTO createdResult = await _resultService.Create(resultDTO);

				TestResultModel returnedResult = _mapper.Map<TestResultDTO, TestResultModel>(createdResult);

				string createdAtUrl = "http://www.google.com";

				return Created(createdAtUrl, returnedResult);
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
		
		[HttpGet]
		public async Task<IHttpActionResult> GetById(int id)
		{
			if (id <= 0)
				return BadRequest("Incorrect result id.");

			try
			{
				TestResultDTO resultDTO = await _resultService.Get(id);

				TestResultModel returnedResult = _mapper.Map<TestResultDTO, TestResultModel>(resultDTO);

				return Ok(returnedResult);
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
		
		[HttpGet]
		public async Task<IHttpActionResult> GetAll()
		{
			IEnumerable<TestResultDTO> resultDTO = await _resultService.GetAll();

			List<TestResultModel> result = _mapper.Map<IEnumerable<TestResultDTO>, List<TestResultModel>>(resultDTO);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteById(int id)
		{
			if (id <= 0)
				return BadRequest("Incorrect result id.");

			try
			{
				TestResultDTO resultDTO = await _resultService.Delete(id);

				TestResultModel returnedResult = _mapper.Map<TestResultDTO, TestResultModel>(resultDTO);

				return Ok(returnedResult);
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

		protected override void Dispose(bool disposing)
		{
			_resultService.Dispose();
			base.Dispose(disposing);
		}
	}
}
