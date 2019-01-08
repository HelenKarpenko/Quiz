using AutoMapper;
using Quiz.BLL.DTO;
using Quiz.BLL.Exceptions;
using Quiz.BLL.Interfaces;
using Quiz.DAL.Entities;
using Quiz.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _database;

        private IMapper mapper; 

        public AnswerService(IUnitOfWork uow)
        {
            _database = uow ?? throw new ArgumentNullException("UnitOfWork must not be null.");

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerDTO, Answer>();
                cfg.CreateMap<Answer, AnswerDTO>();
            })
            .CreateMapper();
        }
        
        public AnswerDTO Create(AnswerDTO answerDTO)
        {
            if (answerDTO == null)
                throw new ArgumentNullException("AnswerDTO must not be null.");
            
            Answer answer = mapper.Map<AnswerDTO, Answer>(answerDTO);

            Answer createdAnswer = _database.Answers.Create(answer);
            
            _database.Save();

            AnswerDTO returnedAnswer = mapper.Map<Answer, AnswerDTO>(createdAnswer);

            return returnedAnswer;
        }

        public AnswerDTO Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect answerDTO id.");

            Answer deletedAnswer = _database.Answers.Delete(id);

            if (deletedAnswer == null)
                throw new EntityNotFoundException($"Answer with id = {id} not found.");

            _database.Save();

            AnswerDTO returnedAnswer = mapper.Map<Answer, AnswerDTO>(deletedAnswer);

            return returnedAnswer;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public AnswerDTO Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect answerDTO id.");

            Answer answer = _database.Answers.Get(id);

            if (answer == null)
                throw new EntityNotFoundException($"Answer with id = {id} not found.");

            AnswerDTO returnedAnswer = mapper.Map<Answer, AnswerDTO>(answer);

            return returnedAnswer;
        }

        public IEnumerable<AnswerDTO> GetAll()
        {
            List<Answer> answers = _database.Answers.GetAll().ToList();

            List<AnswerDTO> returnedAnswers = mapper.Map<List<Answer>, List<AnswerDTO>>(answers);

            return returnedAnswers;
        }

        public AnswerDTO Update(int id, AnswerDTO answerDTO)
        {
            if (answerDTO == null)
                throw new ArgumentNullException("AnswerDTO must not by null.");

            if (id <= 0)
                throw new ArgumentException("Incorrect answer id.");

            answerDTO.Id = id;

            Answer answer = mapper.Map<AnswerDTO, Answer>(answerDTO);

            Answer updatedAnswer = _database.Answers.Update(id, answer);

            if(updatedAnswer == null)
                throw new EntityNotFoundException($"Answer with id = {id} not found.");

            _database.Save();

            AnswerDTO returnedAnswer = mapper.Map<Answer, AnswerDTO>(updatedAnswer);

            return returnedAnswer;
        }
    }
}
