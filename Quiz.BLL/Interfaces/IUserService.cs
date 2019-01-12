using Quiz.BLL.DTO;
using System.Collections.Generic;

namespace Quiz.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO Create(UserDTO userDTO);

        UserDTO Get(string id);

        IEnumerable<UserDTO> GetAll();

        UserDTO Update(string id, UserDTO userDTO);

        UserDTO Delete(string id);

        void Dispose();
    }
}
