using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quiz.Web.API.Controllers
{
    public class AnswersController : ApiController
    {
        private readonly IAnswerService _answerService;

        private IMapper _mapper;

        public AnswersController(IAnswerService service)
        {
            _answerService = service ?? throw new ArgumentNullException("Service must not be null.");

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerModel, AnswerDTO>();
                cfg.CreateMap<AnswerDTO, AnswerModel>();
            })
            .CreateMapper();
        }

        [HttpPost]
        public IHttpActionResult Create(AnswerModel answer)
        {
            if (answer == null)
                return BadRequest("Answer must not be null.");

            try
            {
                AnswerDTO answerDTO = _mapper.Map<AnswerModel, AnswerDTO>(answer);

                AnswerDTO createdAnswer = _answerService.Create(answerDTO);

                AnswerModel returnedAnswer = _mapper.Map<AnswerDTO, AnswerModel>(createdAnswer);

                string createdAtUrl = "http://www.google.com";

                return Created(createdAtUrl, returnedAnswer);
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
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Incorrect answer id.");

            try
            {
                AnswerDTO answerDTO = _answerService.Get(id);

                AnswerModel returnedAnswer = _mapper.Map<AnswerDTO, AnswerModel>(answerDTO);

                return Ok(returnedAnswer);
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
        public IHttpActionResult GetAll()
        {
            List<AnswerDTO> answers = _answerService.GetAll().ToList();

            List<AnswerModel> retunedAnswers = _mapper.Map<IEnumerable<AnswerDTO>, List<AnswerModel>>(answers);

            return Ok(retunedAnswers);
        }

        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            if (id <= 0)
                return BadRequest("Incorrect answer id.");

            try
            {
                AnswerDTO answerDTO = _answerService.Delete(id);

                AnswerModel returnedAnswer = _mapper.Map<AnswerDTO, AnswerModel>(answerDTO);

                return Ok(returnedAnswer);
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
        public IHttpActionResult UpdateById(int id, AnswerModel answer)
        {
            if (answer == null)
                return BadRequest("Answer must not be null.");

            try
            {
                AnswerDTO answerDTO = _mapper.Map<AnswerModel, AnswerDTO>(answer);

                AnswerDTO updatedAnswer = _answerService.Update(id, answerDTO);

                AnswerModel returnedAnswer = _mapper.Map<AnswerDTO, AnswerModel>(updatedAnswer);

                return Ok(returnedAnswer);
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
    }
}
