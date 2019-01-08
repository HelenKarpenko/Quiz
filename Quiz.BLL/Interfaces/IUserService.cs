using Quiz.BLL.DTO;
using System.Collections.Generic;

namespace Quiz.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO Create(UserDTO userDTO);

        UserDTO Get(int id);

        IEnumerable<UserDTO> GetAll();

        UserDTO Update(int id, UserDTO userDTO);

        UserDTO Delete(int id);

        void Dispose();
    }
}
