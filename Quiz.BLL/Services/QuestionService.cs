using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _database;

        private IMapper mapper;

        public QuestionService(IUnitOfWork uow)
        {
            _database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

            mapper = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<QuestionDTO, Question>();
                cfg.CreateMap<Question, QuestionDTO>();
            })
            .CreateMapper();
        }

        public QuestionDTO Create(QuestionDTO questionDTO)
        {
            if (questionDTO == null)
                throw new ArgumentNullException("QuestionDTO must not be null.");

            Question question = mapper.Map<QuestionDTO, Question>(questionDTO);

            Question createdQuestion = _database.Questions.Create(question);

            _database.Save();

            QuestionDTO returnedQuestion = mapper.Map<Question, QuestionDTO>(createdQuestion);

            return returnedQuestion;
        }
        
        public QuestionDTO Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect question id.");

            Question deletedQuestion = _database.Questions.Delete(id);

            if (deletedQuestion == null)
                throw new EntityNotFoundException($"Question with id = {id} not found.");

            _database.Save();

            QuestionDTO returnedQuestion = mapper.Map<Question, QuestionDTO>(deletedQuestion);

            return returnedQuestion;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public QuestionDTO Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect question id.");

            Question question = _database.Questions.Get(id);

            if (question == null)
                throw new EntityNotFoundException($"Question with id = {id} not found.");

            QuestionDTO returnedQuestion = mapper.Map<Question, QuestionDTO>(question);

            return returnedQuestion;
        }

        public IEnumerable<QuestionDTO> GetAll()
        {
            List<Question> questions = _database.Questions.GetAll().ToList();

            List<QuestionDTO> returnedQuestions = mapper.Map<List<Question>, List<QuestionDTO>>(questions);

            return returnedQuestions;
        }

        public QuestionDTO Update(int id, QuestionDTO questionDTO)
        {
            if (questionDTO == null)
                throw new ArgumentNullException("QuestionDTO must not be null.");

            if (id <= 0)
                throw new ArgumentException("Incorrect question id.");

            questionDTO.Id = id;

            Question question = mapper.Map<QuestionDTO, Question>(questionDTO);

            Question updatedQuestion = _database.Questions.Update(id, question);

            if (updatedQuestion == null)
                throw new EntityNotFoundException($"Question with id = {id} not found.");

            _database.Save();

            QuestionDTO returnedQuestion = mapper.Map<Question, QuestionDTO>(updatedQuestion);

            return returnedQuestion;
        }
    }
}
