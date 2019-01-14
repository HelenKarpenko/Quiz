using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.DTO.User;
using Quiz.BLL.DTO.UserResult;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.DAL.Entities;
using Quiz.DAL.Entities.User;
using Quiz.DAL.Entities.UserResults;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.BLL.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _database;

		private IMapper _mapper;

		public UserService(IUnitOfWork uow)
		{
			_database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

			_mapper = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ApplicationUser, UserDTO>();
				cfg.CreateMap<UserDTO, ApplicationUser>();
				cfg.CreateMap<Test, TestDTO>();
				cfg.CreateMap<TestResult, TestResultDTO>();
				cfg.CreateMap<TestResultDTO, TestResult>();
				cfg.CreateMap<ResultDetails, ResultDetailsDTO>();
				cfg.CreateMap<ResultDetailsDTO, ResultDetails>();
			})
			.CreateMapper();
		}

		public async Task<UserDTO> GetById(string id)
		{
			if (id == null)
				throw new ArgumentNullException("Id must not be null.");

			ApplicationUser user = await _database.UserManager.FindByIdAsync(id);
			if (user == null)
				throw new EntityNotFoundException();

			var roles = await GetRoles(id);

			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(user);

			returnedUser.Roles = roles.ToList();
			if (user.UserInfo == null)
				returnedUser.Name = user.UserInfo.Name;

			return returnedUser;
		}

		public async Task<IEnumerable<UserDTO>> GetAll()
		{
			IEnumerable<ApplicationUser> users = _database.UserManager.Users.ToList();

			List<UserDTO> returnedUsers = _mapper.Map<IEnumerable<ApplicationUser>, List<UserDTO>>(users);

			foreach (var user in returnedUsers)
			{
				var roles = await GetRoles(user.Id);
				user.Roles = roles.ToList();
			}

			return returnedUsers;
		}

		public async Task<PagedResultDTO<UserDTO>> GetPaged(
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

			IEnumerable<UserDTO> users = await GetAll();

			List<UserDTO> returnedUsers = users.Where(u => u.UserName.ToLower().Contains(query.ToLower()) || u.Email.ToLower().Contains(query.ToLower())).ToList();

			int skip = pageSize * (page - 1);
			int total = returnedUsers.Count();

			List<UserDTO> result = returnedUsers
				.OrderBy(t => t.Id)
				.Skip(skip)
				.Take(pageSize)
				.ToList();
			
			return new PagedResultDTO<UserDTO>(result, page, pageSize, total);
		}

		public async Task<IEnumerable<string>> GetRoles(string id)
		{
			if (id == null)
				throw new ArgumentNullException("Incorrect user id.");

			return await _database.UserManager.GetRolesAsync(id);
		}

		public async Task<UserDTO> Delete(string id)
		{
			if (id == null)
				throw new ArgumentException("Incorrect userDTO id.");

			ApplicationUser user = await _database.UserManager.FindByIdAsync(id);
			if (user == null)
				throw new EntityNotFoundException();

			var roles = await GetRoles(id);

			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(user);

			var result = await _database.UserManager.DeleteAsync(user);
			if (!result.Succeeded)
				throw new EntityNotFoundException();

			returnedUser.Roles = roles.ToList();

			return returnedUser;
		}

		public async Task<UserDTO> Update(UserDTO userDTO)
		{
			if (userDTO == null)
				throw new ArgumentNullException("UserDTO must not by null.");
			
			ApplicationUser user = await _database.UserManager.FindByIdAsync(userDTO.Id);
			if (user == null)
				throw new EntityNotFoundException();

			var info = user.UserInfo;
			if (info == null)
				user.UserInfo = new UserInfo();

			user.UserInfo.Name = userDTO.Name;

			var result = await _database.UserManager.UpdateAsync(user);
			if (!result.Succeeded)
				throw new EntityNotFoundException();

			var roles = await GetRoles(user.Id);

			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(user);

			returnedUser.Roles = roles.ToList();
			returnedUser.Name = user.UserInfo.Name;

			return returnedUser;
		}

		public async Task<IEnumerable<TestResultDTO>> GetAllTests(string id)
		{
			if (id == null)
				throw new ArgumentNullException("Id must not be null.");

			IEnumerable<TestResult> results = await _database.TestResults.FindAsync(x => x.UserId == id);

			List<TestResultDTO> resultsDTO = _mapper.Map<IEnumerable<TestResult>, List<TestResultDTO>>(results);

			foreach (var r in results)
			{
				foreach (var rDTO in resultsDTO)
				{
					rDTO.TestName = r.Test.Name;
					rDTO.MaxResult = r.Test.Questions.Count;
					rDTO.UserName = r.User.ApplicationUser.UserName;
				}
			}

			return resultsDTO;
		}

		//public async Task<IEnumerable<TestResultDTO>> GetResultByTestId(string userId, int testId)
		//{
		//	if (userId == null)
		//		throw new ArgumentNullException("Id must not be null.");
		//	if (testId <= 0)
		//		throw new ArgumentException("Incorrect test id");

		//	IEnumerable<TestResult> results = await _database.TestResults.FindAsync(x => x.UserId == userId && testId);

		//	List<TestResultDTO> resultsDTO = _mapper.Map<IEnumerable<TestResult>, List<TestResultDTO>>(results);

		//	foreach (var r in results)
		//	{
		//		foreach (var rDTO in resultsDTO)
		//		{
		//			rDTO.TestName = r.Test.Name;
		//			rDTO.MaxResult = r.Test.Questions.Count;
		//		}
		//	}

		//	return resultsDTO;
		//}

		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
