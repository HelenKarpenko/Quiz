using Quiz.BLL.DTO;
using System.Collections.Generic;

namespace Quiz.BLL.Interfaces
{
    public interface IQuestionService
    {
        QuestionDTO Create(QuestionDTO questionDTO);

        QuestionDTO Get(int id);

        IEnumerable<QuestionDTO> GetAll();

        QuestionDTO Update(int id, QuestionDTO questionDTO);

        QuestionDTO Delete(int id);

        void Dispose();
    }
}
