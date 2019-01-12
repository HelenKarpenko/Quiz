//using AutoMapper;
//using Quiz.BLL.DTO;
//using Quiz.BLL.Exceptions;
//using Quiz.BLL.Interfaces;
//using Quiz.Web.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace Quiz.Web.API.Controllers
//{
//  public class UsersController : ApiController
//  {
//    private readonly IUserService _userService;

//    private IMapper _mapper;

//    public UsersController(IUserService service)
//    {
//      _userService = service ?? throw new ArgumentNullException("Service must not be null.");

//      _mapper = new MapperConfiguration(cfg =>
//      {
//        cfg.CreateMap<UserModel, UserDTO>();
//        cfg.CreateMap<UserDTO, UserModel>();
//      })
//      .CreateMapper();
//    }

//    [HttpPost]
//    public IHttpActionResult Create(UserModel user)
//    {
//      if (user == null)
//        return BadRequest("User must not be null.");

//      try
//      {
//        UserDTO userDTO = _mapper.Map<UserModel, UserDTO>(user);

//        UserDTO createdUser = _userService.Create(userDTO);

//        UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(createdUser);

//        string createdAtUrl = "http://www.google.com";

//        return Created(createdAtUrl, returnedUser);
//      }
//      catch (EntityNotFoundException)
//      {
//        return NotFound();
//      }
//      catch (Exception ex)
//      {
//        return BadRequest(ex.Message);
//      }
//    }

//    [HttpGet]
//    public IHttpActionResult GetById(int id)
//    {
//      if (id <= 0)
//        return BadRequest("Incorrect user id.");

//      try
//      {
//        UserDTO userDTO = _userService.Get(id);

//        UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(userDTO);

//        return Ok(returnedUser);
//      }
//      catch (EntityNotFoundException)
//      {
//        return NotFound();
//      }
//      catch (Exception ex)
//      {
//        return BadRequest(ex.Message);
//      }
//    }

//    [Authorize]
//    [HttpGet]
//    public IHttpActionResult GetAll()
//    {
//      List<UserDTO> users = _userService.GetAll().ToList();

//      List<UserModel> retunedUsers = _mapper.Map<IEnumerable<UserDTO>, List<UserModel>>(users);

//      return Ok(retunedUsers);
//    }

//    [HttpDelete]
//    public IHttpActionResult DeleteById(int id)
//    {
//      if (id <= 0)
//        return BadRequest("Incorrect user id.");

//      try
//      {
//        UserDTO userDTO = _userService.Delete(id);

//        UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(userDTO);

//        return Ok(returnedUser);
//      }
//      catch (EntityNotFoundException)
//      {
//        return NotFound();
//      }
//      catch (Exception ex)
//      {
//        return BadRequest(ex.Message);
//      }
//    }

//    [HttpPut]
//    public IHttpActionResult UpdateById(int id, UserModel user)
//    {
//      if (user == null)
//        return BadRequest("User must not be null.");

//      try
//      {
//        UserDTO userDTO = _mapper.Map<UserModel, UserDTO>(user);

//        UserDTO updatedUser = _userService.Update(id, userDTO);

//        UserModel returnedUser = _mapper.Map<UserDTO, UserModel>(updatedUser);

//        return Ok(returnedUser);
//      }
//      catch (EntityNotFoundException)
//      {
//        return NotFound();
//      }
//      catch (Exception ex)
//      {
//        return BadRequest(ex.Message);
//      }
//    }
//  }
//}
