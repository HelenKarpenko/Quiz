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
    public class QuestionsController : ApiController
    {
        private readonly IQuestionService _questionService;

        private IMapper _mapper;

        public QuestionsController(IQuestionService service)
        {
            _questionService = service ?? throw new ArgumentNullException("Service must not be null.");

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionModel, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, QuestionModel>();
            })
            .CreateMapper();
        }

        [HttpPost]
        public IHttpActionResult Create(QuestionModel question)
        {
            if (question == null)
                return BadRequest("Question must not be null.");

            try
            {
                QuestionDTO questionDTO = _mapper.Map<QuestionModel, QuestionDTO>(question);

                QuestionDTO createdQuestion = _questionService.Create(questionDTO);

                QuestionModel returnedQuestion = _mapper.Map<QuestionDTO, QuestionModel>(createdQuestion);

                string createdAtUrl = "http://www.google.com";

                return Created(createdAtUrl, createdQuestion);
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
                return BadRequest("Incorrect question id.");

            try
            {
                QuestionDTO questionDTO = _questionService.Get(id);

                QuestionModel returnedQuestion = _mapper.Map<QuestionDTO, QuestionModel>(questionDTO);

                return Ok(returnedQuestion);
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
            List<QuestionDTO> questions = _questionService.GetAll().ToList();

            List<QuestionModel> retunedQuestions = _mapper.Map<IEnumerable<QuestionDTO>, List<QuestionModel>>(questions);

            return Ok(retunedQuestions);
        }

        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            if (id <= 0)
                return BadRequest("Incorrect question id.");

            try
            {
                QuestionDTO questionDTO = _questionService.Delete(id);

                QuestionModel returnedQuestion = _mapper.Map<QuestionDTO, QuestionModel>(questionDTO);

                return Ok(returnedQuestion);
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
        public IHttpActionResult UpdateById(int id, QuestionModel question)
        {
            if (question == null)
                return BadRequest("Question must not be null.");

            try
            {
                QuestionDTO questionDTO = _mapper.Map<QuestionModel, QuestionDTO>(question);

                QuestionDTO updatedQuestion = _questionService.Update(id, questionDTO);

                QuestionModel returnedQuestion = _mapper.Map<QuestionDTO, QuestionModel>(updatedQuestion);

                return Ok(returnedQuestion);
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
