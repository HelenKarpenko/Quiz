using Quiz.BLL.DTO;
using System.Collections.Generic;

namespace Quiz.BLL.Interfaces
{
    public interface IAnswerService
    {
        AnswerDTO Create(AnswerDTO answerDTO);

        AnswerDTO Get(int id);

        IEnumerable<AnswerDTO> GetAll();

        AnswerDTO Update(int id, AnswerDTO answerDTO);

        AnswerDTO Delete(int id);

        void Dispose();
    }
}
