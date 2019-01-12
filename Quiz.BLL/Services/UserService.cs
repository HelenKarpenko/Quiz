//using AutoMapper;
//using Quiz.BLL.DTO;
//using Quiz.BLL.Exceptions;
//using Quiz.BLL.Interfaces;
//using Quiz.DAL.Entities;
//using Quiz.DAL.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Quiz.BLL.Services
//{
//	public class UserService : IUserService
//	{
//		private readonly IUnitOfWork _database;

//		private IMapper _mapper;

//		public UserService(IUnitOfWork uow)
//		{
//			_database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

//			_mapper = new MapperConfiguration(cfg =>
//			{
//				cfg.CreateMap<ApplicationUser, UserDTO>();
//				cfg.CreateMap<UserDTO, ApplicationUser>();
//			})
//			.CreateMapper();
//		}

//		public UserDTO Create(UserDTO userDTO)
//		{
//			if (userDTO == null)
//				throw new ArgumentNullException("UserDTO must not be null.");

//			ApplicationUser user = _mapper.Map<UserDTO, ApplicationUser>(userDTO);

//			ApplicationUser createdUser = _database.Users.Create(user);

//			_database.Save();

//			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(createdUser);

//			return returnedUser;
//		}

//		public UserDTO Delete(string id)
//		{
//			if (id == null)
//				throw new ArgumentException("Incorrect userDTO id.");

//			ApplicationUser deletedUser = _database.Users.Delete(id);

//			if (deletedUser == null)
//				throw new EntityNotFoundException($"User with id = {id} not found.");

//			_database.Save();

//			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(deletedUser);

//			return returnedUser;
//		}

//		public void Dispose()
//		{
//			_database.Dispose();
//		}

//		public UserDTO Get(string id)
//		{
//			if (id == null)
//				throw new ArgumentException("Incorrect userDTO id.");

//			ApplicationUser user = _database.Users.Get(id);

//			if (user == null)
//				throw new EntityNotFoundException($"User with id = {id} not found.");

//			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(user);

//			return returnedUser;
//		}

//		public IEnumerable<UserDTO> GetAll()
//		{
//			List<ApplicationUser> users = _database.Users.GetAll().ToList();

//			List<UserDTO> returnedUsers = _mapper.Map<List<ApplicationUser>, List<UserDTO>>(users);

//			return returnedUsers;
//		}

//		public UserDTO Update(string id, UserDTO userDTO)
//		{
//			if (userDTO == null)
//				throw new ArgumentNullException("UserDTO must not by null.");
//			userDTO.Id = id ?? throw new ArgumentException("Incorrect user id.");

//			ApplicationUser user = _mapper.Map<UserDTO, ApplicationUser>(userDTO);

//			ApplicationUser updatedUser = _database.Users.Update(id, user);

//			if (updatedUser == null)
//				throw new EntityNotFoundException($"User with id = {id} not found.");

//			_database.Save();

//			UserDTO returnedUser = _mapper.Map<ApplicationUser, UserDTO>(updatedUser);

//			return returnedUser;
//		}
//	}
//}
