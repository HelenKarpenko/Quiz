using AutoMapper;
using Microsoft.AspNet.Identity;
using Quiz.BLL.DTO;
using Quiz.BLL.DTO.User;
using Quiz.BLL.DTO.UserResult;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.Web.API.Models;
using Quiz.Web.API.Models.UserResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace Quiz.Web.API.Controllers
{
	[Authorize(Roles = "admin")]
	public class UsersController : ApiController
	{
		private readonly IUserService _userService;

		private IMapper _mapper;

		public UsersController(IUserService service)
		{
			_userService = service ?? throw new ArgumentNullException("Service must not be null.");

			_mapper = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UserModel, UserDTO>();
				cfg.CreateMap<UserDTO, UserModel>();
				cfg.CreateMap<PagedResultDTO<UserDTO>.PagingInfo, PagedResultModel<UserModel>.PagingInfo>();
				cfg.CreateMap<PagedResultDTO<UserDTO>, PagedResultModel<UserModel>>();
				cfg.CreateMap<ResultDetailsModel, ResultDetailsDTO>();
				cfg.CreateMap<ResultDetailsDTO, ResultDetailsModel>();
				cfg.CreateMap<TestResultModel, TestResultDTO>();
				cfg.CreateMap<TestResultDTO, TestResultModel>();
				cfg.CreateMap<TestModel, TestDTO>();
				cfg.CreateMap<TestDTO, TestModel>();
			})
			.CreateMapper();
		}
		
		[HttpGet]
		public async Task<IHttpActionResult> GetById(string id)
		{
			if (id == null)
				return BadRequest("Incorrect user id.");

			try
			{
				UserDTO userDTO = await _userService.GetById(id);

				UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(userDTO);

				return Ok(returnedUser);
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
		public async Task<IHttpActionResult> Get(
												 string query = "",
												 int page = 1,
												 int pageSize = 10)
		{

			string searchQuery = String.IsNullOrEmpty(query) ? "" : query;

			page = page > 0 ? page : 1;
			pageSize = pageSize > 0 ? pageSize : 10;

			PagedResultDTO<UserDTO> pagedResultDTO = await _userService.GetPaged(searchQuery, page, pageSize);

			PagedResultModel<UserModel> pagedResult = _mapper.Map<PagedResultDTO<UserDTO>, PagedResultModel<UserModel>>(pagedResultDTO);

			return Ok(pagedResult);
		}

		[HttpDelete]
		public async Task<IHttpActionResult> DeleteById(string id)
		{
			if (id == null)
				return BadRequest("Incorrect user id.");

			try
			{
				UserDTO userDTO = await _userService.Delete(id);

				UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(userDTO);

				return Ok(returnedUser);
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
		public async Task<IHttpActionResult> UpdateById(string id, UserModel user)
		{
			if (id == null)
				return BadRequest("Incorrect user id.");
			if (user == null)
				return BadRequest("User must not be null.");

			try
			{
				user.Id = id;

				UserDTO userDTO = _mapper.Map<UserModel, UserDTO>(user);

				UserDTO updatedUser = await _userService.Update(userDTO);

				UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(updatedUser);

				return Ok(returnedUser);
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
		[Route("api/users/currentUser")]
		public async Task<IHttpActionResult> GetCurrentUser()
		{
			try
			{
				UserDTO userDTO = await _userService.GetById(User.Identity.GetUserId());

				UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(userDTO);

				return Ok(returnedUser);
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
		[Route("api/users/{userId}/results")]
		public IHttpActionResult GetUserTestResults(string userId)
		{
			if (userId == null)
				return BadRequest("Incorrect user id.");

			try
			{
				IEnumerable<TestResultDTO> resultsDTO = _userService.GetAllTests(userId);

				List<TestResultModel> results = _mapper.Map<IEnumerable<TestResultDTO>, List<TestResultModel>>(resultsDTO);

				return Ok(results);
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
			_userService.Dispose();
			base.Dispose(disposing);
		}
	}
}
