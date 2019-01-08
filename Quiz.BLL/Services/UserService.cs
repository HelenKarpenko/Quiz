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
//    public class UserService : IUserService
//    {
//        private readonly IUnitOfWork _database;

//        private IMapper _mapper;

//        public UserService(IUnitOfWork uow)
//        {
//            _database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

//            _mapper = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<User, UserDTO>();
//                cfg.CreateMap<UserDTO, User>();
//            })
//            .CreateMapper();
//        }

//        public UserDTO Create(UserDTO userDTO)
//        {
//            if (userDTO == null)
//                throw new ArgumentNullException("UserDTO must not be null.");

//            User user = _mapper.Map<UserDTO, User>(userDTO);

//            User createdUser = _database.Users.Create(user);

//            _database.Save();

//            UserDTO returnedUser = _mapper.Map<User, UserDTO>(createdUser);

//            return returnedUser;
//        }

//        public UserDTO Delete(int id)
//        {
//            if (id <= 0)
//                throw new ArgumentException("Incorrect userDTO id.");

//            User deletedUser = _database.Users.Delete(id);

//            if (deletedUser == null)
//                throw new EntityNotFoundException($"User with id = {id} not found.");

//            _database.Save();

//            UserDTO returnedUser = _mapper.Map<User, UserDTO>(deletedUser);

//            return returnedUser;
//        }

//        public void Dispose()
//        {
//            _database.Dispose();
//        }

//        public UserDTO Get(int id)
//        {
//            if (id <= 0)
//                throw new ArgumentException("Incorrect userDTO id.");

//            User user = _database.Users.Get(id);

//            if (user == null)
//                throw new EntityNotFoundException($"User with id = {id} not found.");

//            UserDTO returnedUser = _mapper.Map<User, UserDTO>(user);

//            return returnedUser;
//        }

//        public IEnumerable<UserDTO> GetAll()
//        {
//            List<User> users = _database.Users.GetAll().ToList();

//            List<UserDTO> returnedUsers = _mapper.Map<List<User>, List<UserDTO>>(users);

//            return returnedUsers;
//        }

//        public UserDTO Update(int id, UserDTO userDTO)
//        {
//            if (userDTO == null)
//                throw new ArgumentNullException("UserDTO must not by null.");

//            if (id <= 0)
//                throw new ArgumentException("Incorrect user id.");

//            userDTO.Id = id;

//            User user = _mapper.Map<UserDTO, User>(userDTO);

//            User updatedUser = _database.Users.Update(id, user);

//            if (updatedUser == null)
//                throw new EntityNotFoundException($"User with id = {id} not found.");

//            _database.Save();

//            UserDTO returnedUser = _mapper.Map<User, UserDTO>(updatedUser);

//            return returnedUser;
//        }
//    }
//}
